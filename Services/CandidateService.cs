using AutoMapper;
using Program_Application_Form.Abstractions;
using Program_Application_Form.DTOs;
using Program_Application_Form.Models;

namespace Program_Application_Form.Services;
public class CandidateService : ICandidateService
{
    private readonly IRepository<Candidate> _candidateRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Answer> _answerRepository;
    private readonly IMapper _mapper;

    public CandidateService(IRepository<Candidate> candidateRepository, IRepository<Course> courseRepository,
                            IRepository<Question> questionRepository, IRepository<Answer> answerRepository,
                            IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _courseRepository = courseRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _mapper = mapper;
    }

    public async Task RegisterCandidateForCourseAsync(CandidateDTO candidateDto, string courseId, Dictionary<string, string> answers)
    {
        var candidate = _mapper.Map<Candidate>(candidateDto);
        var course = await _courseRepository.GetByIdAsync(courseId);

        if (candidate == null || course == null)
        {
            throw new Exception("Candidate or Course not found");
        }

        if (!candidate.CourseIds.Contains(courseId))
        {
            candidate.CourseIds.Add(courseId);
            await _candidateRepository.UpdateAsync(candidate);
        }

        if (!course.CandidateIds.Contains(candidate.Id))
        {
            course.CandidateIds.Add(candidate.Id);
            await _courseRepository.UpdateAsync(course);
        }

        var questions = await _questionRepository.GetAllAsync();
        foreach (var question in questions)
        {
            if (question.CourseId != courseId) continue;

            var answer = new Answer
            {
                Id = Guid.NewGuid().ToString(),
                CandidateId = candidate.Id,
                CourseId = courseId,
                QuestionId = question.Id
            };

            if (answers.TryGetValue(question.Id, out var answerValue))
            {
                switch (question.Type)
                {
                    case QuestionType.MultipleChoice:
                    case QuestionType.Dropdown:
                        answer.OptionAnswer = answerValue;
                        break;
                    case QuestionType.YesNo:
                        answer.YesNoAnswer = bool.Parse(answerValue);
                        break;
                    case QuestionType.Date:
                        answer.DateAnswer = DateTime.Parse(answerValue);
                        break;
                    case QuestionType.Number:
                        answer.NumberAnswer = double.Parse(answerValue);
                        break;
                    case QuestionType.Text:
                        answer.TextAnswer = answerValue;
                        break;
                }

                await _answerRepository.AddAsync(answer);
            }
        }
    }

    public async Task<CandidateDTO> GetCandidateAsync(string id)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        return _mapper.Map<CandidateDTO>(candidate);
    }

    public async Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync()
    {
        var candidates = await _candidateRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CandidateDTO>>(candidates);
    }
}