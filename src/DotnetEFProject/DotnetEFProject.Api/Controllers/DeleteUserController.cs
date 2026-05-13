using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEFProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteUserController : ControllerBase
    {

        private readonly IDeleteUserRepository _deleteUserRepository;

        public DeleteUserController(IDeleteUserRepository deleteUserRepository)
        {
            _deleteUserRepository = deleteUserRepository;
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserInput req, CancellationToken ct = default)
        {

            await _deleteUserRepository.DeleteUserAsync(req, ct);
            return Ok();

        }
    }
}
