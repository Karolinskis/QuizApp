using QuizApp.Models;
using QuizApp.Models.DTOs;

namespace QuizApp.Services
{
    public interface IQuizService
    {
        List<QuestionDto> GetQuestions();
        int CalculateScore(QuizSubmissionDto submission);
        void AddQuizEntry(QuizEntry quizEntry);
        List<HighScoreDto> GetHighScores(int count);
    }
}
