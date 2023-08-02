using Amazon.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryServices _icategoryServices;
        private readonly ISubcategoryServices _subcategoryServices;

        public CategoryController(IcategoryServices categoryServices,
            ISubcategoryServices subcategoryServices)
        {
            _icategoryServices = categoryServices;
            _subcategoryServices = subcategoryServices;
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> getAll()
        {
            //if (Request.Headers.TryGetValue("accept-language", out var lang))
            //{
            //    lang = lang.ToString();
            //}
            //if (lang == "en")
            //{
            //    var res = await _icategoryServices.GetAllCategory();
            //    return Ok(res);
            //}
            //else if(lang == "ar")
            //{
            //    var res = await _icategoryServices.GetAllCategoryInAR();
            //    return Ok(res);
            //}
            //else
            //{
            //    var res = await _icategoryServices.GetAllCategory();
            //    return Ok(res);
            //}
            var res = await _icategoryServices.GetAllCategory();
            return Ok(res);

        }

        [HttpGet]
        [Route("SubCategories")]
        public async Task<IActionResult> getSubCategory(int id)
        {
            //if (Request.Headers.TryGetValue("accept-language", out var lang))
            //{
            //    lang = lang.ToString();
            //}
            //if (lang == "en")
            //{
            //    var res = await _subcategoryServices.getSubCategoryByCatId(id);
            //    return Ok(res);
            //}else if(lang == "ar")
            //{
            //    var res = await _subcategoryServices.getSubCategoryByCatIdInAR(id);
            //    return Ok(res);
            //}
            //else
            //{
            //    var res = await _subcategoryServices.getSubCategoryByCatId(id);
            //    return Ok(res);
            //}
            var res = await _subcategoryServices.getSubCategoryByCatId(id);
            return Ok(res);

        }

        [HttpGet]
        [Route("AllSubCategories")]
        public async Task<IActionResult> getSubCategory()
        {
            //if (Request.Headers.TryGetValue("accept-language", out var lang))
            //{
            //    lang = lang.ToString();
            //}
            //if (lang == "en")
            //{
            //    var res = await _subcategoryServices.GetAllSubcategories();
            //    return Ok(res);
            //}
            //else if (lang == "ar")
            //{
            //    var res = await _subcategoryServices.GetAllSubcategoriesInAR();
            //    return Ok(res);
            //}
            //else
            //{
            //    var res = await _subcategoryServices.GetAllSubcategories();
            //    return Ok(res);
            //}
            var res = await _subcategoryServices.GetAllSubcategories();
            return Ok(res);
        }

    }
}
