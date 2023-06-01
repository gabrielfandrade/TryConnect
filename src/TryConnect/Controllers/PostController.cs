using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostController : Controller
    {
        private readonly ITryConnectRepository _repository;

        public PostController(ITryConnectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_repository.GetPosts());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var post = _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound("Post not found!");
            }
            return Ok(post);
        }

        [HttpGet]
        public IActionResult Get([FromBody] Student student)
        {
            var posts = _repository.GetPostsByStudentId(student.StudentId);
            if (posts == null || !posts.Any())
            {
                return NotFound("Posts not found!");
            }
            return Ok(posts);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Need a Post!");
            }

            _repository.CreatePost(post);

            return CreatedAtAction("Get", new { id = post.PostId }, post);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Post post)
        {
            if (post == null || post.PostId != id)
            {
                return BadRequest("Need a Post and ID!");
            }
            var postInDb = _repository.GetPostById(id);
            if (postInDb == null)
            {
                return NotFound("Post not found!");
            }
            _repository.UpdatePost(post);
            return Ok("Post updated!");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var postInDb = _repository.GetPostById(id);
            if (postInDb == null)
            {
                return NotFound("Post not found!");
            }
            _repository.DeletePost(postInDb);
            return NoContent();
        }
    }
}