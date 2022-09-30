using MemoProject.Contracts;
using MemoProject.Models.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemoProject.Controllers
{
    public class Settings : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly UserManager<IdentityUser> _userManager;

        public Settings(ISettingsService settingsService, UserManager<IdentityUser> userManager)
        {
            _settingsService = settingsService;
            _userManager = userManager;
        }

        //GET: Memo
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _settingsService.GetUserSettings(user.Id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SettingViewModel data)
        {
            var user = await _userManager.GetUserAsync(User);
            await _settingsService.SetUserSettings(data, user.Id, Request);
           
            return RedirectToAction("Inxed", "Home"); 
        }
        
        public async Task<IActionResult> SetDefault()
        {
            var defSettings = await _settingsService.GetDefultSettingsAsync();
            return PartialView("_DefaultSettingsPartial", defSettings);
        }
        [HttpPost]
        public async Task<IActionResult> SetDefault(SettingViewModel data)
        {
            var user = await _userManager.GetUserAsync(User);
            var setting = await _settingsService.SetDefaultSettingAsync(data,user.UserName, Request);
            return RedirectToAction(nameof(Index));
        }
    }
}
