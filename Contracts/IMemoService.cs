using MemoProject.Data;
using MemoProject.Helpers;
using MemoProject.Models.DataTable;
using MemoProject.Models.Memos;
using MemoProject.Models.Response;
using MemoProject.Models.Result;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface IMemoService
    {
        Task<Result<CreateMemoViewModel>> Create(string userId, CreateMemoViewModel memoDTO, HttpRequest request);
        Task<Result<NoValue>> Delete(long id, string userId, HttpRequest request);
        Task<Result<List<MemoViewModel>>> FetchAll(string userId);
        Task<Result<MemoViewModel>> FetchById(long id);
        Task<Result<MemoViewModel>> Update(string userId, MemoViewModel memoDTO, HttpRequest request);
        
        Task<DataTableModel> GetDataAsync(PaginatedResponse settings, string userId);
    }
}