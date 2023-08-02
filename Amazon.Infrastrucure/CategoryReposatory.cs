using Amazon.Application.Contracts;
using Amazon.Context;
using Amazon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure
{
	public class CategoryReposatory : Reposatory<Category, int>, ICategoryReposatory
	{
		public CategoryReposatory(ApplicationContext context) : base(context)
		{
		}
	}
}
