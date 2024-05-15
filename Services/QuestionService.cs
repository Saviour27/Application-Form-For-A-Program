using AutoMapper;
using Program_Application_Form.Abstractions;
using Program_Application_Form.DTOs;
using Program_Application_Form.Models;

namespace Program_Application_Form.Services;
public class QuestionService : IQuestionService
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IMapper _mapper;

    public QuestionService(IRepository<Question> questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task AddQuestionAsync(QuestionDTO questionDto)
    {
        var question = _mapper.Map<Question>(questionDto);
        await _questionRepository.AddAsync(question);
    }

    public async Task DeleteQuestionAsync(string id)
    {
        await _questionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<QuestionDTO>> GetAllQuestionsAsync()
    {
        var questions = await _questionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<QuestionDTO>>(questions);
    }

    public async Task<QuestionDTO> GetQuestionAsync(string id)
    {
        var question = await _questionRepository.GetByIdAsync(id);
        return _mapper.Map<QuestionDTO>(question);
    }

    public async Task UpdateQuestionAsync(string id, QuestionDTO questionDto)
    {
        var existingQuestion = await _questionRepository.GetByIdAsync(id);
        if (existingQuestion == null)
        {
            throw new ArgumentException("Question not found");
        }

        var updatedQuestion = _mapper.Map(questionDto, existingQuestion);
        await _questionRepository.UpdateAsync(updatedQuestion);
    }
}