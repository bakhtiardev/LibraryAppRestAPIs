using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = _mapper.Map<List<UserDto>>(_repository.Users.GetAll());
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            if (!ModelState.IsValid || user == null)
                return BadRequest(ModelState);

            var userDto = _mapper.Map<User>(user);
            _repository.Users.Add(userDto);

            if (!_repository.Complete())
            {
                return BadRequest(ModelState);
            }
            return Ok("Successfully added");
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto user)
        {
            if (userId != user.Id || !ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(user);
            _repository.Users.Update(userMap);

            if (!_repository.Complete())
            {
                return BadRequest(ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userToDelete = _repository.Users.Get(userId);

            if (userToDelete == null)
                return NotFound();
            _repository.Users.Remove(userToDelete);


            if (!_repository.Complete())
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return Ok("Delete successful");
        }
    }
}
