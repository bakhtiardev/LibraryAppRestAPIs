using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AuhorExists(int id)
        {
            return _context.Authors.Any(a => a.Id == id);
        }

        public bool CreateAuthor(int bookId, Author author)
        {
            var bookEntity = _context.Books.Where(p => p.Id == bookId).FirstOrDefault();

            var bookAuth = new BookAuthor()
            {
                Book = bookEntity,
                Author = author,
            };
            _context.Add(bookAuth);
            _context.Add(author);
            return Save();
        }
        public bool UpdateAuthor(Author updateAuthor)
        {
            _context.Update(updateAuthor);
            return Save();
        }
        public Author GetAuthor(int id)
        {
            return _context.Authors.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public ICollection<Book> GetBookByAuhtor(int auhorId)
        {
            return _context.BookAuthors.Where(e=>e.AuthorId==auhorId).Select(c=>c.Book).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteAuthor(Author author)
        {
            _context.Remove(author);
            return Save();
        }
    }
}
