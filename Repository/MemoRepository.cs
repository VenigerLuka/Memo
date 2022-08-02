using MemoProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MemoProject.Helpers;

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
