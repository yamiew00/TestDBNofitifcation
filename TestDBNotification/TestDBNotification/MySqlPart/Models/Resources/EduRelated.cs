using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.Resources
{
    public partial class EduRelated
    {
        public string Uid { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string EduCode { get; set; }
        public string Maintainer { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public sbyte? Orderby { get; set; }
    }
}
