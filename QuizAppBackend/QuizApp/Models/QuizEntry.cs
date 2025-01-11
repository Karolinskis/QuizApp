namespace QuizApp.Models;

public class QuizEntry
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int Score { get; set; }
    public DateTime CompletedAt { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
}
