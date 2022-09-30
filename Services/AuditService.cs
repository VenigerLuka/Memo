using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoProject.Contracts;
using MemoProject.Data;
using MemoProject.Models.Audit;
using MemoProject.Models.Result;
using MemoProject.Repository;
using Microsoft.AspNetCore.Identity;

namespace MemoProject.Services
{
    public class AuditService : ServiceBase, IAuditService
    {
        private readonly UserManager<IdentityUser> _userManager;

       

        public AuditService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<AuditViewModel>>> FetchAll()
        {
            Result<IEnumerable<AuditViewModel>> result = new();
            var audits = await _unitOfWork.Audit.FindAll();
            result.Value = audits
                .Select(audit => new AuditViewModel(audit.Id,audit.AuditEventId,audit.Value,audit.UserAgent,audit.CreatedBy,audit.CreatedAt))
                .ToList();
            result.Succedded = true;
            return result;
        }

    }
}
