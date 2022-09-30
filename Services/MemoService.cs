using AutoMapper;
using MemoProject.Contracts;
using MemoProject.Data;
using MemoProject.Models.DataTable;
using MemoProject.Models.Memos;
using MemoProject.Models.Response;
using MemoProject.Models.Result;
using MemoProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MemoProject.Common.Enums;
using Memo = MemoProject.Data.Memo;

namespace MemoProject.Services
{

    public class MemoService : ServiceBase, IMemoService
    {
        
        private readonly IMapper _mapper;
        private readonly ILogger<Memo> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public MemoService(IUnitOfWork unitofWork, IMapper mapper, ILogger<Memo> logger, UserManager<IdentityUser> userManager)
        :base(unitofWork)
        {
           
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<Result<NoValue>> Delete(long id, string userId, HttpRequest request)
        {
            Result<NoValue> result = new();
            try
            {

                var toDelete = await _unitOfWork.Memo.FindById(id);
                if (toDelete is null)
                {
                    result.Succedded = false;
                    result.Message = "Memo not found";
                    result.Value = null;
                    return result;
                }
                
                var user = await _userManager.FindByIdAsync(userId);
                var userAgent = request.Headers["User-Agent"];
                var audit = new Audit();
                audit.CreatedAt = DateTime.UtcNow;
                audit.CreatedBy = user.UserName;
                audit.AuditEventId = (int)AuditEventEnum.MemoDeleted;
                audit.UserAgent = userAgent;
                audit.Value = JsonConvert.SerializeObject(toDelete);
                await _unitOfWork.Audit.CreateAsync(audit);

                await _unitOfWork.Memo.DeleteByID(id);
                result.Succedded = true;
                result.Message = "Deleted successfuly";
                result.Value = null;
                await _unitOfWork.CommitAsync();
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"recieved ID: {id}");
                _logger.LogError(ex.GetBaseException().Message);
                result.Message = "Something went wrong";
                result.Succedded = false;
                return result;

            }
        }

        public async Task<Result<List<MemoViewModel>>> FetchAll(string userId)
        {
            Setting setting = await GetSettingByUserIdAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);
            Result<List<MemoViewModel>> result = new();
            List<MemoViewModel> memoVMs = new();
            try
            {
                List<Memo> memos = await _unitOfWork.Memo.FindAll();
                if (await _userManager.IsInRoleAsync(user, "Admin") == true)
                {                
                    memoVMs = memos
                    .Select(memo => new MemoViewModel(memo, setting.DateFormat, setting.TimeFormat))
                    .ToList();
                }
                else
                {
                    memoVMs = memos
                   .Where(memo => memo.UserId == userId)
                   .Select(memo => new MemoViewModel(memo, setting.DateFormat, setting.TimeFormat))
                   .ToList();
                }
                result.Succedded = true;
                result.Message = "Success";
                result.Value = memoVMs;
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
                result.Message = "Something went wrong";
                result.Succedded = false;
                return result;

            }
        }

        public async Task<Result<MemoViewModel>> FetchById(long id)
        {
            Result<MemoViewModel> result = new();
            try
            {

                var search = await _unitOfWork.Memo.FindById(id);
                if (search == null)
                {
                    result.Succedded = false;
                    result.Message = "Memo not found!";
                    return result;
                }
                var memoDTO = _mapper.Map<MemoViewModel>(search);
                result.Value = memoDTO;
                result.Message = "Sucess";
                result.Succedded = true;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Recieved id: {id}");
                _logger.LogError(ex.GetBaseException().Message);
                result.Message = "Something went wrong!";
                result.Succedded = false;
                return result;
            }

        }

        public async Task<Result<MemoViewModel>> Update(string userId, MemoViewModel memoDTO,HttpRequest request)
        {

            Result<MemoViewModel> result = new();
            try
            {
                Setting setting = await GetSettingByUserIdAsync(userId);

                var memo = await _unitOfWork.Memo.FindById(memoDTO.Id);
                memo.Update(memoDTO.Note, memoDTO.Title);
                await _unitOfWork.Tag.DeleteByMemoAsync(memo.Id);
                var tags = memoDTO.Tags.Split(' ');                 
                await _unitOfWork.Tag.AddRange(tags,memo);
                var editedCategory = new MemoViewModel(memo,setting.DateFormat, setting.TimeFormat);
                editedCategory.Tags = memoDTO.Tags;
                
                var user = await _userManager.FindByIdAsync(userId);
                var userAgent = request.Headers["User-Agent"];
                var audit = new Audit();
                audit.CreatedAt = DateTime.UtcNow;
                audit.CreatedBy = user.UserName;
                audit.AuditEventId = (int)AuditEventEnum.MemoEdited;
                audit.UserAgent = userAgent;
                audit.Value = JsonConvert.SerializeObject(editedCategory);
                await _unitOfWork.Audit.CreateAsync(audit);

                await _unitOfWork.CommitAsync();
                
                result.Value = editedCategory;
                result.Message = "Update Successful!";
                result.Succedded = true;
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
                result.Value = null;
                result.Message = "Something went wrong!";
                result.Succedded = false;
                return result;
            }
        }

