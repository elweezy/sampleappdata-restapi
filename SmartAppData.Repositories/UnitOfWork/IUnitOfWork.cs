using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
