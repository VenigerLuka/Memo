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
        private readonly MemoDbContext _context;

        public BaseRepository(MemoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T memo)
        {
            await _context.Set<T>().AddAsync(memo);
        }

        public void DeleteByID(long id)
        {
            var memo = _context.Memo.Find(id);
            _context.Memo.Remove(memo);
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

        public void Update(T memo)
        {
            _context.Set<T>().Update(memo);
        }
        public IQueryable<T> FindAllQ()
        {
            return _context.Set<T>()
                .AsNoTracking();
        }
    }

}
