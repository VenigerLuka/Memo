using MemoProject.Models.Setting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface ISettingsService
    {
        Task<SettingViewModel> GetUserSettings(string userId);
        Task<SettingViewModel> SetUserSettings(SettingViewModel settingsData, string userid, HttpRequest request);
        Task<SettingViewModel> GetDefultSettingsAsync();
        Task<SettingViewModel> SetDefaultSettingAsync(SettingViewModel defaultSetttingsInfo, string userId, HttpRequest request);
    }
}