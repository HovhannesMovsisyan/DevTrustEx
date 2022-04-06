using DevTrustTest.DAL.Repositories;
using DevTrustTest.DAL.RepositoryInterfaces;
using System;

namespace DevTrustTest.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private MyTestContext _context;
        private PersonRepository _personRepository;
        public UnitOfWork(MyTestContext context) => _context = context;
        public IPersonRepository Person
        {
            get
            {
                if (_personRepository == null)
                    _personRepository = new PersonRepository(_context);
                return _personRepository;
            }
        }

        public void Dispose() => _context.Dispose();
        public int Save() => _context.SaveChanges();
    }
}
