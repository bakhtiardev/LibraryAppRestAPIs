using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        ICollection<Author> GetAuthors();
        Author GetAuthor(int id);

        ICollection<Book> GetBookByAuhtor(int auhorId);

        bool CreateAuthor(int bookId,Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool AuhorExists(int id);
        bool Save();
    }
}
