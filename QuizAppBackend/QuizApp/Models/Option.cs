namespace QuizApp.Models;

public class Option
{
    public int Id { get; set; }
    public string Value { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
