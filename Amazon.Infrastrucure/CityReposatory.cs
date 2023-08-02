using Amazon.Application.Contracts;
using Amazon.Context;
using Amazon.Domain;
using Amazon.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastrucure
{
    public class CityReposatory : Reposatory<City, int>,ICityReposatory
    {
        private readonly ApplicationContext context;
        private readonly DbSet<City> dbset;
        public CityReposatory(ApplicationContext context) : base(context)
        {
            this.context = context;
            dbset = this.context.Set<City>();

        }

        public async Task<List<City>> GetAllByCountryId(int id)
        {

            return await dbset.Where(c=>c.CountryId== id).ToListAsync();
           
        }
    }
}
