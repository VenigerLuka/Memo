using MemoProject.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class AdminRepository : BaseRepository<IdentityUser>, IAdminRepository
    {
        private readonly MemoDbContext _context;

        public AdminRepository(MemoDbContext context) : base(context)
        {
            _context = context;
        }



        public IEnumerable<IdentityUser> FindAll()
        {
            return  _context.Set<IdentityUser>().ToList();
        }

        public async Task<IdentityUser> FindById(string id)
        {
            return await _context.Set<IdentityUser>().FindAsync(id);
        }
    }
}
