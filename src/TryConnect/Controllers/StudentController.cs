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
        [AllowAnonymous]
        public IActionResult Get()
        {
            var students = _repository.GetStudents();

            if (students != null && students.Any())
            {
                foreach (var student in students)
                {
                    student.Password = string.Empty;
                }
            }

            return Ok(students);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var student = _repository.GetStudentById(id);
            if (student == null)
            {
                return NotFound("Student not found!");
            }

            student.Password = string.Empty;

            return Ok(student);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Need a Student!");
            }

            _repository.CreateStudent(student);

            student.Password = String.Empty;

            return CreatedAtAction("Get", new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            if (student == null || student.StudentId != id)
            {
                return BadRequest("Need a Student and ID!");
            }
            var studentInDb = _repository.GetStudentById(id);
            if (studentInDb == null)
            {
                return NotFound("Student not found!");
            }
            _repository.UpdateStudent(student);
            return Ok("Student updated!");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var studentInDb = _repository.GetStudentById(id);
            if (studentInDb == null)
            {
                return NotFound("Student not found!");
            }
            _repository.DeleteStudent(studentInDb);
            return NoContent();
        }
    }
}