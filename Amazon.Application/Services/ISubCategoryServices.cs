using Amazon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
	public interface ISubcategoryServices
	{
		Task<List<SubCategoryDTO>> getSubCategoryByCatId(int id);
		Task<List<SubCategoryDTO>> GetAllSubcategories();

        Task<List<arsubcategory>> getSubCategoryByCatIdInAR(int id);
        Task<List<arsubcategory>> GetAllSubcategoriesInAR();
    }
}
