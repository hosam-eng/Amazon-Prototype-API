using Amazon.Application.Contracts;
using Amazon.Context;
using Amazon.Domain;
using Amazon.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastrucure
{
    public class ImageReposatory : Reposatory<Image, int>, IImageReposatory
    {
        ApplicationContext Context;
        private readonly IConfiguration _configuration;
        public ImageReposatory(ApplicationContext context,IConfiguration configuration) : base(context)
        {
            Context = context;
            _configuration = configuration;
        }

        public  string GetImagesByCategoryId(int id)
        {
            var res = Context.Images.FirstOrDefault(p => p.categoryId == id)?.Name;
            return _configuration.GetSection("GetFilePath").Value+res;
        }

        public async Task<List<string>> GetImagesByPrdId(int id)
        {
            //var res = Context.Images.Where(p => p.ProductID == id).Select(i=>System.IO.File.ReadAllBytes(i.Name)).ToList();
            return Context.Images.Where(p=>p.ProductID==id).Select(i=> _configuration.GetSection("GetFilePath").Value+i.Name).ToList();
        }
    }
}
