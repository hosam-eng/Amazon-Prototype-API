using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.DTO
{
	public class UserLoginDTO
	{
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}
