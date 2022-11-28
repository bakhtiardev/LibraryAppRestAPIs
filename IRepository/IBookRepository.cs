using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetBookTrimToUpper(BookDto BookCreate);
        bool CreateBook(int authorId, int studentId, Book book);
        //bool UpdateBook(int authorId, int studentId, Book book);
        /*  ICollection<Book> GetBooks();

          Book GetBook(int id);
          Book GetBook(string name);
          Book GetBookTrimToUpper(BookDto BookCreate);
          //decimal GetBookRating(int bookId);
          bool BookExists(int bookId);
          bool CreateBook(int authorId, int studentId,Book book);
          bool UpdateBook(int authorId, int studentId,Book book);

          bool DeleteBook(Book Book);
          bool Save();*/
    }
}
