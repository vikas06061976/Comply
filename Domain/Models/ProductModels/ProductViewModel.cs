﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.ProductModels
{
    public class ProductViewModel : BaseModel
    {
        public ProductViewModel()
        {

        }
        public Guid Id { get; set; } 
        public string Name { get; set; } 

    }
}
