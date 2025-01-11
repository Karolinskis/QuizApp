namespace QuizApp.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public List<string> AnswerValue { get; set; } = new List<string>();
    public int QuizEntryId { get; set; }
}
