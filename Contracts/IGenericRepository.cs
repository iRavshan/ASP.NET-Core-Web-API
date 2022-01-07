using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid id);
    }
}
