using MemoProject.Contracts;
using MemoProject.Data;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMemoRepository Memo { get; private set; }
        public ITagRepository Tag { get; private set; }
        public ISettingsReposirory Setting {get; private set; }

        public IDeafultSettingsRepository DefaultSettings { get; private set; } 
        public IAuditRepository Audit { get; private set; }

        protected readonly MemoDbContext _context;

        public UnitOfWork(MemoDbContext context)
        {
            _context = context;
            Memo = new MemoRepository(_context);
            Tag = new TagRepository(_context);
            Setting = new SettingsRepository(_context);
            DefaultSettings = new DefaultSettingsRepository(_context);
            Audit = new AuditRepository(_context);
            
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
