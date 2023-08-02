using Amazon.Application.Services;
using Amazon.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _prodservices;
        public ProductController(IProductServices prodservices)
        {
            _prodservices = prodservices;
        }
        [HttpGet]
        [Route("ProductsPagination")]
        public async Task<IActionResult> GetProductsPagination(int pagenumber=1,int items=3)
        {
            List<ShowProductDTO> products =await _prodservices.
                ShowProductsPagination(pagenumber, items);
            return Ok(products);
        }
        [HttpGet]
        [Route("AllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            List<ShowProductDTO> products = await _prodservices.GetAllProducts();
            return Ok(products);
        }
        [HttpGet]
        [Route("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(int categoryid)
        {
            List<ShowProductDTO> products = await _prodservices.GetProductsByCategoryId(categoryid);
            return Ok(products);
        }
        [HttpGet]
        [Route("FillterByPrice")]
        public async Task<IActionResult> GetProductsPrice(int catid,decimal minprice,decimal maxprice)
        {
            List<ShowProductDTO> products = await _prodservices.FilterByPrice(catid,minprice,maxprice);
            return Ok(products);
        }
        [HttpGet]
        [Route("SerchByName")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            List<ShowProductDTO> products = await _prodservices.SearchByProductName(name);
            return Ok(products);
        }

        [HttpGet]
        [Route("SerchByArabicName")]
        public async Task<IActionResult> GetProductsByArName(string Arname="بنطلون")
        {
            List<ShowProductDTO> products = await _prodservices.SearchByArProductName(Arname);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetProdById")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            ShowProductDTO products = await _prodservices.GetProductsById(id);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetMinMaxPrice")]
        public async Task<IActionResult> GetPriceRange(string lang,int categoryid)
        {
            
            PriceDTO prices = await _prodservices.GetPriceCategoryId(categoryid);
            return Ok(prices);
        }

        [HttpGet]
        [Route("FillterByPriceMax")]
        public async Task<IActionResult> GetmaxPriceProducts(int catid,decimal maxprice)
        {
            //string lang;
            if(Request.Headers.TryGetValue("accept-language",out var lang))
            {
                lang = lang.ToString();
            }
            List<ShowProductDTO> products = await _prodservices.GetProductsWithMaxPriceFillter(catid, maxprice);
            return Ok(products);
        }

    }
}
