using System;

namespace Infrastructure.Application.Core.Services.Business
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}