namespace Program_Application_Form.Models;
public class Answer
{
    public string Id { get; set; }
    public string CandidateId { get; set; }
    public string CourseId { get; set; }
    public string QuestionId { get; set; }
    public string TextAnswer { get; set; }
    public string OptionAnswer { get; set; }
    public bool? YesNoAnswer { get; set; }
    public DateTime? DateAnswer { get; set; }
    public double? NumberAnswer { get; set; }
}
