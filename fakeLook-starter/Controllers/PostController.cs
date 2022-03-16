using auth_example.Filters;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(GetUserActionFilter))]
    public class PostController : ControllerBase
    {
        private IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // POST api/<PostController>/AddPost
        [HttpPost("AddPost")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public async Task AddPost([FromBody] Post item)
        {
            await _postRepository.AddPost(item);
        }


        // GET api/<PostController>/5
        [HttpGet("{id}")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public Post GetById(int id)
        {
       
            return _postRepository.GetById(id);
        }

        // EDIT api/<PostController>/5
        [HttpPut("{id}")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public async Task EditPost(Post item)
        {
            await _postRepository.EditPost(item);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public async Task DeletePost(Post item)
        {
            await _postRepository.DeletePost(item);
        }

        [HttpGet("GetAllPosts")]
        public IEnumerable<Post> GetAllPosts()
        {
            return _postRepository.GetAllPosts();
        }

        [HttpPost("TagPost")]
        public IEnumerable<Post> TagPost()
        {
            return _postRepository.GetAllPosts();
        }
    }
}
