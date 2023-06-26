using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Documentation
{
    public class DocumentationInsert
    {
        public string Name { get; set; }
        public int DocumentationTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
