using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface  IPublisherRepository
    {
        ICollection<Publisher> GetPublishers();
        Publisher GetPublisher(int pubId);
        Publisher GetPublisher(string name);

        ICollection<Book> GetBooksByPublisher(int pubId);
        bool PublisherExists(int pubId);
        bool CreatePublisher(Publisher publisher);
        bool UpdatePublisher(Publisher publisher);
        bool DeletePublisher(Publisher pubId);
        bool Save();
    }
}
