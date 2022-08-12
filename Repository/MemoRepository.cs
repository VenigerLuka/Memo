using MemoProject.Contracts;
using MemoProject.Data;

namespace MemoProject.Repository
{
    public class MemoRepository : BaseRepository<Memo>, IMemoRepository
    {
        private readonly MemoDbContext _context;
        public MemoRepository(MemoDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
