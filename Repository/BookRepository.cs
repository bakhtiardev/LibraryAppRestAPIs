using LibraryAppRestapi.Data;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppRestapi.Repository
{
    public class BookRepository : Repository<Book>,IBookRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public Book GetBookTrimToUpper(BookDto bookCreate)
        {
            return ApplicationDbContext.Books.Where(p => p.Title.Trim().ToUpper() == bookCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
        }
        public bool CreateBook(int authorId, int studentId, Book book)
        {
            var authorEntity = ApplicationDbContext.Authors.Where(p => p.Id == authorId).FirstOrDefault();
            var studentEntity = ApplicationDbContext.Students.Where(c => c.Id == studentId).FirstOrDefault();
            //var pubEntity = _context.Publishers.Where(x=>x.Id==pubId).FirstOrDefault();


            var bookAuth = new BookAuthor()
            {
                Author = authorEntity,
                Book = book,
            };
            ApplicationDbContext.Add(bookAuth);

            var bookStudent = new IssueRecord()
            {
                Student = studentEntity,
                Book = book,
            };

            ApplicationDbContext.Add(bookStudent);

            ApplicationDbContext.Add(book);

            return ApplicationDbContext.SaveChanges() > 0 ? true : false;

        }

        
        //private readonly ApplicationDbContext _context;
        //public BookRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public bool BookExists(int bookId)
        //{
        //    return _context.Books.Any(p=>p.Id==bookId); 
        //}

        //public bool CreateBook(int authorId, int studentId,Book book)
        //{
        //    var authorEntity = _context.Authors.Where(p => p.Id == authorId).FirstOrDefault();
        //    var studentEntity = _context.Students.Where(c=>c.Id==studentId).FirstOrDefault();
        //    //var pubEntity = _context.Publishers.Where(x=>x.Id==pubId).FirstOrDefault();


        //    var bookAuth = new BookAuthor()
        //    {
        //        Author = authorEntity,
        //        Book = book,
        //    };
        //    _context.Add(bookAuth);

        //    var bookStudent = new IssueRecord()
        //    {
        //        Student = studentEntity,
        //        Book = book,
        //    };

        //    _context.Add(bookStudent);

        //    _context.Add(book);

        //    return Save();

        //}
        //public bool UpdateBook(int authId, int studentId, Book book)
        //{
        //    _context.Update(book);
        //    return Save();
        //}

        //public Book GetBook(int id)
        //{
        //    return _context.Books.Where(p => p.Id == id).FirstOrDefault();
        //}

        //public Book GetBook(string name)
        //{
        //    return _context.Books.Where(p => p.Title.Trim().ToUpper().Equals( name.Trim().ToUpper())).FirstOrDefault();
        //}


        //public ICollection<Book> GetBooks()
        //{
        //    return _context.Books.OrderBy(p => p.Id).ToList();
        //}

        //public Book GetBookTrimToUpper(BookDto bookCreate)
        //{
        //    return _context.Books.Where(p => p.Title.Trim().ToUpper() == bookCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
        //}

        //public bool Save()
        //{
        //   var saved = _context.SaveChanges();
        //    return saved > 0 ? true : false;
        //}

        //public bool DeleteBook(Book Book)
        //{
        //   _context.Remove(Book);
        //    return Save();
        //}
    }
}
