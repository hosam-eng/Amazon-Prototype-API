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
    public class CityService:ICityService
    {
        private readonly ICityReposatory _cityRepo;
        private readonly IMapper _mapper;

        public CityService(ICityReposatory cityRepo,IMapper mapper)
        {
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

     

        public async Task<List<CitiesListDTO>> GetCitiesByCountryId(int id)
        {
            List<City> cities = await _cityRepo.GetAllByCountryId(id);
            return _mapper.Map<List<CitiesListDTO>>(cities);
        }
    }
}
