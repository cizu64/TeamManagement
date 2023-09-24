using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
