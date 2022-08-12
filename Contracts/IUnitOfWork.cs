using MemoProject.Contracts;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public interface IUnitOfWork
    {
        IMemoRepository Memo { get; }
        ITagRepository Tag { get; }

        IAdminRepository Admin { get; }

        Task CommitAsync();
    }
}