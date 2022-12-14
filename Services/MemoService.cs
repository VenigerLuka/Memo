using AutoMapper;
using MemoProject.Contracts;
using MemoProject.Data;
using MemoProject.Helpers;
using MemoProject.Models.Memo;
using MemoProject.Models.Result;
using MemoProject.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MemoProject.Common.Enums;

namespace MemoProject.Services
{

    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly ILogger<Memo> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public MemoService(IUnitOfWork unitofWork, IMapper mapper, ILogger<Memo> logger,  UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<Result<NoValue>> Delete(long id)
        {
            Result<NoValue> result = new();
            try
            {

                var toDelete = await _unitofWork.Memo.FindById(id);
                if (toDelete is null)
                {
                    result.Succedded = false;
                    result.Message = "Memo not found";
                    result.Value = null;
                    return result;
                }
                _unitofWork.Memo.DeleteByID(id);
                result.Succedded = true;
                result.Message = "Deleted successfuly";
                result.Value = null;
                await _unitofWork.CommitAsync();
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

        public async Task<Result<IEnumerable<MemoViewModel>>> FetchAll()
        {
            Result<IEnumerable<MemoViewModel>> result = new();
            try
            {
                var memos = await _unitofWork.Memo.FindAll();
                var memoList = _mapper.Map<IEnumerable<MemoViewModel>>(memos);
                result.Succedded = true;
                result.Message = "Success";
                result.Value = memoList;
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

                var search = await _unitofWork.Memo.FindById(id);
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

        public async Task<Result<MemoViewModel>> Update(MemoViewModel memoDTO)
        {
            
            Result<MemoViewModel> result = new();
            try
            {

                Memo editMemo = new()
                {
                    Id = memoDTO.Id,
                    Title = memoDTO.Title,
                    Note = memoDTO.Note,                    
                    UserId = memoDTO.UserId,
                    CreatedAt = memoDTO.CreatedAt,
                    StatusId = memoDTO.StatusId
                };
                var tags = memoDTO.Tags;
                var tagsList = tags.Split(' ');
                

                foreach (var tagItem in tagsList)
                {
                    Tag tag = new();
                    tag.Name = tagItem;
                    tag.Memo = editMemo;
                    await _unitofWork.Tag.CreateAsync(tag);
                }



                _unitofWork.Memo.Update(editMemo);
                await _unitofWork.CommitAsync();
                var editedCategory = _mapper.Map(editMemo, new MemoViewModel());
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

        public async Task<Result<CreateMemoViewModel>> Create(string userId, CreateMemoViewModel memoDTO)
        {
            Result<CreateMemoViewModel> result = new();
            
            try
            {
                
                Memo memo = new();
                memo.UserId = userId;
                memo.StatusId = (int)StatusEnum.Active;
                memo.CreatedAt = DateTime.UtcNow;
                var tags = memoDTO.Tags;
                var tagsList = tags.Split(' ');
                memo.Title = memoDTO.Title;
                memo.Note = memoDTO.Note;
                
                foreach(var tagItem in tagsList)
                {
                    Tag tag = new();
                    tag.Name = tagItem;
                    tag.Memo = memo;
                    await _unitofWork.Tag.CreateAsync(tag);
                }

                //memo = _mapper.Map<Memo>(memoDTO);


                await _unitofWork.Memo.CreateAsync(memo);
                await _unitofWork.CommitAsync();
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

    }


}
