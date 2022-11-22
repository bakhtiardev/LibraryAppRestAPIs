using System.ComponentModel.DataAnnotations;

namespace LibraryAppRestapi.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Address { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
