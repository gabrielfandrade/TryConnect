using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : Controller
    {
        private readonly ITryConnectRepository _repository;

        public StudentController(ITryConnectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _repository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            _repository.CreateStudent(student);

            return CreatedAtAction("Get", new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            if (student == null || student.StudentId != id)
            {
                return BadRequest();
            }
            var studentInDb = _repository.GetStudentById(id);
            if (studentInDb == null)
            {
                return NotFound();
            }
            _repository.UpdateStudent(student);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var studentInDb = _repository.GetStudentById(id);
            if (studentInDb == null)
            {
                return NotFound();
            }
            _repository.DeleteStudent(studentInDb);
            return NoContent();
        }
    }
}