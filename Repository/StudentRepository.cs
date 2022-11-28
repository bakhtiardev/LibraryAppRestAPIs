using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppRestapi.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public StudentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public ICollection<Student> GetStudentsByBook(int bookId)
        {
            return ApplicationDbContext.IssueRecords.Where(p => p.BookId == bookId).Select(c => c.Student).ToList();
        }

        //private readonly ApplicationDbContext _context;
        //public StudentRepository(ApplicationDbContext context)
        //{
        //    _context=context;
        //}
        //public ICollection<Book> GetBooksByStudent(int studentId)
        //{
        //    return _context.IssueRecords.Where(p => p.StudentId == studentId).Select(c => c.Book).ToList();
        //}

        //public Student GetStudent(int id)
        //{
        //    return _context.Students.Where(p => p.Id == id).FirstOrDefault();
        //}

        //public Student GetStudent(string name)
        //{
        //    return _context.Students.Where(p => p.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        //}

        //public ICollection<Student> GetStudentsByBook(int bookId)
        //{
        //    return _context.IssueRecords.Where(p => p.BookId == bookId).Select(c => c.Student).ToList();
        //}

        //public ICollection<Student> GetStudents()
        //{
        //    return _context.Students.ToList();
        //}

        //public bool StudentExists(int studentId)
        //{
        //    return _context.Students.Any(p => p.Id == studentId);
        //}

        //public bool CreateStudent(int bookId, Student student)
        //{
        //    var bookEntity=_context.Books.Where(p=>p.Id==bookId).FirstOrDefault();
        //    var studentBook = new IssueRecord()
        //    {
        //        IssueDate = DateTime.Now,
        //        Book = bookEntity,
        //        Student = student,

        //    };

        //    _context.Add(studentBook);
        //    _context.Add(student);

        //    return Save();
        //}
        //public bool UpdateStudent(int bookId, Student updateStudent)
        //{

        //    _context.Update(updateStudent);
        //    return Save();
        //}
        //public bool Save()
        //{
        //    var save = _context.SaveChanges();
        //    return save > 0 ? true: false;
        //}

        //public bool DeleteStudent(Student student)
        //{
        //    _context.Remove(student);
        //    return Save();
        //}
    }
}
