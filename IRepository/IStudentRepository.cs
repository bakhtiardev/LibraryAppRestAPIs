using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        ICollection<Student> GetStudentsByBook(int bookId);
        /*ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudent(string name);
        ICollection<Student> GetStudentsByBook(int bookId);
        ICollection<Book> GetBooksByStudent(int studentId);
        bool StudentExists(int studentId);
        bool CreateStudent(int bookId, Student student);
        bool UpdateStudent(int bookId, Student student);
        bool DeleteStudent(Student student);
        bool Save();*/

    }
}
