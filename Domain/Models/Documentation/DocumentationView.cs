using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Documentation
{
    public class DocumentationView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
