using MemoProject.Contracts;
using MemoProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(MemoDbContext context) : base(context)
        {
        }

        public async Task<bool> AddRange(IEnumerable<string> tagList, Memo memo)
        {
            var tags = new List<Tag>();
            foreach (var tagItem in tagList)
            {
                Tag tag = new();
                tag.Name = tagItem;
                tag.Memo = memo;
                tags.Add(tag);
            }
            await _context.Tag.AddRangeAsync(tags);
            return true;
        }

        public async Task DeleteByMemoAsync(long memoId)
        {
            var tags = await DbSet
            .Where(tag => tag.MemoId == memoId)
            .ToListAsync();
            DbSet.RemoveRange(tags);
        }
    }
}
