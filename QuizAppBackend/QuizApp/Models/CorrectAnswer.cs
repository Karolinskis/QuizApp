namespace QuizApp.Models;

public class CorrectAnswer
{
    public int Id { get; set; }
    public required string Value { get; set; }
    public int QuestionId { get; set; }
    public required Question Question { get; set; }
}
