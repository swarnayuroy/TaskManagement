using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Service
{
    public interface IService<T> where T : class
    {
        Task<IList<T>> Get();
        Task<T> GetById(Guid id);
    }
}
