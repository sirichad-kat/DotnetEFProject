using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEFProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModifyUserController : ControllerBase
    {

        private readonly IModifyUserRepository _modifyUserRepository; 

        public ModifyUserController(IModifyUserRepository modifyUserRepository)
        {
            _modifyUserRepository = modifyUserRepository; 
        }

        [HttpPatch]
        [Route("ModifyUser")]
        public async Task<IActionResult> ModifyUser([FromBody] ModifyUserInput req, CancellationToken ct = default)
        {

            await _modifyUserRepository.ModifyUserAsync(req, ct);
            return Ok();

        }
    }
}
