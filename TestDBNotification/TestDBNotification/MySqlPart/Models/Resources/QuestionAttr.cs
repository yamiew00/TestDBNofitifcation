using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.Resources
{
    public partial class QuestionAttr
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Maintainer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
