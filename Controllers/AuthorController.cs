using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Repository;
using LibraryAppRestapi.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork repository, IMapper mapper)
        {
            //_authorRepository = authorRepository;
            ////_reviewRepository = reviewRepository;
            //_bookRepository = bookRepository;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public IActionResult GetAuthors()
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authors = _mapper.Map<List<AuthorDto>>(_repository.Authors.GetAll());


            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthor(int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var author = _mapper.Map<AuthorDto>(_repository.Authors.Get(authorId));

            if(author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpGet("book/{authorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookByAuthorId(int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var books = _mapper.Map<List<BookDto>>(_repository.Authors.GetBookByAuthor(authorId));

            return Ok(books);
        }
        [HttpPost]

        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromQuery] int bookId, [FromBody] AuthorDto author)
        {
            if (author == null)
                return BadRequest(ModelState);

            var authMap = _mapper.Map<Author>(author);

            if (!_repository.Authors.CreateAuthor(bookId, authMap))
            {
                ModelState.AddModelError("", "Something went wrong while insertion");
                return StatusCode(500, ModelState);

            }

            return Ok("Successfully created");

            


        }
        [HttpPut("{authorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto author)
        {
            if (author == null || authorId != author.Id)
                return BadRequest(ModelState);


            var mapUpdate = _mapper.Map<Author>(author);

            _repository.Authors.Update(mapUpdate);


            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "somthing went wrong updating Author");
                return StatusCode(500, ModelState);

            }
            return Ok("Update Successful");

        }
        [HttpDelete("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorToDelete = _repository.Authors.Get(authorId);

            if (authorToDelete == null)
                return NotFound();

            _repository.Authors.Remove(authorToDelete);

            if(!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong deleting author");
                return StatusCode(500,ModelState);
            }

            return Ok("Delete successful");
        }
    }
}
