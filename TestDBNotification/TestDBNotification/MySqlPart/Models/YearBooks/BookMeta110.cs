using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.YearBooks
{
    public partial class BookMeta110
    {
        public string Uid { get; set; }
        public string BookId { get; set; }
        public string EduSubject { get; set; }
        public string MetaType { get; set; }
        public string MetaUid { get; set; }
        public string ParentUid { get; set; }
        public int Orderby { get; set; }
        public string Maintainer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
