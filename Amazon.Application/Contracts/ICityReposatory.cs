using Amazon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Contracts
{
    public interface ICityReposatory:IReposatory<City,int>
    {
		Task<List<City>> GetAllByCountryId(int id);
	}
}
