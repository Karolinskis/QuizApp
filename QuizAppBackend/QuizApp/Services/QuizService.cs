using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.DTOs;

namespace QuizApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public QuizService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public List<QuestionDto> GetQuestions()
        {
            var questions = _applicationContext.Questions.ToList();
            return _mapper.Map<List<QuestionDto>>(questions);
        }

        public int CalculateScore(QuizSubmissionDto submission)
        {
            int score = 0;

            foreach (var answer in submission.Answers)
            {
                var question = _applicationContext.Questions.Find(answer.QuestionId);

                if (question == null) continue;

                switch (question.Type)
                {
                    case QuestionType.RadioButton:
                    case QuestionType.Checkbox:
                        var goodAnswerCount = question.CorrectAnswer.Count;
                        var correctlyAnswered = question.CorrectAnswer.Count(answer.AnswerValue.Contains);
                        score += (int)Math.Ceiling(100.0 / goodAnswerCount * correctlyAnswered);
                        break;

                    case QuestionType.Textbox:
                        if (string.Equals(answer.AnswerValue.FirstOrDefault(), question.CorrectAnswer.FirstOrDefault(), StringComparison.OrdinalIgnoreCase))
                            score += 100;
                        break;
                }
            }

            return score;
        }

        public void AddQuizEntry(QuizEntry quizEntry)
        {
            _applicationContext.QuizEntries.Add(quizEntry);
            _applicationContext.SaveChanges();
        }

        public List<HighScoreDto> GetHighScores(int count)
        {
            var quizEntries = _applicationContext.QuizEntries
                .OrderByDescending(qe => qe.Score)
                .Take(count)
                .ToList();

            return _mapper.Map<List<HighScoreDto>>(quizEntries);
        }
    }
}
