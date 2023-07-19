using System;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.ContentBlock
{
    public class ContentManagementUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
