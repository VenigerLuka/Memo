using MemoProject.Contracts;
using MemoProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Repository
{


    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        public readonly MemoDbContext _context;
        protected readonly DbSet<T> DbSet;

        public BaseRepository(MemoDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteByID(long id)
        {
            var memo = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(memo);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> FindById(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task<IQueryable<T>> FindAllQ()
        {
            return _context.Set<T>()
                .AsNoTracking();
        }
    }

}
