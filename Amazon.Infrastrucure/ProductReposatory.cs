using Amazon.Application.Contracts;
using Amazon.Context;
using Amazon.Domain;
using Amazon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure
{
    public class ProductReposatory : Reposatory<Product, int>, IProductReposatory
    {
        public ProductReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
