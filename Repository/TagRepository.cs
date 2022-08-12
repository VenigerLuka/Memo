using MemoProject.Contracts;
using MemoProject.Data;

namespace MemoProject.Repository
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(MemoDbContext context) : base(context)
        {
        }
    }
}
