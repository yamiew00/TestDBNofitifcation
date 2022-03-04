using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.Resources
{
    public partial class Source
    {
        public string Uid { get; set; }
        public string MetaType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentUid { get; set; }
        public string Maintainer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Remark { get; set; }
    }
}
