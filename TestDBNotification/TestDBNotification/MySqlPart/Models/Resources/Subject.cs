using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.Resources
{
    public partial class Subject
    {
        public string Code { get; set; }
        public string EduCode { get; set; }
        public string Grade { get; set; }
        public string ParentCode { get; set; }
        public string Attribute { get; set; }
        public sbyte? Orderby { get; set; }
        public string Maintainer { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
