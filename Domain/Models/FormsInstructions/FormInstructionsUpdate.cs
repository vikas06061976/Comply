using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.FormInstructions
{
    public class FormInstructionsUpdate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
