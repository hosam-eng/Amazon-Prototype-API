using Amazon.Application.Contracts;
using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<UserProfileDTO> getUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return mapper.Map<UserProfileDTO>(user);
        }
        public async Task<bool> UpdateAsync(string id,UserProfileDTO userProfileDTO)
        {
            var user =await userManager.FindByIdAsync(id);
            user.Phone = userProfileDTO.Phone;
            user.EmailAddress = userProfileDTO.EmailAddress;
            user.UserName = userProfileDTO.userName;
            var res= await userManager.UpdateAsync(user);
            return res!=null? true : false;
        }
    }
}
