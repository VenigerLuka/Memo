using MemoProject.Contracts;
using MemoProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class SettingsRepository : BaseRepository<Setting>, ISettingsReposirory
    {
        public SettingsRepository(MemoDbContext context) : base(context)
        {
        }

        public async Task<Setting> FindByUserIdAsync(string userId)
            => await DbSet
            .Where(setting => setting.UserId == userId)
            .FirstOrDefaultAsync();          
        
    }
}
