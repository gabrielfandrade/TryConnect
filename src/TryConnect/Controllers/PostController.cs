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
        public IActionResult Get()
        {
            return Ok(_repository.GetPosts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet]
        public IActionResult Get([FromBody] Student student)
        {
            var posts = _repository.GetPostsByStudentId(student.StudentId);
            if (posts == null || !posts.Any())
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            _repository.CreatePost(post);

            return CreatedAtAction("Get", new { id = post.PostId }, post);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Post post)
        {
            if (post == null || post.PostId != id)
            {
                return BadRequest();
            }
            var postInDb = _repository.GetPostById(id);
            if (postInDb == null)
            {
                return NotFound();
            }
            _repository.UpdatePost(post);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var postInDb = _repository.GetPostById(id);
            if (postInDb == null)
            {
                return NotFound();
            }
            _repository.DeletePost(postInDb);
            return NoContent();
        }
    }
}