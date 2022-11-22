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
        private IMapper _mapper;
        public IssueRecordController(IIssueRecordRepository issueRecordRepository,IMapper mapper)
        {
            _issueRecordRepository = issueRecordRepository;
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
    }
}
