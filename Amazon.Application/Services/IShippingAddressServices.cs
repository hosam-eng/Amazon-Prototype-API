using Amazon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public interface IShippingAddressServices
    {
      public Task<AddAndEditShippingAddressDTO> AddShippingAddress(AddAndEditShippingAddressDTO addAndEditShippingAddressDTO);
        public Task<ShippingAddressDto> GetAddressForUserById(string id);

    }
}
