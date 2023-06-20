﻿using System;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.ContentBlock
{
    public class ContentManagementInsert
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string MoreText { get; set; }
        public string Language { get; set; }
        public string Translation { get; set; }
        public string ToolTip { get; set; }
        public ContentManagement TypeId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
