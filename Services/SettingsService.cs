using MemoProject.Contracts;
using MemoProject.Data;
using MemoProject.Models.Setting;
using MemoProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MemoProject.Common.Enums;

namespace MemoProject.Services
{
    public class SettingsService : ServiceBase, ISettingsService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SettingsService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
            : base(unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<SettingViewModel> GetUserSettings(string userId)
        {

            Setting setting = await _unitOfWork.Setting.FindByUserIdAsync(userId);
            if (setting == null)
            {
                setting = new Setting();
                var settingsDTO = await _unitOfWork.DefaultSettings.FindById(1);
                setting.Culture = settingsDTO.Culture;
                setting.Zone = settingsDTO.Zone;
                setting.TimeFormat= settingsDTO.TimeFormat;
                setting.DateFormat= settingsDTO.DateFormat;
            }
            var settingDTO = new SettingViewModel()
            {
                Id = setting.Id,
                Zone = setting.Zone,
                Culture = setting.Culture,
                DateFormat = setting.DateFormat,
                TimeFormat = setting.TimeFormat
            };
            return settingDTO;
        }
        public async Task<SettingViewModel> SetUserSettings(SettingViewModel settingsData, string userId, HttpRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
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

            var userAgent = request.Headers["User-Agent"];
            var audit = new Audit();
            audit.CreatedAt = DateTime.UtcNow;
            audit.CreatedBy = user.UserName;
            audit.AuditEventId = (int)AuditEventEnum.ChangeUserSettings;
            audit.UserAgent = userAgent;
            audit.Value = JsonConvert.SerializeObject(setting);
            await _unitOfWork.Audit.CreateAsync(audit);

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
        public async Task<SettingViewModel> GetDefultSettingsAsync()
        {
            var auditTableInfo = await _unitOfWork.Audit.FindAll();

            var defaultSettingAudit = auditTableInfo
                .Where(entity => entity.AuditEventId == (int)AuditEventEnum.DefaultSettings)
                .FirstOrDefault();
            var auditWithSettings = defaultSettingAudit.Value;

            var defaultSettings = JsonConvert.DeserializeObject<DefaultSettings>(auditWithSettings);




            var settings = await _unitOfWork.DefaultSettings.FindById(1);
            var settingsDTO = new SettingViewModel()
            {
                Zone = defaultSettings.Zone,
                Culture = defaultSettings.Culture,
                DateFormat = defaultSettings.DateFormat,
                TimeFormat = defaultSettings.TimeFormat
            };
            return settingsDTO;
        }
        public async Task<SettingViewModel> SetDefaultSettingAsync(SettingViewModel defaultSettingsInfo, string userId, HttpRequest request)
        {
            var auditTableInfo = await _unitOfWork.Audit.FindAll();
            var userAgent = request.Headers["User-Agent"];

            var defaultSettingAudit = auditTableInfo
                .Where(entity => entity.AuditEventId == (int)AuditEventEnum.DefaultSettings)
                .FirstOrDefault();
            var auditWithSettings = defaultSettingAudit.Value;
            var defaultSettings = JsonConvert.DeserializeObject<DefaultSettings>(auditWithSettings);


            defaultSettings.Culture = defaultSettingsInfo.Culture;
            defaultSettings.Zone = defaultSettingsInfo.Zone;
            defaultSettings.TimeFormat = defaultSettingsInfo.TimeFormat;
            defaultSettings.DateFormat = defaultSettingsInfo.DateFormat;
            string settingsJSON = JsonConvert.SerializeObject(defaultSettings);
            defaultSettingAudit.Value = settingsJSON;
            _unitOfWork.Audit.Update(defaultSettingAudit);

            var audit = new Audit();
            audit.CreatedAt = DateTime.UtcNow;
            audit.CreatedBy = userId;
            audit.AuditEventId = (int)AuditEventEnum.ChangeDefaultSettings;
            audit.Value = settingsJSON;
            audit.UserAgent = userAgent;
            await _unitOfWork.Audit.CreateAsync(audit);
            await _unitOfWork.CommitAsync();
            return defaultSettingsInfo;
        }





    }
}
