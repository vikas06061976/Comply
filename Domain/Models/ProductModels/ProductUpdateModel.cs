using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.ProductModels
{
    public class ProductUpdateModel
    {
        public ProductUpdateModel()
        {
    
        }
        public Guid Id { get; set; } 
        public string Name { get; set; }        

    }
}
