using Microsoft.AspNetCore.Mvc;
using Program_Application_Form.Abstractions;
using Program_Application_Form.DTOs;

namespace Program_Application_Form.Controllers;
[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion(string id)
    {
        var questionDto = await _questionService.GetQuestionAsync(id);
        if (questionDto == null)
        {
            return NotFound();
        }
        return Ok(questionDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQuestions()
    {
        var questions = await _questionService.GetAllQuestionsAsync();
        return Ok(questions);
    }

    [HttpPost]
    public async Task<IActionResult> AddQuestion([FromBody] QuestionDTO questionDto)
    {
        await _questionService.AddQuestionAsync(questionDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDTO questionDto)
    {
        try
        {
            await _questionService.UpdateQuestionAsync(id, questionDto);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(string id)
    {
        await _questionService.DeleteQuestionAsync(id);
        return Ok();
    }
}