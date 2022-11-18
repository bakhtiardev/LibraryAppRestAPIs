using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudent(string name);
       
    }
}
