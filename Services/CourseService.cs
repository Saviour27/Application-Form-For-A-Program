using AutoMapper;
using Program_Application_Form.Abstractions;
using Program_Application_Form.DTOs;
using Program_Application_Form.Models;

namespace Program_Application_Form.Services;
public class CourseService : ICourseService
{
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IMapper _mapper;

    public CourseService(IRepository<Course> courseRepository, IRepository<Question> questionRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task AddCourseAsync(CourseDTO courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);
        await _courseRepository.AddAsync(course);

        foreach (var questionDto in courseDto.Questions)
        {
            var question = _mapper.Map<Question>(questionDto);
            question.CourseId = course.Id;
            await _questionRepository.AddAsync(question);
        }
    }

    public async Task<CourseDTO> GetCourseAsync(string id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            return null;

        var questions = await _questionRepository.GetAllAsync();
        course.Questions = questions.Where(q => q.CourseId == id).ToList();

        return _mapper.Map<CourseDTO>(course);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        var coursesDto = _mapper.Map<IEnumerable<CourseDTO>>(courses);

        foreach (var courseDto in coursesDto)
        {
            var questions = await _questionRepository.GetAllAsync();
            courseDto.Questions = questions.Where(q => q.CourseId == courseDto.Id).ToList();
        }

        return coursesDto;
    }

    public async Task UpdateCourseAsync(CourseDTO courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);
        await _courseRepository.UpdateAsync(course);

        var existingQuestions = await _questionRepository.GetAllAsync();
        var questionsToRemove = existingQuestions.Where(q => q.CourseId == course.Id).ToList();
        foreach (var question in questionsToRemove)
        {
            await _questionRepository.DeleteAsync(question.Id);
        }

        foreach (var questionDto in courseDto.Questions)
        {
            var question = _mapper.Map<Question>(questionDto);
            question.CourseId = course.Id;
            await _questionRepository.AddAsync(question);
        }
    }

    public async Task DeleteCourseAsync(string id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course != null)
        {
            var questions = await _questionRepository.GetAllAsync();
            var questionsToDelete = questions.Where(q => q.CourseId == id).ToList();
            foreach (var question in questionsToDelete)
            {
                await _questionRepository.DeleteAsync(question.Id);
            }

            await _courseRepository.DeleteAsync(id);
        }
    }
}
