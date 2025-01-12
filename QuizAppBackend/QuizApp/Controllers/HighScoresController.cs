using Microsoft.AspNetCore.Mvc;
using QuizApp.Models.DTOs;
using QuizApp.Services;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class HighScoresController : ControllerBase
{
    private readonly IQuizService _quizService;

    public HighScoresController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet]
    public ActionResult<List<HighScoreDto>> GetHighScores([FromQuery] int count = 10)
    {
        var topScores = _quizService.GetHighScores(count);

        return Ok(topScores);
    }
}
