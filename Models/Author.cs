namespace LibraryAppRestapi.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Address { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
