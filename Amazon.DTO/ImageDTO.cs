﻿using Amazon.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        [RegularExpression(@"\w+\.(jpg|png|gif)")]
        public string Name { get; set; }
        public int? ProductID { get; set; }
        public int? categoryId { get; set; }
    }
}
