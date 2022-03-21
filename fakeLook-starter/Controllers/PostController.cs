using auth_example.Filters;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(GetUserActionFilter))]
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
        public async Task<ActionResult<Post>> AddPost([FromBody] Post item)//, ICollection<string> taggedUsers)
        {
            try
            {
                return await _postRepository.AddPost(item);
            }
            catch (Exception)
            {
                return Problem("Add post failed");
            }
            
        }


        // GET api/<PostController>/5
        [HttpGet("{id}")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public Post GetById(int id)
        {
            return _postRepository.GetById(id);
        }

        // EDIT api/<PostController>/EditPost
        [HttpPut("EditPost")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<Post>> EditPost([FromBody]Post item)
        {
            return await _postRepository.EditPost(item);
            //try
            //{
            //    return await _postRepository.EditPost(item);
            //}
            //catch (Exception)
            //{

            //    return Problem("Edit post failed");
            //}
        }


        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        //[Route("Authenticated")]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public async Task DeletePost(int id)
        {
            await _postRepository.DeletePost(id);
        }

        [HttpGet("GetAllPosts")]
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }

        // POST api/<PostController>/LikeUnlike/{postId}/{userId}
        [HttpPost("LikeUnlike/{postId}/{userId}")]
        public async Task<ActionResult<Post>> LikeUnlike(int postId, int userId)
        {
            try
            {
                return await _postRepository.LikeUnlike(postId, userId);
            }
            catch (Exception)
            {

                return Problem("LikeUnlike post failed");
            }
            
        }

        // POST api/<PostController>/AddComment
        [HttpPost("AddComment")]
        public async Task<ActionResult<Post>> AddComment([FromBody] Comment item)
        {
            try
            {
                return await _postRepository.AddComment(item);
            }
            catch (Exception)
            {
                return Problem("Add comment post failed");
            } 
        }

        [HttpPost]
        [Route("/Filter")]
        public ICollection<Post> Filter([FromBody]PostFilter filter)
        {
            var res = _postRepository.GetByPredicate(post =>
            {
                bool date = filter.checkDate(post.Date);//, filter.StartingDate, filter.EndingDate);
                bool publishers = filter.checkPublishers(_postRepository.ConvertUserIdToUserName(post.UserId));//, filter.Publishers);
                bool taggs = filter.checkTaggs(post.Tags);//, filter.Tags);
                bool taggedUsers = filter.checkTaggedUsers(post.UserTaggedPost);//, filter.TaggedUsers);
                return date && publishers && taggs && taggedUsers;
            });
            return res;
        }
    }
}
