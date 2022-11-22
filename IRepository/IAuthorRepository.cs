using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IAuthorRepository
    {
        ICollection<Author> GetAuthors();
        Author GetAuthor(int id);

        ICollection<Book> GetBookByAuhtor(int auhorId);

        bool CreateAuthor(int bookId, Author author);
        bool AuhorExists(int id);
        bool Save();
    }
}
