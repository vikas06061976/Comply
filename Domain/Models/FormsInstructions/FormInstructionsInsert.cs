using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.FormInstructions
{
    public class FormInstructionsInsert
    {
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
