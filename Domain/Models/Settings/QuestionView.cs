using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Settings
{
    public class QuestionView
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionHint { get; set; }

    }
}
