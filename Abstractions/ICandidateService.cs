using Program_Application_Form.DTOs;

namespace Program_Application_Form.Abstractions;
public interface ICandidateService
{
    Task RegisterCandidateForCourseAsync(CandidateDTO candidateDto, string courseId, Dictionary<string, string> answers);
    Task<CandidateDTO> GetCandidateAsync(string id);
    Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync();
}
