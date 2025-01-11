using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetTopScores()
    {
        var topScores = _quizService.GetTopScores();

        return Ok(topScores);
    }
}
