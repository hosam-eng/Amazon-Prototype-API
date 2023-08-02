using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.DTO
{
    public class ArShowproduct
    {
        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "يجب ان لا يقل الاسم عن 5 حروف")]
        public String arabicName { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public int UnitInStock { get; set; }
        [MinLength(5, ErrorMessage = "يجب ان لا يقل الاسم عن 5 حروف")]
        public string arabicDescription { get; set; }
        public List<string> images { get; set; }
        public int CategoryId { get; set; }

    }
}
