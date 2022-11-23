using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository,IBookRepository bookRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            //_reviewRepository = reviewRepository;
            _bookRepository= bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public IActionResult GetAuthors()
        {
            var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAuthors());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthor(int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_authorRepository.AuhorExists(authorId))
                return NotFound();


            var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(authorId));



            return Ok(author);
        }

        [HttpGet("book/{authorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookByAuthorId(int authorId)
        {


            var books = _mapper.Map<List<BookDto>>(_authorRepository.GetBookByAuhtor(authorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }
        [HttpPost]
        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromQuery] int bookId, [FromBody] AuthorDto author)
        {
            if (author == null)
                return BadRequest(ModelState);

            var checkAuth = _authorRepository.GetAuthors()
                .Where(p => p.AuthorName.Trim().ToLower().Equals(author.AuthorName.Trim().ToLower()))
                .FirstOrDefault();

            if(checkAuth!=null)
            {
                ModelState.AddModelError("", "Author already exists");
                return StatusCode(422,ModelState);
            }

            var authMap = _mapper.Map<Author>(author);
            
            if (!_authorRepository.CreateAuthor(bookId,authMap))
            {
                ModelState.AddModelError("", "Something went wrong while insertion");
                return StatusCode(500, ModelState);

            }

            return Ok("Successfully created");

        }
        [HttpPut("{authorId}")]

        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto author)
        {
            if (author == null)
                return BadRequest(ModelState);

            if (authorId != author.Id)
                return BadRequest(ModelState);

            if (!_authorRepository.AuhorExists(authorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var authorMap = _mapper.Map<Author>(author);
            
            if (!_authorRepository.UpdateAuthor( authorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating author");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successful");

        }
        [HttpDelete("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (!_authorRepository.AuhorExists(authorId))
            {
                return NotFound();
            }

            var authorToDelete = _authorRepository.GetAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_authorRepository.DeleteAuthor(authorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting author");
            }

            return Ok("Delete successful");
        }
    }
}
