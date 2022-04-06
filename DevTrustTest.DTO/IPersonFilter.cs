using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTrustTest.DTO
{
    public interface IFilter<T> where T : class
    {
        IQueryable<T> Filter(IQueryable<T> query);
    }
}
