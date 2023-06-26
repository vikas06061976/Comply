using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
            IsActive = true;
        }


        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
    }
}
