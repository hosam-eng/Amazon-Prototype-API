using Amazon.Application.Services;
using Amazon.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressServices shippingAddressServices;
        private readonly ICityService cityService;
        private readonly ICountryServices countryServices;

        public ShippingAddressController(IShippingAddressServices shippingAddressServices,
            ICityService cityService,ICountryServices countryServices)
        {
            this.shippingAddressServices = shippingAddressServices;
            this.cityService = cityService;
            this.countryServices = countryServices;
        }
        [HttpGet]
        [Route("AllCitiesByCountryId")]
        public async Task<IActionResult> AllCittes(int id)
        {
            List<CitiesListDTO> citiesLists = await cityService.GetCitiesByCountryId(id);
                
            return Ok(citiesLists);
        }
        [HttpGet]
        [Route("AllCountries")]
        public async Task<IActionResult> AllCountries()
        {
            List<CountryDTO> countries = await countryServices.GetAll();

            return Ok(countries);
        }
        [HttpPost]
        [Route("CreateShippingAddress")]
        public async Task<IActionResult> Create(AddAndEditShippingAddressDTO shippingAddressDTO)
        {
            var res = await shippingAddressServices.AddShippingAddress(shippingAddressDTO);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetAddressByUserId")]
        public async Task<IActionResult>GetAddressByUserId(string id)
        {
            var res=await shippingAddressServices.GetAddressForUserById(id);
            return Ok(res);

        }
    }
}
