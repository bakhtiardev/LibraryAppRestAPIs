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
        private readonly IBookRepository _bookRepository;
        private IMapper _mapper;
        public PublisherController(IPublisherRepository publisherRepository,IBookRepository bookRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
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


        [HttpPut("{publisherId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePublisher(int publisherId, [FromBody] PublisherDto updatePublisher)
        {
            if (updatePublisher == null)
                return BadRequest(ModelState);

            if (publisherId != updatePublisher.Id)
                return BadRequest(ModelState);

            if (!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            var mapUpdate = _mapper.Map<Publisher>(updatePublisher);

            if (!_publisherRepository.UpdatePublisher(mapUpdate))
            {
                ModelState.AddModelError("", "somthing went wrong");
                return StatusCode(500, ModelState);

            }
            return Ok("Update Successful");

        }
        [HttpDelete("{pubId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePublisher(int pubId)
        {
            if (!_publisherRepository.PublisherExists(pubId))
            {
                return NotFound();
            }

          
            var pubToDelete = _publisherRepository.GetPublisher(pubId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!_publisherRepository.DeletePublisher(pubToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting publisher");
            }

            return Ok("Delete successful");
        }
    }
}
