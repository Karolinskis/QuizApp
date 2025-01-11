using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models.DTOs
{
    public class AnswerDto
    {
        [Required]
        public int QuestionId { get; set; }
        public List<string> AnswerValue { get; set; } = new List<string>();
    }
}
