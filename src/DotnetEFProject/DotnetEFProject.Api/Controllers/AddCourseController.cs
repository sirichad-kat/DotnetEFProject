using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEFProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCourseController : ControllerBase
    {
        private readonly IAddCourseRepository _addLessonRepository;

        public AddCourseController(IAddCourseRepository addLessonRepository)
        {
            _addLessonRepository = addLessonRepository;
        }

        [HttpPost]
        [Route("AddLesson")]
        public async Task<IActionResult> AddLesson([FromBody] AddCourseInput req, CancellationToken cancellationToken = default)
        {
            await _addLessonRepository.AddLessonAsync(req, cancellationToken);
            return Ok();
        }
    }
}
