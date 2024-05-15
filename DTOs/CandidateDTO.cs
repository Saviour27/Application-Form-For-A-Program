using System.ComponentModel.DataAnnotations;

namespace Program_Application_Form.DTOs;
public class CandidateDTO
{
    [Required] public string Id { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public string Nationality { get; set; }
    [Required] public string CurrentResidence { get; set; }
    [Required] public string IdNumber { get; set; }
    [Required] public DateTime DateOfBirth { get; set; }
    [Required] public string Gender { get; set; }
    public ICollection<string> CourseIds { get; set; } = new List<string>();
    public ICollection<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
}
