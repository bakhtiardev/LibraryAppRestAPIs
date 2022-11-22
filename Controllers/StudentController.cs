using LibraryAppRestapi.IRepository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Repository;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var book = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }
        [HttpGet("student/{bookId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentByBookId(int bookId)
        {


            var student = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudentsByBook(bookId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(student);
        }
        [HttpGet("book/{studentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByStudentId(int studentId)
        {
            var books = _mapper.Map<List<BookDto>>(_studentRepository.GetBooksByStudent(studentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }
    }
}
