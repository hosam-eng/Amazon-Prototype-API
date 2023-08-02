using Amazon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public interface IImageService
    {
        Task<bool> uploadImage(ImageDTO img);
        Task<bool> UpdateImage(ImageDTO img, int id);
        Task<bool> deleteImage(int id);
    }
}
