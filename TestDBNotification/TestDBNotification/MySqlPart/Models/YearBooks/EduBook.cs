using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.YearBooks
{
    public partial class EduBook
    {
        public string Edu { get; set; }
        public string BookCode { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Maintainer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
