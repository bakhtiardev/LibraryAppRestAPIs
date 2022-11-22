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
    public class PublisherController: Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        private IMapper _mapper;
        public PublisherController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
        public IActionResult GetPublishers()
        {
            var publishers = _mapper.Map<List<PublisherDto>>(_publisherRepository.GetPublishers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(publishers);
        }


        [HttpGet("{pubId}")]
        [ProducesResponseType(200, Type = typeof(Publisher))]
        [ProducesResponseType(400)]
        public IActionResult GetPublisher(int pubId)
        {
            if (!_publisherRepository.PublisherExists(pubId))
                return NotFound();

            var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisher(pubId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(publisher);
        }
        [HttpGet("publisherName")]
        public IActionResult GetPublisherByPubName([FromQuery] string pubName)
        {


            var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisher(pubName));

          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(publisher);
        }

        [HttpGet("book/{pubId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByPublisherId(int pubId)
        {

            var books = _mapper.Map<List<BookDto>>(_publisherRepository.GetBooksByPublisher(pubId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePublisher([FromBody] PublisherDto publisher)
        {
            if (publisher == null)
                return BadRequest(ModelState);

            var check= _publisherRepository.GetPublisher(publisher.PublisherName);

            if (check!=null)
            {
                ModelState.AddModelError("", "Publisher already exists");
                return StatusCode(403, ModelState);
            }

            var mapPub = _mapper.Map<Publisher>(publisher);
            if (!_publisherRepository.CreatePublisher(mapPub))
            {
                ModelState.AddModelError("", "Something went wrong in insertion");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
    }
}
