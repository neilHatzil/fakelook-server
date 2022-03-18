using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        // GET api/<UserController>/userName
        [HttpGet("{userName}")]
        public User Get(string userName)
        {
            return _userRepository.GetByUserName(userName);
        }

        // POST api/<UserController>
        [HttpPost("SignUp")]
        public async Task Post([FromBody] User value)
        { 
            await _userRepository.Add(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task Put([FromBody] User value)
        {
            //var checkUser = _userRepository.FindItem(value);
            //if (checkUser == null) return Problem("userName already in DB");
            await _userRepository.Update(value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
