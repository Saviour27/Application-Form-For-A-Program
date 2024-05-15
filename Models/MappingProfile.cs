using AutoMapper;
using Program_Application_Form.DTOs;

namespace Program_Application_Form.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Candidate, CandidateDTO>().ReverseMap();
        CreateMap<Course, CourseDTO>().ReverseMap();
        CreateMap<Question, QuestionDTO>().ReverseMap();
        CreateMap<Answer, AnswerDTO>().ReverseMap();
    }
}
