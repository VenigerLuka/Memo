using MemoProject.Models.Result;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface IAdminService
    {
        Task<Result<NoValue>> EditRole(string userId, string role);
        Result<IEnumerable<IdentityUser>> FetchAll();
    }
}