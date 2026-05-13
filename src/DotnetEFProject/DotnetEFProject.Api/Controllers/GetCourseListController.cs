using DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEFProject.Api.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class GetCourseListController : ControllerBase
    {
        private readonly IGetCourseViewReader _getCourseViewReader;

        public GetCourseListController(IGetCourseViewReader getCourseViewReader)
        {
            _getCourseViewReader = getCourseViewReader;
        }

        [HttpPost]
        [Route("GetCourseList")]
        public async Task<IActionResult> GetCourseList(CancellationToken ct = default)
        {
           var res =  await _getCourseViewReader.GetCourseListAsync(ct);
            return Ok(res);
        }
    }
}
