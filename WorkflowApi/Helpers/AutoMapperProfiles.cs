using AutoMapper;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Entities;

namespace WorkflowApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateProjection<AppTaskAudit, AppTaskAuditDto>()
                .ForMember(dest => dest.UpdaterEmail, opt => opt.MapFrom(source => source.Updater.Email));
            CreateMap<AppTaskUpdateDto, AppTask>();
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<MessageDto, Message>();
            CreateMap<Message, MessageDto>();
            CreateMap<SyncfusionTaskDto, AppTask>()
                .ForMember(destinationMember => destinationMember.Id, act => act.Ignore());
            CreateMap<AppUser, TeamMemberDto>();

            CreateMap<AppTask, CreateAppTaskDto>();
            CreateMap<CreateAppTaskDto, AppTask>()  // CreateAppTaskDto
                .ForMember(dest => dest.LastUpdaterId, opt => opt.MapFrom((src, dst, _, context) =>
                    context.Options.Items["UserId"]
                ));

            CreateMap<AppTask, AppTaskDto>();
            CreateMap<AppTaskDto, AppTask>()
                .ForMember(dest => dest.LastUpdaterId, opt => opt.MapFrom((src, dst, _, context) =>
                    context.Options.Items["UserId"]
                ));

                //.ForMember(dest => dest.AppTaskChanges, opt => opt.MapFrom(sourse =>
                //    sourse.AppTaskChanges.Select(appTaskChanges =>
                //        new AppTaskChanges()
                //        {
                //            ActionType = appTaskChanges.ActionType,
                //            Id = appTaskChanges.Id,
                //            EntityName = appTaskChanges.EntityName,
                //            Username = appTaskChanges.Username,
                //            TimeStamp = appTaskChanges.TimeStamp,
                //            EntityId = appTaskChanges.EntityId,
                //        })
                //));
                //.ForMember(destinationMember => destinationMember, act => act.Ignore());
                //.ForMember(dest => dest.AppTaskChanges, opt => opt.MapFrom(source => source.AppTaskChanges.Select(x => x.ActionType)));

            CreateMap<GanttEvent, GanttEventDto>();
            CreateMap<GanttEventDto, GanttEvent>();
            
        }


    }
}
