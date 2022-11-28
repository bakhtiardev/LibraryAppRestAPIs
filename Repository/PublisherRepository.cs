using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LibraryAppRestapi.Repository
{
    public class PublisherRepository : Repository<Publisher> ,IPublisherRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public PublisherRepository(ApplicationDbContext context) : base(context)
        {

        }

        public ICollection<Book> GetBooksByPublisher(int pubId)
        {
            return ApplicationDbContext.Books.Where(p => p.PublisherId == pubId).ToList();
        }

        /*private readonly ApplicationDbContext _context;
        public PublisherRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreatePublisher(Publisher publisher)
        {
            _context.Add(publisher);
            return Save();
        }
        public bool UpdatePublisher(Publisher publisher)
        {
            _context.Update(publisher);
            return Save();
        }

        public ICollection<Book> GetBooksByPublisher(int pubId)
        {
            return _context.Books.Where(p => p.PublisherId == pubId).ToList();
        }

        public Publisher GetPublisher(int pubId)
        {
            return _context.Publishers.Where(p => p.Id == pubId).FirstOrDefault();
        }

        public Publisher GetPublisher(string name)
        {
            return _context.Publishers.Where(p => p.PublisherName.ToLower().Equals(name.ToLower())).FirstOrDefault();
        }

        public ICollection<Publisher> GetPublishers()
        {
            return _context.Publishers.ToList();
        }

        public bool PublisherExists(int pubId)
        {
            return _context.Publishers.Any(p => p.Id == pubId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool DeletePublisher(Publisher publisher)
        {
            _context.Remove(publisher);
            return Save();
        }*/

    }
}
