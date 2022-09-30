using MemoProject.Contracts;
using MemoProject.Models.Result;
using MemoProject.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public Result<IEnumerable<IdentityUser>> FetchAll()
        {
            Result<IEnumerable<IdentityUser>> result = new();
            result.Value = _userManager.Users.ToList();
            result.Succedded = true;
            return result;
        }

        public async Task<Result<NoValue>> EditRole(string userId, string role)
        {
            Result<NoValue> result = new();
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, role);
            result.Succedded = true;
            return result;
        }
        


    }
}
