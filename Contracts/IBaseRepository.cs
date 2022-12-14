using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T memo);
        void Delete(T entity);
        void DeleteByID(long id);
        Task<IEnumerable<T>> FindAll();
        Task<T> FindById(long id);
        void Update(T memo);
        IQueryable<T> FindAllQ();
    }
}