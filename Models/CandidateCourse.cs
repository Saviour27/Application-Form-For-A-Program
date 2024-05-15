namespace Program_Application_Form.Models;
public class CandidateCourse
{
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }
    public string CourseId { get; set; }
    public Course Course { get; set; }
}
