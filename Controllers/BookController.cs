using AutoMapper;
using LibraryAppRestapi.IRepository;
using Microsoft.AspNetCore.Mvc;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.Dto;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        //private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            //_reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();

            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }
        [HttpGet("title")]
        public IActionResult GetBookByTitle([FromQuery]string title)
        {
           

            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(title));

            if (book == null || title == "" || title==null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        //[HttpGet("{pokeId}/rating")]
        //[ProducesResponseType(200, Type = typeof(decimal))]
        //[ProducesResponseType(400)]
        //public IActionResult GetPokemonRating(int pokeId)
        //{
        //    if (!_pokemonRepository.PokemonExists(pokeId))
        //        return NotFound();

        //    var rating = _pokemonRepository.GetPokemonRating(pokeId);

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(rating);
        //}

        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        //{
        //    if (pokemonCreate == null)
        //        return BadRequest(ModelState);

        //    var pokemons = _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate);

        //    if (pokemons != null)
        //    {
        //        ModelState.AddModelError("", "Owner already exists");
        //        return StatusCode(422, ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);


        //    if (!_pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while savin");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfully created");
        //}

        //[HttpPut("{pokeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult UpdatePokemon(int pokeId,
        //    [FromQuery] int ownerId, [FromQuery] int catId,
        //    [FromBody] PokemonDto updatedPokemon)
        //{
        //    if (updatedPokemon == null)
        //        return BadRequest(ModelState);

        //    if (pokeId != updatedPokemon.Id)
        //        return BadRequest(ModelState);

        //    if (!_pokemonRepository.PokemonExists(pokeId))
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);

        //    if (!_pokemonRepository.UpdatePokemon(ownerId, catId, pokemonMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong updating owner");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{pokeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeleteBook(int bookId)
        //{
        //    if (!_bookRepository.BookExists(bookId))
        //    {
        //        return NotFound();
        //    }

        //    var reviewsToDelete = _reviewRepository.GetReviewsOfAPokemon(pokeId);
        //    var pokemonToDelete = _pokemonRepository.GetPokemon(pokeId);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
        //    {
        //        ModelState.AddModelError("", "Something went wrong when deleting reviews");
        //    }

        //    if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
        //    {
        //        ModelState.AddModelError("", "Something went wrong deleting owner");
        //    }

        //    return NoContent();
        //}
    }
}
