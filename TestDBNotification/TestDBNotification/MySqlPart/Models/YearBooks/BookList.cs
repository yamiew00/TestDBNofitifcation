using System;
using System.Collections.Generic;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.YearBooks
{
    public partial class BookList
    {
        public string BookId { get; set; }
        public int Year { get; set; }
        public string Curriculum { get; set; }
        public string EduSubject { get; set; }
        public string Maintainer { get; set; }
        public string MaintainerName { get; set; }
        public DateTime CreateTime { get; set; }
        public string LockMaintainer { get; set; }
        public ulong? IsLock { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
