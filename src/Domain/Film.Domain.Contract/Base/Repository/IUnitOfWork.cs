using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Domain.Contract.Base.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        void BeginTransaction();
        Task<int> CommitAsync();
        int Commit();

    }
}
