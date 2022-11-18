namespace LibraryAppRestapi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Isbn { get; set; }
        
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<IssueRecord> IssueRecords { get; set; }
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

    }
}
