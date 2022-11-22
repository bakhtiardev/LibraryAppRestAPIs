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

        public bool CreateBook(int authorId, int studentId,Book book)
        {
            var authorEntity = _context.Authors.Where(p => p.Id == authorId).FirstOrDefault();
            var studentEntity = _context.Students.Where(c=>c.Id==studentId).FirstOrDefault();
            //var pubEntity = _context.Publishers.Where(x=>x.Id==pubId).FirstOrDefault();


            var bookAuth = new BookAuthor()
            {
                Author = authorEntity,
                Book = book,
            };
            _context.Add(bookAuth);

            var bookStudent = new IssueRecord()
            {
                Student = studentEntity,
                Book = book,
            };
            
            _context.Add(bookStudent);

            _context.Add(book);

            return Save();

        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(p => p.Id == id).FirstOrDefault();
        }

        public Book GetBook(string name)
        {
            return _context.Books.Where(p => p.Title.Trim().ToUpper().Equals( name.Trim().ToUpper())).FirstOrDefault();
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
