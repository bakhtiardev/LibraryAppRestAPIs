using AutoMapper;
using Azure.Core;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LibraryAppRestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork repository, IConfiguration configuration, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
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

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserDto user)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            if (!ModelState.IsValid || user == null)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(user);
            userMap.PasswordHash = passwordHash;
            userMap.PasswordSalt = passwordSalt;

            _repository.Users.Add(userMap);

            if (!_repository.Complete())
            {
                return BadRequest(ModelState);
            }
            return Ok(userMap);
        }

        [HttpPost("login")]
        public ActionResult Login(UserDto request)
        {
            
            if (!_repository.Users.userExists(request.Username))
            {
                return BadRequest("User not found.");
            }
            var user = _repository.Users.Get(request.Username);

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        //[HttpPut("{userId}")]
        //public IActionResult UpdateUser(int userId, [FromBody] UserDto user)
        //{
        //    if (userId != user.Id || !ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var userMap = _mapper.Map<User>(user);
        //    _repository.Users.Update(userMap);

        //    if (!_repository.Complete())
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return Ok("Successfully updated");
        //}

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
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
