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
    public class ImageServices : IImageService
    {
        private readonly IImageReposatory _imagerepo;
        private readonly IMapper _Mapper;
        public ImageServices(IImageReposatory imagerepo, IMapper mapper)
        {
            _imagerepo = imagerepo;
            _Mapper = mapper;
        }
        public async Task<bool> deleteImage(int id)
        {
            var res= await _imagerepo.DeleteAsync(id);
            if (res)
            {
                await _imagerepo.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<bool> UpdateImage(ImageDTO img, int id)
        {
            Image image = _Mapper.Map<Image>(img);
            image.Id = id;
            var res=await _imagerepo.UpdateAsync(image,id);
            if (res)
            {
                await _imagerepo.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<bool> uploadImage(ImageDTO img)
        {
            var res=await _imagerepo.CreateAsync(_Mapper.Map<Image>(img));
            if(res != null)
            {
                await _imagerepo.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}
