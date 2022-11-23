using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueRecordController: Controller
    {
        private readonly IIssueRecordRepository _issueRecordRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private IMapper _mapper;
        public IssueRecordController(IIssueRecordRepository issueRecordRepository,
            IStudentRepository studentRepository,IBookRepository bookRepository ,IMapper mapper)
        {
            _issueRecordRepository = issueRecordRepository;
            _studentRepository = studentRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IssueRecord>))]
        public IActionResult GetIssueRecords()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var publishers = _mapper.Map<List<IssueRecordDto>>(_issueRecordRepository.GetIssueRecords());

            return Ok(publishers);
        }

        [HttpGet("{issueId}")]
        [ProducesResponseType(200, Type = typeof(IssueRecord))]
        [ProducesResponseType(400)]
        public IActionResult GetIssueRecord(int issueId)
        {
            if (!_issueRecordRepository.IssueRecordExists(issueId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var publisher = _mapper.Map<IssueRecordDto>(_issueRecordRepository.GetIssueRecord(issueId));

            return Ok(publisher);
        }

        [HttpGet("recordsBystudent/{studentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IssueRecord>))]
        [ProducesResponseType(400)]
        public IActionResult GetIssueRecordsByStudent(int studentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var books = _mapper.Map<List<IssueRecordDto>>(_issueRecordRepository.GetIssueRecordbyStudent(studentId));
            return Ok(books);
        }

        [HttpGet("recordsBybook/{bookId}")]
       
        public IActionResult GetIssueRecordByBook(int bookId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var books = _mapper.Map<List<IssueRecordDto>>(_issueRecordRepository.GetIssueRecordByBook(bookId));
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]

        public IActionResult CreateIssueRecord([FromQuery] int studentId, [FromQuery] int bookId, [FromBody] IssueRecordDto issueRecord)
        {
            if (issueRecord == null)
                return BadRequest(ModelState);

            var mapRecord = _mapper.Map<IssueRecord>(issueRecord);
            mapRecord.Student=_studentRepository.GetStudent(studentId);
            mapRecord.Book=_bookRepository.GetBook(bookId);

            if (!_issueRecordRepository.CreateIssueRecord(mapRecord))
            {
                ModelState.AddModelError("", "something went wrong!");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully created!");
        }
        [HttpPut("{issueId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateIssueRecord(int issueId, [FromBody] IssueRecordDto updatedIssueRecord)
        {
            if (updatedIssueRecord == null)
                return BadRequest(ModelState);

            if (issueId != updatedIssueRecord.Id)
                return BadRequest(ModelState);

            if (!_issueRecordRepository.IssueRecordExists(issueId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var issueMap = _mapper.Map<IssueRecord>(updatedIssueRecord);

            if (!_issueRecordRepository.UpdateIssueRecord(issueMap))
            {
                ModelState.AddModelError("", "Something went wrong updating issueRecord");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successful");
        }
        [HttpDelete("{issueId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteIssueRecord(int issueId)
        {
            if (!_issueRecordRepository.IssueRecordExists(issueId))
            {
                return NotFound();
            }

            var ownerToDelete = _issueRecordRepository.GetIssueRecord(issueId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_issueRecordRepository.DeleteIssueRecord(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting issueRecord");
            }

            return Ok("Delete successful");
        }
    }
}
