using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models.DTOs;

public class QuizSubmissionDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
}
