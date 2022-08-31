using MemoProject.Contracts;
using MemoProject.Data;
using MemoProject.Models.Setting;
using MemoProject.Repository;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Services
{
    public class SettingsService : ServiceBase, ISettingsService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SettingsService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
            :base(unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<SettingViewModel> GetUserSettings(string userId)
        {

            Setting setting = await _unitOfWork.Setting.FindByUserIdAsync(userId);
            if (setting == null)
            {
                var newSetting = new SettingViewModel();
                newSetting.Culture = "en";
                newSetting.DateFormat= "dd/MM/yyyy";
                newSetting.TimeFormat= "HH:MM:ss";
                newSetting.Zone = "WorldWideWeb";
                await SetUserSettings(newSetting, userId);                               

            }
            var settingDTO = new SettingViewModel
            {
                Id = setting.Id,
                Zone = setting.Zone,
                Culture = setting.Culture,
                DateFormat = setting.DateFormat,
                TimeFormat = setting.TimeFormat
            };
            return settingDTO;
        }
        public async Task<SettingViewModel> SetUserSettings(SettingViewModel settingsData, string userId)
        {             
            Setting setting = new()
            {
                Id = settingsData.Id,
                Zone = settingsData.Zone,
                Culture = settingsData.Culture,
                TimeFormat = settingsData.TimeFormat,
                DateFormat = settingsData.DateFormat,
                UserId = userId

            };
            _unitOfWork.Setting.Update(setting);
            await _unitOfWork.CommitAsync();
            SettingViewModel settingDTO = new()
            {
                Id = setting.Id,
                Zone = setting.Zone,
                Culture = setting.Culture,
                TimeFormat = setting.TimeFormat,
                DateFormat = setting.DateFormat
            };

            return settingDTO;
        }





    }
}
