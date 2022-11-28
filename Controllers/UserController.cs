using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetAll());



            return Ok(users);
        }
    }
}
