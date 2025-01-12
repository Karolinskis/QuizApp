namespace QuizApp.Models.DTOs
{
    public class HighScoreDto
    {
        public string Email { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
