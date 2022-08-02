using MemoProject.Data;
using MemoProject.Helpers;
using MemoProject.Models.Memo;
using MemoProject.Models.Response;
using MemoProject.Models.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface IMemoService
    {
        Task<Result<CreateMemoViewModel>> Create(string userId, CreateMemoViewModel memoDTO);
        Task<Result<NoValue>> Delete(long id);
        Task<Result<IEnumerable<MemoViewModel>>> FetchAll();
        Task<Result<MemoViewModel>> FetchById(long id);
        Task<Result<MemoViewModel>> Update(MemoViewModel memoDTO);
    }
}