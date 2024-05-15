using Microsoft.AspNetCore.Mvc;
using Program_Application_Form.Abstractions;
using Program_Application_Form.DTOs;

namespace Program_Application_Form.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] CourseDTO courseDto)
    {
        await _courseService.AddCourseAsync(courseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = courseDto.Id }, courseDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(string id)
    {
        var course = await _courseService.GetCourseAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(string id, [FromBody] CourseDTO courseDto)
    {
        if (id != courseDto.Id)
        {
            return BadRequest();
        }

        await _courseService.UpdateCourseAsync(courseDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(string id)
    {
        await _courseService.DeleteCourseAsync(id);
        return NoContent();
    }
}