using Microsoft.AspNetCore.Mvc;
using Program_Application_Form.Abstractions;
using Program_Application_Form.DTOs;

namespace Program_Application_Form.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public RegistrationController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] CandidateDTO candidateDto, string courseId, [FromBody] Dictionary<string, string> answers)
    {
        try
        {
            await _candidateService.RegisterCandidateForCourseAsync(candidateDto, courseId, answers);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCandidate(string id)
    {
        var candidateDto = await _candidateService.GetCandidateAsync(id);
        if (candidateDto == null)
        {
            return NotFound();
        }
        return Ok(candidateDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCandidates()
    {
        var candidates = await _candidateService.GetAllCandidatesAsync();
        return Ok(candidates);
    }
}
