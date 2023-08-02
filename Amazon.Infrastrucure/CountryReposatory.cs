using Amazon.Application.Contracts;
using Amazon.Context;
using Amazon.Domain;
using Amazon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastrucure
{
    public class CountryReposatory : Reposatory<Country, int>, IcountryReposatory
    {
        public CountryReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
