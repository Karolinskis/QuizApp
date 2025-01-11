using QuizApp.Models;

public enum QuestionType
{
    RadioButton,
    Checkbox,
    Textbox
}

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public List<string> Options { get; set; } = new List<string>();
    public List<string> CorrectAnswer { get; set; } = new List<string>();
}
