using auth_example.Filters;
using fakeLook_models.Models;
using fakeLook_starter.Contract;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auth_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _repo;
        private ITokenService _tokenService { get; }

        public AuthController(IUserRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            var dbUser = _repo.FindItem(user);
            // Check if user in Db
            if (dbUser == null) return Problem("userName doesn't exist");
            // Check if password is correct
            if (dbUser.Password != Utilities.CreateHashCode(user.Password)) return Problem("the password is wrong");

            var token = _tokenService.CreateToken(dbUser);
            //return Ok(new { token });
            var res = UserContract.CreateInstance(token, dbUser);
            return Ok(res);
            //return  Ok(new { token = res.Token, username = res.UserName});
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] User user)
        {
            var checkUser = _repo.FindItem(user);
            // Check if user in Db
            if (checkUser != null) return Problem("userName already in DB");

            var dbUser = _repo.Add(user).Result;
            var token = _tokenService.CreateToken(dbUser);
            return Ok(UserContract.CreateInstance(token, dbUser));
        }

        [Authorize]
        [HttpGet]
        [Route("TestAll")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IActionResult TestAll()
        {
            return Ok();
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        [Route("TestAdmin")]
        public IActionResult TestAdmin()
        {
            return Ok();
        }
    }
}
