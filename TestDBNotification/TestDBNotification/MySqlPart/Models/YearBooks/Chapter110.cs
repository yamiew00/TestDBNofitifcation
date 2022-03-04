using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.YearBooks
{
    public partial class Chapter110
    {
        public string Uid { get; set; }
        public string BookId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentUid { get; set; }
        public string Maintainer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ExamType2 { get; set; }
        public string ExamType3 { get; set; }
        public string Remark { get; set; }
    }
}
