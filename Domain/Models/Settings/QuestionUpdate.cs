using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Settings
{
    public class QuestionUpdate                                                                 
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionHint { get; set; }
        public int QuestionHintId { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
