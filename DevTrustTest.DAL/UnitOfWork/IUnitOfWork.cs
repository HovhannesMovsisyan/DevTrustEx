using DevTrustTest.DAL.RepositoryInterfaces;
using System;

namespace DevTrustTest.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IPersonRepository Person { get; }
        int Save();
    }
}
