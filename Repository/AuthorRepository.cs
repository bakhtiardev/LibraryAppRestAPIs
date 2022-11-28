using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppRestapi.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
       
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
           
        }

        public ICollection<Book> GetBookByAuthor(int auhorId)
        {
            return ApplicationDbContext.BookAuthors.Where(e => e.AuthorId == auhorId).Select(c => c.Book).ToList();
        }
        //public bool AuhorExists(int id)
        //{
        //    return _context.Authors.Any(a => a.Id == id);
        //}

        public bool CreateAuthor(int bookId, Author author)
        {
            var bookEntity = ApplicationDbContext.Books.Where(p => p.Id == bookId).FirstOrDefault();

            var bookAuth = new BookAuthor()
            {
                Book = bookEntity,
                Author = author,
            };
            ApplicationDbContext.Add(bookAuth);
            ApplicationDbContext.Add(author);
            return ApplicationDbContext.SaveChanges() > 0 ? true :false ;
        }
        //public bool UpdateAuthor(Author updateAuthor)
        //{
        //    _context.Update(updateAuthor);
        //    return Save();
        //}
        //public Author GetAuthor(int id)
        //{
        //    return _context.Authors.Where(p => p.Id == id).FirstOrDefault();
        //}

        //public ICollection<Author> GetAuthors()
        //{
        //    return _context.Authors.ToList();
        //}

        //public ICollection<Book> GetBookByAuhtor(int auhorId)
        //{
        //    return _context.BookAuthors.Where(e=>e.AuthorId==auhorId).Select(c=>c.Book).ToList();
        //}
        //public bool Save()
        //{
        //    var saved = ApplicationDbContext.SaveChanges();
        //    return saved > 0 ? true : false;
        //}

        //public bool DeleteAuthor(Author author)
        //{
        //    _context.Remove(author);
        //    return Save();
        //}
    }
}
