﻿namespace LibraryAppRestapi.Models
{
    public class IssueRecord
    {
        public int Id { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int StudentId { get; set; }
        public int BookId { get; set; }
        public Student Student { get; set; }
        public Book Book { get; set; }
       
    }
}
