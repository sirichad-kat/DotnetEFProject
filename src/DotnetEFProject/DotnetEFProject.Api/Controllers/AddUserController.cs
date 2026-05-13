using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEFProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUserController : ControllerBase
    {
        private readonly IAddUserRepository _addUserRepository;

        public AddUserController(IAddUserRepository addUserRepository)
        {
            _addUserRepository = addUserRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserInput req, CancellationToken cancellationToken = default)
        {
            await _addUserRepository.AddUserAsync(req, cancellationToken);
            return Ok();
        }
    }
}
