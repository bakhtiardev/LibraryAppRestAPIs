using LibraryAppRestapi.Data;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.Repository
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool BookExists(int bookId)
        {
            return _context.Books.Any(p=>p.Id==bookId); 
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(p => p.Id == id).FirstOrDefault();
        }

        public Book GetBook(string name)
        {
            return _context.Books.Where(p => p.Title == name).FirstOrDefault();
        }


        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(p => p.Id).ToList();
        }

        public Book GetBookTrimToUpper(BookDto bookCreate)
        {
            return _context.Books.Where(p => p.Title.Trim().ToUpper() == bookCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
