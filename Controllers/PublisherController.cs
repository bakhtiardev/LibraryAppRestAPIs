using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Repository;
using LibraryAppRestapi.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController: Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public PublisherController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
        public IActionResult GetPublishers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publishers = _mapper.Map<List<PublisherDto>>(_repository.Publishers.GetAll());


            return Ok(publishers);
        }
        [HttpGet("{pubId}")]
        [ProducesResponseType(200, Type = typeof(Publisher))]
        [ProducesResponseType(400)]
        public IActionResult GetPublisher(int pubId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = _mapper.Map<PublisherDto>(_repository.Publishers.Get(pubId));

            return Ok(publisher);
        }
        [HttpGet("book/{pubId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByPublisherId(int pubId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var books = _mapper.Map<List<BookDto>>(_repository.Publishers.GetBooksByPublisher(pubId));

            return Ok(books);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePublisher([FromBody] PublisherDto publisher)
        {
            if (publisher == null)
                return BadRequest(ModelState);


            var mapPub = _mapper.Map<Publisher>(publisher);
            _repository.Publishers.Add(mapPub);

            
            if (!_repository.Complete())
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
            if (updatePublisher == null || publisherId != updatePublisher.Id)
                return BadRequest(ModelState);


            var mapUpdate = _mapper.Map<Publisher>(updatePublisher);
            _repository.Publishers.Update(mapUpdate);
            if (!_repository.Complete())
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
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pubToDelete = _repository.Publishers.Get(pubId);



            _repository.Publishers.Remove(pubToDelete);

            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong deleting publisher");
            }

            return Ok("Delete successful");
        }
        //private readonly IPublisherRepository _publisherRepository;
        //private readonly IBookRepository _bookRepository;
        //private IMapper _mapper;
        //public PublisherController(IPublisherRepository publisherRepository,IBookRepository bookRepository, IMapper mapper)
        //{
        //    _publisherRepository = publisherRepository;
        //    _bookRepository = bookRepository;
        //    _mapper = mapper;
        //}
        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
        //public IActionResult GetPublishers()
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var publishers = _mapper.Map<List<PublisherDto>>(_publisherRepository.GetPublishers());


        //    return Ok(publishers);
        //}


        //[HttpGet("{pubId}")]
        //[ProducesResponseType(200, Type = typeof(Publisher))]
        //[ProducesResponseType(400)]
        //public IActionResult GetPublisher(int pubId)
        //{
        //    if (!_publisherRepository.PublisherExists(pubId))
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisher(pubId));


        //    return Ok(publisher);
        //}
        //[HttpGet("publisherName")]
        //public IActionResult GetPublisherByPubName([FromQuery] string pubName)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisher(pubName));




        //    return Ok(publisher);
        //}

        //[HttpGet("book/{pubId}")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBooksByPublisherId(int pubId)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var books = _mapper.Map<List<BookDto>>(_publisherRepository.GetBooksByPublisher(pubId));



        //    return Ok(books);
        //}
        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreatePublisher([FromBody] PublisherDto publisher)
        //{
        //    if (publisher == null)
        //        return BadRequest(ModelState);

        //    var check= _publisherRepository.GetPublisher(publisher.PublisherName);

        //    if (check!=null)
        //    {
        //        ModelState.AddModelError("", "Publisher already exists");
        //        return StatusCode(403, ModelState);
        //    }

        //    var mapPub = _mapper.Map<Publisher>(publisher);
        //    if (!_publisherRepository.CreatePublisher(mapPub))
        //    {
        //        ModelState.AddModelError("", "Something went wrong in insertion");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfuly created");
        //}


        //[HttpPut("{publisherId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public IActionResult UpdatePublisher(int publisherId, [FromBody] PublisherDto updatePublisher)
        //{
        //    if (updatePublisher == null)
        //        return BadRequest(ModelState);

        //    if (publisherId != updatePublisher.Id)
        //        return BadRequest(ModelState);

        //    if (!_publisherRepository.PublisherExists(publisherId))
        //        return NotFound();

        //    var mapUpdate = _mapper.Map<Publisher>(updatePublisher);

        //    if (!_publisherRepository.UpdatePublisher(mapUpdate))
        //    {
        //        ModelState.AddModelError("", "somthing went wrong");
        //        return StatusCode(500, ModelState);

        //    }
        //    return Ok("Update Successful");

        //}
        //[HttpDelete("{pubId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeletePublisher(int pubId)
        //{
        //    if (!_publisherRepository.PublisherExists(pubId))
        //    {
        //        return NotFound();
        //    }
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var pubToDelete = _publisherRepository.GetPublisher(pubId);





        //    if (!_publisherRepository.DeletePublisher(pubToDelete))
        //    {
        //        ModelState.AddModelError("", "Something went wrong deleting publisher");
        //    }

        //    return Ok("Delete successful");
        //}
    }
}
