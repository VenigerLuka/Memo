using MemoProject.Contracts;
using MemoProject.Models.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            await _settingsService.SetUserSettings(data, user.Id);
           
            return RedirectToAction("Inxed", "Home"); 
        }
    }
}
