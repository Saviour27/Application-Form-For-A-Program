namespace Program_Application_Form.Models;
public class Candidate
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Nationality { get; set; }
    public string CurrentResidence { get; set; }
    public string IdNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    public ICollection<string> CourseIds { get; set; } = new List<string>();
}
