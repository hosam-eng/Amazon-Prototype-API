using Amazon.Application.Contracts;
using Amazon.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
	public class CategoryService : IcategoryServices
	{
		ICategoryReposatory _Repo;
		private readonly IMapper mapper;
		public CategoryService(ICategoryReposatory repo, IMapper mapper)
		{
			_Repo = repo;
			this.mapper = mapper;
		}

		public async Task<List<CategoryDTO>> GetAllCategory()
		{
			var categories= (await _Repo.GetAllAsync())
				.Where(c => c.categoryId == null).ToList();
			return mapper.Map<List<CategoryDTO>>(categories);
		}

		public async Task<CategoryDTO> GetByIdAsync(int ID)
		{
			var category = await _Repo.GetByIdAsync(ID);
			return mapper.Map<CategoryDTO>(category);
		}

        public async Task<List<arCategoryDTO>> GetAllCategoryInAR()
        {
            var categories = (await _Repo.GetAllAsync())
                     .Where(c => c.categoryId == null).ToList();
            return mapper.Map<List<arCategoryDTO>>(categories);
        }

        public async Task<arCategoryDTO> GetByIdAsyncInAR(int ID)
        {
            var category = await _Repo.GetByIdAsync(ID);
            return mapper.Map<arCategoryDTO>(category);
        }
    }
}
