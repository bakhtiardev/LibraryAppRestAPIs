using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBook(int id);
        Book GetBook(string name);
        Book GetBookTrimToUpper(BookDto BookCreate);
        //decimal GetBookRating(int bookId);
        bool BookExists(int bookId);
        bool CreateBook(int authorId, int studentId,Book Book);
        //bool UpdateBook(int ownerId, int categoryId, Book Book);
        //bool DeleteBook(Book Book);
        bool Save();
    }
}
