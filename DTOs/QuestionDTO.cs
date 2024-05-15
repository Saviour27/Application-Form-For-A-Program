using Program_Application_Form.Models;

namespace Program_Application_Form.DTOs;
public class QuestionDTO
{
    public string Id { get; set; }
    public string CourseId { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public ICollection<string> Options { get; set; } = new List<string>();
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
}