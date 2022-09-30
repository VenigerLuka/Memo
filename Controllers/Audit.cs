using MemoProject.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Controllers
{
    public class Audit : Controller
    {
        private readonly IAuditService _auditService;
        private readonly UserManager<IdentityUser> _userManager;

        public Audit(IAuditService auditService, UserManager<IdentityUser> userManager)
        {
            _auditService = auditService;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetAudits()
        {
            var result = await _auditService.FetchAll();
            return View(result.Value);

        }
    }
}
