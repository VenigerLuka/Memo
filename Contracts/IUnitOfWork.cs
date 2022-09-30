using MemoProject.Contracts;
using System.Threading.Tasks;

namespace MemoProject.Repository
{
    public interface IUnitOfWork
    {
        IMemoRepository Memo { get; }
        ITagRepository Tag { get; }
        ISettingsReposirory Setting { get; }
        IDeafultSettingsRepository DefaultSettings { get; }
        IAuditRepository Audit { get; }

        Task CommitAsync();
    }
}