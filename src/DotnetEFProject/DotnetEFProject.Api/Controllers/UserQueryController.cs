using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEFProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQueryController : ControllerBase
    {
        private readonly IGetUserListViewReader _getUserListViewReader;
        private readonly IGetUserViewReader _getUserViewReader;

        public UserQueryController(IGetUserListViewReader getUserListViewReader, IGetUserViewReader getUserViewReader)
        {
            _getUserListViewReader = getUserListViewReader;
            _getUserViewReader = getUserViewReader;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] GetUserInput req, CancellationToken ct = default)
        {
            var result = await _getUserViewReader.GetUserAsync(req, ct);
            return Ok(result);
        } 

        [HttpGet]
        [Route("GetUserList")]
        public async Task<IActionResult> GetUserList(CancellationToken ct = default)
        {
            var result = await _getUserListViewReader.GetUserListViewAsync(ct);
            return Ok(result);
        }

    }
}
