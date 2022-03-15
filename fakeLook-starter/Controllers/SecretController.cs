using auth_example.Filters;
using fakeLook_models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecretController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("All")]
        public IActionResult All()
        {
            return Ok(new { msg = "everybody gets this" });
        }

        [HttpGet]
        [Route("Authenticated")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IActionResult Authenticated()
        {
            Request.RouteValues.TryGetValue("user", out var obj);
            var user = obj as User;
            return Ok(new { msg = $"only authenticated gets this {user.Name}" });
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        [Route("Admin")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IActionResult Admin()
        {
            Request.RouteValues.TryGetValue("user", out var obj);
            var user = obj as User;
            return Ok(new { msg = $"only admin gets this {user.Name}" });
        }
    }
}
