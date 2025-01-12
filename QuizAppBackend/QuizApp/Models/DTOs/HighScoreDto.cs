namespace QuizApp.Models.DTOs
{
    public class HighScoreDto
    {
        public required string Email { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
