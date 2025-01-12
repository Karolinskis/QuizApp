using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.DTOs;

namespace QuizApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationContext _applicationContext;
        public QuizService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public List<QuestionDto> GetQuestions()
        {
            return _applicationContext.Questions
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type.ToString(),
                    Options = q.Options
                })
                .ToList();
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
            return _applicationContext.QuizEntries
                .OrderByDescending(qe => qe.Score)
                .Take(count)
                .Select(qe => new HighScoreDto
                {
                    Email = qe.Email,
                    Score = qe.Score,
                    CompletedAt = qe.CompletedAt
                })
                .ToList();
        }
    }
}
