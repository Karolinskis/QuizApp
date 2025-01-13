using AutoMapper;
using QuizApp.Models;
using QuizApp.Models.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Question, QuestionDto>();

        CreateMap<QuizSubmissionDto, QuizEntry>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        CreateMap<AnswerDto, Answer>();

        CreateMap<QuizEntry, HighScoreDto>();
    }
}
