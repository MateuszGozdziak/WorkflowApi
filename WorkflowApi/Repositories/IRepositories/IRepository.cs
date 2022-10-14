using System.Threading.Tasks;

namespace WorkflowApi.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        void Add(T entity);
        void Remove(T entity);
        //update też void bo pyta dopiero przy save
    }
}
