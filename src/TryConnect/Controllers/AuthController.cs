using Microsoft.AspNetCore.Mvc;
using TryConnect.Models;
using TryConnect.Repository;
using TryConnect.Services;
using TryConnect.ViewModels;

namespace TryConnect.Controllers
{
    [ApiController]
    [Route("login")]
    public class AuthController : Controller
    {
        private readonly ITryConnectRepository _repository;

        public AuthController(ITryConnectRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<StudentViewModel> Authenticate([FromBody] Login login)
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            try
            {
                studentViewModel.student = _repository.GetStudent(login);

                if (studentViewModel.student == null)
                {
                    return NotFound("User not found!");
                }

                studentViewModel.Token = new TokenGenerator().Generate(studentViewModel.student);

                studentViewModel.student.Password = string.Empty;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return studentViewModel;
        }
    }
}