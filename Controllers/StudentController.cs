using LibraryAppRestapi.IRepository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Repository;
using System.Diagnostics.Metrics;
using LibraryAppRestapi.UnitOfWorkk;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public StudentController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var students = _mapper.Map<List<StudentDto>>(_repository.Students.GetAll());
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = _mapper.Map<StudentDto>(_repository.Students.Get(studentId));

            return Ok(book);
        }
        [HttpGet("student/{bookId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentByBookId(int bookId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _mapper.Map<List<StudentDto>>(_repository.Students.GetStudentsByBook(bookId));



            return Ok(student);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudent([FromQuery] int bookId, [FromBody] StudentDto student)
        {
            if (student == null)
                return BadRequest(ModelState);


            var mapStudent = _mapper.Map<Student>(student);

            if (!_repository.Students.CreateStudent(bookId, mapStudent))
            {
                ModelState.AddModelError("", "something went wrong in insertion");
                return StatusCode(500, ModelState);
            }

            return Ok("sucessfully added");
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int studentId, [FromBody] StudentDto updateStudent)
        {
            if (updateStudent == null || studentId != updateStudent.Id|| !ModelState.IsValid)
                return BadRequest(ModelState);

           

            var studentMap = _mapper.Map<Student>(updateStudent);
            _repository.Students.Update(studentMap);

            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong updating student");
                return StatusCode(500, ModelState);
            }

            return Ok("Update Successful");

        }
        [HttpDelete("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int studentId)
        {
            

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            var studentToDelte = _repository.Students.Get(studentId);

            if (studentToDelte == null)
                return NotFound();
            _repository.Students.Remove(studentToDelte);


            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong deleting student");
            }

            return Ok("Delete successful");
        }

        //private readonly IStudentRepository _studentRepository;
        //private IMapper _mapper;

        //public StudentController(IStudentRepository studentRepository, IMapper mapper)
        //{
        //    _studentRepository = studentRepository;
        //    _mapper = mapper;
        //}

        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        //public IActionResult GetStudents()
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());



        //    return Ok(students);
        //}

        //[HttpGet("{studentId}")]
        //[ProducesResponseType(200, Type = typeof(Student))]
        //[ProducesResponseType(400)]
        //public IActionResult GetStudent(int studentId)
        //{
        //    if (!_studentRepository.StudentExists(studentId))
        //        return NotFound();
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var book = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));


        //    return Ok(book);
        //}
        //[HttpGet("student/{bookId}")]
        //[ProducesResponseType(200, Type = typeof(Student))]
        //[ProducesResponseType(400)]
        //public IActionResult GetStudentByBookId(int bookId)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var student = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudentsByBook(bookId));



        //    return Ok(student);
        //}
        //[HttpGet("book/{studentId}")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBooksByStudentId(int studentId)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var books = _mapper.Map<List<BookDto>>(_studentRepository.GetBooksByStudent(studentId));


        //    return Ok(books);
        //}

        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateStudent([FromQuery]int bookId, [FromBody] StudentDto student)
        //{
        //    if (student == null)
        //        return BadRequest(ModelState);

        //    var check = _studentRepository.GetStudent(student.Name);

        //    if(check != null)
        //    {
        //        ModelState.AddModelError("", "student already exists!");
        //        return StatusCode(403,ModelState);
        //    }

        //    var mapStudent = _mapper.Map<Student>(student);


        //    if(!_studentRepository.CreateStudent(bookId, mapStudent))
        //    {
        //        ModelState.AddModelError("", "something went wrond in insertion");
        //        return StatusCode(500,ModelState);
        //    }

        //    return Ok("sucessfully added");
        //}
        //[HttpPut("{studentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult UpdateStudent(int studentId,[FromQuery]int bookId, [FromBody] StudentDto updateStudent)
        //{
        //    if (updateStudent == null)
        //        return BadRequest(ModelState);

        //    if (studentId != updateStudent.Id)
        //        return BadRequest(ModelState);

        //    if (!_studentRepository.StudentExists(studentId))
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var studentMap = _mapper.Map<Student>(updateStudent);

        //    if (!_studentRepository.UpdateStudent(bookId,studentMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong updating student");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}
        //[HttpDelete("{studentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeleteStudent(int studentId)
        //{
        //    if (!_studentRepository.StudentExists(studentId))
        //    {
        //        return NotFound();
        //    }

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var studentToDelte = _studentRepository.GetStudent(studentId);





        //    if (!_studentRepository.DeleteStudent(studentToDelte))
        //    {
        //        ModelState.AddModelError("", "Something went wrong deleting student");
        //    }

        //    return Ok("Delete successful");
        //}
    }
}
