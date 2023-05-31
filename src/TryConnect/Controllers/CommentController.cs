using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly ITryConnectRepository _repository;

        public CommentController(ITryConnectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            return Ok(_repository.GetCommentsByPostId(id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] PostComment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            _repository.CreateComment(comment);

            return CreatedAtAction("Get", new { id = comment.PostCommentId}, comment);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] PostComment comment)
        {
            if (comment == null || comment.PostCommentId != id)
            {
                return BadRequest();
            }
            var commentInDb = _repository.GetCommenById(id);
            if (commentInDb == null)
            {
                return NotFound();
            }
            _repository.UpdateComment(comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var commentInDb = _repository.GetCommenById(id);
            if (commentInDb == null)
            {
                return NotFound();
            }
            _repository.DeleteComment(commentInDb);
            return Ok();
        }
    }
}