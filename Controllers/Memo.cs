using MemoProject.Contracts;
using MemoProject.Helpers;
using MemoProject.Models.Memo;
using MemoProject.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Controllers
{
    [Authorize]
    public class Memo : Controller
    {
        private readonly IMemoService _memoService;
        private readonly UserManager<IdentityUser> _userManager;


        public Memo(IMemoService memoService, UserManager<IdentityUser> userManager)
        {
            _memoService = memoService;
            _userManager = userManager;
        }







        //GET: Memo
        public async Task<IActionResult> Index()
        {
            var result = await _memoService.FetchAll();
            if (result.Succedded)
            {
                return View(result.Value);
            }

            return Json(result.Message);
        }



        // GET: Memo/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var result = await _memoService.FetchById(id);
            if (!result.Succedded)
            {
                return Json(result.Message);
            }

            return View(result.Succedded);
        }

        // GET: Memo/Create
        public IActionResult Create()
        {
            CreateMemoViewModel createModel = new();
            return PartialView("_MemoModalPartial", createModel);
        }

        // POST: Memo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemoViewModel memoViewModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var result = await _memoService.Create(currentUser.Id, memoViewModel);
                if (result.Succedded)
                    return RedirectToAction(nameof(Index));
                return Json(result.Message);
            }
            return View(memoViewModel);
        }

        // GET: Memo/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var result = await _memoService.FetchById(id);
            if (!result.Succedded)
            {
                return Json(result.Message);
            }
            return PartialView("_MemoEditModalPartial", result.Value);
        }



        // POST: Memo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Note,CreatedAt,StatusId,UserId,Tags")] MemoViewModel memoViewModel)
        {
            if (id != memoViewModel.Id)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);


            if (ModelState.IsValid)
            {
                try
                {
                    await _memoService.Update(memoViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {


                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(memoViewModel);
        }



        // GET: Memo/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _memoService.FetchById(id);
            if (!result.Succedded)
            {
                return Json(result.Message);
            }

            return View(result.Value);
        }

        // POST: Memo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var searchResult = await _memoService.FetchById(id);
            if (searchResult.Succedded)
            {
                var deleteResult = await _memoService.Delete(id);
                if (deleteResult.Succedded)
                    return RedirectToAction(nameof(Index));
                return Json(deleteResult.Message);
            }
            return Json(searchResult.Message);
        }
        [HttpPost]
        public async Task<JsonResult> GetFilteredItems()
        {
            var model = new PaginatedResponse
            {
                PageSize = Request.Form["length"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["length"].FirstOrDefault()) : 0,
                Skip = Request.Form["start"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["start"].FirstOrDefault()) : 0,
                SearchValue = Request.Form["search[value]"].FirstOrDefault(),
                SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][title]"].FirstOrDefault(),
                SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault()
            };
            var memos = await _memoService.GetDataAsync(model);

            return Json(new{
                            draw = Convert.ToInt32(Request.Form["draw"]),
                            recordsFiltered = memos.RecordsFiltered,
                            recordsTotal = memos.RecordsTotal,
                            data = memos.MemoList
                        }
                    );
        }

    }
}
