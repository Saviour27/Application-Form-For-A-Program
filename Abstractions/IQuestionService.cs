using Program_Application_Form.DTOs;

namespace Program_Application_Form.Abstractions;
public interface IQuestionService
{
    Task<QuestionDTO> GetQuestionAsync(string id);
    Task<IEnumerable<QuestionDTO>> GetAllQuestionsAsync();
    Task AddQuestionAsync(QuestionDTO questionDto);
    Task UpdateQuestionAsync(string id, QuestionDTO questionDto);
    Task DeleteQuestionAsync(string id);
}