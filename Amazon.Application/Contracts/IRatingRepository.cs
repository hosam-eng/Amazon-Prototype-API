using Amazon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Contracts
{
    public interface IRatingRepository : IReposatory<Rating, int>
    {
        Task<List<Rating>> GetAllByProductIdAsync(int productId);
        Task<Dictionary<int, int>> calculateProductRate(int productId);
    }
}