        public async Task<Result<CreateMemoViewModel>> Create(string userId, CreateMemoViewModel memoDTO, HttpRequest request)
        {
            Result<CreateMemoViewModel> result = new();
            try
            {
                var setting =await  GetSettingByUserIdAsync(userId);


                Memo memo = new();
                memo.UserId = userId;
                memo.StatusId = (int)StatusEnum.Active;
                memo.CreatedAt = DateTime.UtcNow;
                memo.Title = memoDTO.Title;
                memo.Note = memoDTO.Note;


                var tagsList = memoDTO.Tags.Split(' ');
                await _unitOfWork.Tag.AddRange(tagsList.ToList(), memo);

                var memoVM = new MemoViewModel(memo, setting.DateFormat, setting.TimeFormat);
                memoVM.Tags = memoDTO.Tags;


                var user = await _userManager.FindByIdAsync(userId);
                var userAgent = request.Headers["User-Agent"];
                var audit = new Audit();
                audit.CreatedAt = DateTime.UtcNow;
                audit.CreatedBy = user.UserName;
                audit.AuditEventId = (int)AuditEventEnum.MemoCreated;
                audit.UserAgent = userAgent;
                audit.Value = JsonConvert.SerializeObject(memoVM);
                await _unitOfWork.Audit.CreateAsync(audit);

                //memo = _mapper.Map<Memo>(memoDTO);


                //await _unitOfWork.Memo.CreateAsync(memo);
                await _unitOfWork.CommitAsync();
                result.Message = "Created Successfuly";

                //CreateMemoViewModel newMemoDto = new();
                //newMemoDto = _mapper.Map(memo, newMemoDto);
                //result.Value = newMemoDto;

                result.Succedded = true;
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Memo title: {memoDTO.Title}");
                _logger.LogError(ex.GetBaseException().Message);
                result.Message = Errors.SomethingWentWrong.ToString();
                result.Succedded = false;
                return result;
            }
        }
        


        public async Task<DataTableModel> GetDataAsync(PaginatedResponse settings, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            Setting setting = await GetSettingByUserIdAsync(userId);
            var results = await _unitOfWork.Memo.FindAll();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(setting.Zone);

            foreach(var item in results)
            {
                item.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(item.CreatedAt, timeZoneInfo);
            }


            var memoVMs = new List<MemoViewModel>();
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var perUserResults = results
                    .Where(memo => memo.UserId == userId);
                
                memoVMs = perUserResults
                    .Select(memo => new MemoViewModel(memo, setting.DateFormat, setting.TimeFormat))
                    .ToList();
            }
            else
            {
                memoVMs = results
                .Select(memo => new MemoViewModel(memo, setting.DateFormat, setting.TimeFormat))
                .ToList();
            }


            
            

            // Total count matching search criteria 

            // Total Records Count
            var recordsTotalCount = memoVMs.Count();

            // Filtered & Sorted & Paged data to be sent from server to view
            List<MemoViewModel> filteredData = null;
            if (settings.SortColumnDirection == "asc")
            {

                filteredData =
                     memoVMs
                    .Where(a => a.Title.Contains(settings.SearchValue) || a.Note.Contains(settings.SearchValue))
                    .OrderBy(a => a.Title)//Sort by sortColumn
                    .Skip(settings.Skip)
                    .Take(settings.PageSize)
                    .ToList();


            }
            else
            {
                filteredData =
                    memoVMs
                   .Where(a => a.Title.Contains(settings.SearchValue) || a.Note.Contains(settings.SearchValue))
                   .OrderByDescending(x => x.Title)
                   .Skip(settings.Skip)
                   .Take(settings.PageSize)
                   .ToList();
            }
            /*
            var viewModelList = new List<MemoViewModel>();
            var memoDto = new MemoViewModel();
            if (filteredData != null)
            {
                foreach (var memo in filteredData)
                {
                    viewModelList.Add(memo);
                }
            }
            else
            {
                foreach (var memo in resultsDTO)
                {
                    viewModelList.Add(memo);
                }
            }
            */
            int recordsFilteredCount =
                    memoVMs
                    .Where(a => a.Title.Contains(settings.SearchValue) || a.Note.Contains(settings.SearchValue))
                    .Count();
            var dataTableParams = new DataTableModel();

            dataTableParams.MemoList = filteredData;
            dataTableParams.RecordsFiltered = recordsFilteredCount;
            dataTableParams.RecordsTotal = recordsTotalCount;

            return dataTableParams;

        }


    }


}
