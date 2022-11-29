using AutoMapper;
using LibraryAppRestapi.IRepository;
using Microsoft.AspNetCore.Mvc;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.UnitOfWorkk;
using LibraryAppRestapi.Repository;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork repository, IMapper mapper)
        {
          
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var books = _mapper.Map<List<BookDto>>(_repository.Books.GetAll());
            return Ok(books);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBook(int bookId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = _mapper.Map<BookDto>(_repository.Books.Get(bookId));

            if(book == null)
                return NotFound();

            return Ok(book);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBook([FromQuery] int authorId, [FromQuery] int studentId, [FromQuery] int pubId, [FromBody] BookDto bookCreate)
        {
            
               

            if (!ModelState.IsValid || bookCreate ==null)
                return BadRequest(ModelState);

            var books = _repository.Books.GetBookTrimToUpper(bookCreate);

            if (books != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(422, ModelState);
            }


            var bookMap = _mapper.Map<Book>(bookCreate);
            bookMap.Publisher = _repository.Publishers.Get(pubId);
            _repository.Books.CreateBook(authorId, studentId, bookMap);

            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBook(int bookId,
            //[FromQuery] int authorId, [FromQuery] int studentId,
            [FromBody] BookDto updatedBook)
        {
            

            if (bookId != updatedBook.Id || updatedBook==null|| !ModelState.IsValid)
                return BadRequest(ModelState);


            var bookMap = _mapper.Map<Book>(updatedBook);
            _repository.Books.Update(bookMap);

            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong updating book");
                return StatusCode(500, ModelState);
            }

            return Ok("Update successful");
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook(int bookId)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var bookToDelte = _repository.Books.Get(bookId);


            _repository.Books.Remove(bookToDelte);


            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong deleting book");
            }

            return Ok("Delete successful");
        }

        //private readonly IBookRepository _bookRepository;
        //private readonly IPublisherRepository _publisherRepository;
        //private readonly IMapper _mapper;

        //public BookController(IBookRepository bookRepository, IPublisherRepository publisherRepository, IMapper mapper)
        //{
        //    _bookRepository = bookRepository;
        //    _publisherRepository = publisherRepository;
        //    _mapper = mapper;
        //}



        //[HttpGet("{bookId}")]
        //[ProducesResponseType(200, Type = typeof(Book))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBook(int bookId)
        //{
        //    if (!_bookRepository.BookExists(bookId))
        //        return NotFound();
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookId));



        //    return Ok(book);
        //}
        //[HttpGet("title")]
        //public IActionResult GetBookByTitle([FromQuery]string title)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var book = _mapper.Map<BookDto>(_bookRepository.GetBook(title));

        //    if (book == null || title == "" || title == null)
        //        return NotFound();

        //    return Ok(book);
        //}

        ////[HttpGet("{pokeId}/rating")]
        ////[ProducesResponseType(200, Type = typeof(decimal))]
        ////[ProducesResponseType(400)]
        ////public IActionResult GetPokemonRating(int pokeId)
        ////{
        ////    if (!_pokemonRepository.PokemonExists(pokeId))
        ////        return NotFound();

        ////    var rating = _pokemonRepository.GetPokemonRating(pokeId);

        ////    if (!ModelState.IsValid)
        ////        return BadRequest();

        ////    return Ok(rating);
        ////}

        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateBook([FromQuery] int authorId, [FromQuery] int studentId, [FromQuery] int pubId,[FromBody] BookDto bookCreate)
        //{
        //    if (bookCreate == null)
        //        return BadRequest(ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var books = _bookRepository.GetBookTrimToUpper(bookCreate);

        //    if (books != null)
        //    {
        //        ModelState.AddModelError("", "Owner already exists");
        //        return StatusCode(422, ModelState);
        //    }


        //    var bookMap = _mapper.Map<Book>(bookCreate);
        //    bookMap.Publisher = _publisherRepository.GetPublisher(pubId);

        //    if (!_bookRepository.CreateBook(authorId,studentId,bookMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while savin");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfully created");
        //}

        //[HttpPut("{bookId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult UpdateBook(int bookId,
        //    [FromQuery] int authorId, [FromQuery] int studentId,
        //    [FromBody] BookDto updatedBook)
        //{
        //    if (updatedBook == null)
        //        return BadRequest(ModelState);

        //    if (bookId != updatedBook.Id)
        //        return BadRequest(ModelState);

        //    if (!_bookRepository.BookExists(bookId))
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var bookMap = _mapper.Map<Book>(updatedBook);

        //    if (!_bookRepository.UpdateBook(authorId, studentId, bookMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong updating owner");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Update successful");
        //}

        //[HttpDelete("{bookId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeleteBook(int bookId)
        //{
        //    if (!_bookRepository.BookExists(bookId))
        //    {
        //        return NotFound();
        //    }
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);


        //    var bookToDelte = _bookRepository.GetBook(bookId);




        //    if (!_bookRepository.DeleteBook(bookToDelte))
        //    {
        //        ModelState.AddModelError("", "Something went wrong deleting book");
        //    }

        //    return Ok("Delete successful");
        //}
    }
}
