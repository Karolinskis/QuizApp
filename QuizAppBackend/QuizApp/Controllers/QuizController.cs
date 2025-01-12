using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using QuizApp.Data;
using QuizApp.Models.DTOs;
using QuizApp.Models;

using QuizApp.Services;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost("submit")]
    public IActionResult SubmitQuiz([FromBody] QuizSubmissionDto submission)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int score = _quizService.CalculateScore(submission);

        var quizEntry = new QuizEntry
        {
            Email = submission.Email,
            Score = score,
            CompletedAt = DateTime.UtcNow,
            Answers = submission.Answers.Select(a => new Answer
            {
                QuestionId = a.QuestionId,
                AnswerValue = a.AnswerValue
            }).ToList()
        };
        _quizService.AddQuizEntry(quizEntry);

        return Ok(new { Score = score });
    }

    [HttpGet("questions")]
    public ActionResult<List<QuestionDto>> GetQuestions()
    {
        var questions = _quizService.GetQuestions();
        return Ok(questions);
    }
}
