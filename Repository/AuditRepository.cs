using MemoProject.Contracts;
using MemoProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        public AuditRepository(MemoDbContext context) : base(context)
        {
        }
    }
}
