using System;

namespace Infrastructure.Application.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}