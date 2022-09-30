using MemoProject.Models.Audit;
using MemoProject.Models.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface IAuditService
    {
        Task<Result<IEnumerable<AuditViewModel>>> FetchAll();
    }
}