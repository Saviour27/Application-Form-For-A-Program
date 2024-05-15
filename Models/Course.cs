﻿namespace Program_Application_Form.Models;
public class Course
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<string> CandidateIds { get; set; } = new List<string>();
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
