using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using QuizApp.Data;
using QuizApp.Models.DTOs;
using QuizApp.Models;

using QuizApp.Services;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;

    public QuizController(IQuizService quizService, IMapper mapper)
    {
        _quizService = quizService;
        _mapper = mapper;
    }

    [HttpPost("submit")]
    public IActionResult SubmitQuiz([FromBody] QuizSubmissionDto submission)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int score = _quizService.CalculateScore(submission);

        var quizEntry = _mapper.Map<QuizEntry>(submission);
        quizEntry.Score = score;
        quizEntry.CompletedAt = DateTime.Now;

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
