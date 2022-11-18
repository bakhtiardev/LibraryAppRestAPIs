using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppRestapi.Models
{
    public class Publisher
    {
       
        public int Id { get; set; }
       
        public string PublisherName { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
