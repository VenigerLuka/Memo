using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public interface IAdminRepository
    {
        Task<IdentityUser> FindById(string id);
        IEnumerable<IdentityUser> FindAll();
    }
}