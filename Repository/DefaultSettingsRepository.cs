using MemoProject.Contracts;
using MemoProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class DefaultSettingsRepository : BaseRepository<DefaultSettings>, IDeafultSettingsRepository
    {
        public DefaultSettingsRepository(MemoDbContext context) : base(context)
        {
        }
    }
}
