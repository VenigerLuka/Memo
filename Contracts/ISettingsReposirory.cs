using MemoProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface ISettingsReposirory : IBaseRepository<Setting>
    {
        Task<Setting> FindByUserIdAsync(string userId);
    }
}
