using QuizApp.Models;
using QuizApp.Models.DTOs;

namespace QuizApp.Services
{
    public interface IQuizService
    {
        List<QuestionDto> GetQuestions();
        int CalculateScore(QuizSubmissionDto submission);
        void AddQuizEntry(QuizEntry quizEntry);
        // TODO: Might want to add a count parameter
        List<object> GetTopScores(); // TODO: Shouldn't be an object
    }
}
