using Amazon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Contracts
{
    public interface IImageReposatory:IReposatory<Image,int>
    {
		public Task<List<String>> GetImagesByPrdId(int id);
        public string GetImagesByCategoryId(int id);
    }
}
