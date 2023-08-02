using Amazon.Application.Contracts;
using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public class ShippingAddressServices : IShippingAddressServices
    {
        private readonly IShippingAddressRepository repository;
        private readonly IMapper mapper;

        public ShippingAddressServices( IShippingAddressRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<AddAndEditShippingAddressDTO> AddShippingAddress(AddAndEditShippingAddressDTO addAndEditShippingAddressDTO)
        {
            shippingAddress shippingAddress = mapper.Map<shippingAddress>(addAndEditShippingAddressDTO);
            var res = await repository.CreateAsync(shippingAddress);
           return mapper.Map<AddAndEditShippingAddressDTO>(res);

        }

        public async Task<ShippingAddressDto> GetAddressForUserById(string id)
        {
            var res= await repository.GetAddressForUserById(id);
            ShippingAddressDto shippingAddress=new ShippingAddressDto();
            shippingAddress.id = res.id;
            shippingAddress.userid = res.userid;
            shippingAddress.Name = res.Name;
            shippingAddress.buildname = res.buildname;
            shippingAddress.street = res.street;
            shippingAddress.cityname = res.City.Name;
            shippingAddress.Phone = res.Phone;
            return shippingAddress;
        }
    }
}
