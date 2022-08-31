using MemoProject.Models.Setting;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface ISettingsService
    {
        Task<SettingViewModel> GetUserSettings(string userId);
        Task<SettingViewModel> SetUserSettings(SettingViewModel settingsData, string userid);
    }
}