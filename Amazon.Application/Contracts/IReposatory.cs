using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Contracts
{
    public interface IReposatory<T,Tid>
    {
        Task<T> CreateAsync(T item);
        Task<T> GetByIdAsync(Tid id);
        Task<IQueryable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T item,Tid id);
        Task<bool> DeleteAsync(Tid id);
        Task<int> SaveChangesAsync();
    }
}
