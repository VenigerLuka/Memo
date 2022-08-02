using MemoProject.Data;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMemoRepository Memo { get; private set; }
        public ITagRepository Tag { get; private set; }
        public IAdminRepository Admin { get; private set; }
        

        protected readonly MemoDbContext _context;

        public UnitOfWork(MemoDbContext context)
        {
            _context = context;
            Memo = new MemoRepository(_context);
            Tag = new TagRepository(_context);
            Admin = new AdminRepository(_context);
            
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
