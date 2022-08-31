using MemoProject.Data;
using MemoProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Services
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<Setting> GetSettingByUserIdAsync(string userId)
        {
            var setting = await _unitOfWork.Setting.FindByUserIdAsync(userId);
            if (setting == null)
            {
                throw  new ArgumentException("No user setting found!", nameof(userId));
            }
            return setting;

        }



    }
}
