using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.DTO
{
    public class arsubcategory
    {
        public int Id { get; set; }
        [MinLength(5, ErrorMessage = "يجب ان لا يقل الاسم عن 5 حروف")]
        public string arabicName { get; set; }

        public int? categoryId { get; set; }

        public string? imageName { get; set; }
    }
}
