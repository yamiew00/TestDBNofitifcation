using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.Resources
{
    public partial class Definition
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public sbyte? Orderby { get; set; }
        public string Name { get; set; }
        public string Maintainer { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ParentType { get; set; }
        public string Remark { get; set; }
        public string SystemRemark { get; set; }
    }
}
