using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.AgentUSSourceIncome
{
    public class USSourcedIncomeTypeSelectionViewModel
    {
        public int Id { get; set; } 
        public int USSourcedIncomeTypeId { get; set; }
        public int AgentId { get; set; }
        public string USSourcedIncomeQuestion { get; set; }
        public string QuestionText { get; set; }
        public bool state { get; set; } 
        public string USSourcedIncomeQuestionAlias { get; set; }
        public string QuestionTextAlias { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
