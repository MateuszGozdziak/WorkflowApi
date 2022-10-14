using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkflowApi;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Models.Validators;
using WorkflowApi.Services;
using WorkflowApi.SignalR;
using StackExchange.Redis;
using WorkflowApi.Entities;
using WorkflowApi.Models;
using WorkflowApi.Repositories.IRepositories;
using WorkflowApi.Repositories;
using WorkflowApi.Helpers;
using System.Reflection;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.Batch;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new JwtSettings();
builder.Configuration.GetSection("JWT").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddIdentityCore<AppUser>(opt =>
    {
        opt.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<AppRole>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(authenticationSettings.JwtKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authenticationSettings.jwtIssuer,
            ValidAudience = authenticationSettings.jwtIssuer
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = contextUnknow =>
            {
                var accessToken = contextUnknow.Request.Query["access_token"];
                var path = contextUnknow.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                {
                    contextUnknow.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };

    });

builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ErrorHandlingMiddleware> (); //CustomExceptionMiddleware doczytaæ czy potrzebny do tego servis
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator> ();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
}).AddStackExchangeRedis(builder.Configuration.GetConnectionString("Redis"), options =>
{
   options.Configuration.ChannelPrefix = "SignalRWebApi";
});
builder.Services.AddCors();

ConfigurationOptions RedisOptions = new ConfigurationOptions()
{
    EndPoints = { { builder.Configuration.GetConnectionString("Redis") } },
    IncludeDetailInExceptions=true,
};

builder.Services.AddSingleton<IConnectionMultiplexer>(opt => 
    ConnectionMultiplexer.Connect(RedisOptions)
);
builder.Services.AddHttpContextAccessor();////mo¿e nie dzia³aæ


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ErrorHandlingMiddleware>();//CustomExceptionMiddleware

app.UseODataRouteDebug();

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200", "http://localhost:64569")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

//app.Use((context, next) => {
//    context.Response.Headers["Access-Control-Allow-Origin"] = "*";
//    return next.Invoke();
//});
app.UseODataBatching();

app.UseRouting();

app.UseAuthentication();//added

app.UseAuthorization();//a mo¿e to?

///???

app.MapControllers();//dodajemy/mapujemy tutaj œcie¿ki(route) do kontrolerów czyli tak w³aœciwie sprawiamy ¿e np.
//'api/Messages/PostMessage' jest widoczna i dzia³a

//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.MapHub<PresenceHub>("hubs/presence");//dodajemy tutaj œcie¿kê(route) do naszego haba odpowiadzialnego za aktywnoœæ u¿ytkpowników
app.MapHub<MessageHub>("hubs/messages");
app.MapHub<AppTaskHub>("hubs/AppTasks");
//signalr nie mo¿e wysy³aæ nag³ówków do autoryzacji

//jeœli nie bêdzie dzia³œ signalR dodaæ Cors
app.Run();

