namespace Program_Application_Form.Models;
public class Question
{
    public string Id { get; set; }
    public string CourseId { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public ICollection<string> Options { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
}

public enum QuestionType
{
    MultipleChoice,
    YesNo,
    Date,
    Number,
    Text,
    Dropdown
}
