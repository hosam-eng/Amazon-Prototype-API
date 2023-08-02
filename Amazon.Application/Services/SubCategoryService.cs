using Amazon.Application.Contracts;
using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
	public class SubCategoryService : ISubcategoryServices
    {
		ICategoryReposatory _Repo;
		IImageReposatory _imageReposatory;
		private readonly IMapper mapper;
        private readonly IConfiguration _configure;
		public SubCategoryService(ICategoryReposatory repo, IConfiguration configuration,IMapper mapper,IImageReposatory imageReposatory)
		{
			_Repo = repo;
			this.mapper = mapper;
			this._imageReposatory = imageReposatory;
            _configure= configuration;
		}

        public async Task<List<SubCategoryDTO>> getSubCategoryByCatId(int id)
        {
            var subCategories= (await _Repo.GetAllAsync())
				.Where(c=>c.categoryId==id).ToList();
			var list = mapper.Map<List<SubCategoryDTO>>(subCategories);

            foreach (var item in list)
			{
				var res =  _imageReposatory.GetImagesByCategoryId(item.Id);
                if(res != null)
				{
                    item.imageName = res;
                }
			}

			return list;
        }

        public async Task<List<SubCategoryDTO>> GetAllSubcategories()
        {
            var subCategories = (await _Repo.GetAllAsync())
                .Where(c => c.categoryId != null).ToList();
            var list = mapper.Map<List<SubCategoryDTO>>(subCategories);

            if(list != null)
            {
                foreach (var item in list)
                {
                    var res = _imageReposatory.GetImagesByCategoryId(item.Id);
                    if (res != null)
                    {
                        item.imageName = res;
                    }
                }
            }
            return list;
        }

        public async Task<List<arsubcategory>> getSubCategoryByCatIdInAR(int id)
        {
            var subCategories = (await _Repo.GetAllAsync())
                   .Where(c => c.categoryId == id).ToList();
            var list = mapper.Map<List<arsubcategory>>(subCategories);

            foreach (var item in list)
            {
                var res = _imageReposatory.GetImagesByCategoryId(item.Id);
                if (res != null)
                {
                    item.imageName = res;
                }
            }

            return list;
        }

        public async Task<List<arsubcategory>> GetAllSubcategoriesInAR()
        {
            var subCategories = (await _Repo.GetAllAsync())
                 .Where(c => c.categoryId != null).ToList();
            var list = mapper.Map<List<arsubcategory>>(subCategories);

            if (list != null)
            {
                foreach (var item in list)
                {
                    var res = _imageReposatory.GetImagesByCategoryId(item.Id);
                    if (res != null)
                    {
                        item.imageName = res;
                    }
                }
            }
            return list;
        }
    }
}
