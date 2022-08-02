using MemoProject.Contracts;
using MemoProject.Models.EiditRole;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Controllers
{
    public class Admin : Controller
    {
        private readonly IAdminService _adminService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;

        public Admin(IAdminService adminService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole<string>> roleManager)
        {
            _adminService = adminService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var result = _adminService.FetchAll();
            if (result.Succedded)
                return View(result.Value);
            return Json(result.Message);

        }

        [HttpGet]
        public async Task<IActionResult>Edit(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id: {userId} was not found!";
                return RedirectToAction("Index", "Admin");
            }
            var model = new List<EditRoleViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var EditViewModel = new EditRoleViewModel
                {
                    Id = role.Id,
                    Role = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    EditViewModel.IsSelected = true;
                else
                    EditViewModel.IsSelected = false;
                model.Add(EditViewModel);                
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(List<EditRoleViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.Role));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index", "Admin");

        }
    }
}
