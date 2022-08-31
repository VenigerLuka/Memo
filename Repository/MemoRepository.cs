using MemoProject.Contracts;
using MemoProject.Data;

namespace MemoProject.Repository
{
    public class MemoRepository : BaseRepository<Memo>, IMemoRepository
    {
        
        public MemoRepository(MemoDbContext context) : base(context)
        {
        }


    }
}
