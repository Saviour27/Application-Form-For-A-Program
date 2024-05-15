using Program_Application_Form.DTOs;

namespace Program_Application_Form.Abstractions;
public interface ICourseService
{
    Task AddCourseAsync(CourseDTO courseDto);
    Task<CourseDTO> GetCourseAsync(string id);
    Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
    Task UpdateCourseAsync(CourseDTO courseDto);
    Task DeleteCourseAsync(string id);
}