using Xunit;
using Moq;
using QuizApp.Services;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.UnitTests;

public class QuizServiceTests : IDisposable
{
    private readonly QuizService _quizService;
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public QuizServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "QuizAppTest")
            .Options;

        _context = new ApplicationContext(options);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();

        _quizService = new QuizService(_context, _mapper);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public void GetQuestions_ReturnsAllQuestions()
    {
        // Arrange
        var questions = new List<Question>
        {
            new Question { Id = 1, Text = "Question 1", Type = QuestionType.RadioButton, Options = new List<string> { "Option 1", "Option 2" }, CorrectAnswer = new List<string> { "Option 1" } },
            new Question { Id = 2, Text = "Question 2", Type = QuestionType.Checkbox, Options = new List<string> { "Option 1", "Option 2" }, CorrectAnswer = new List<string> { "Option 1" } }
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var result = _quizService.GetQuestions();

        // Assert
        Assert.Equal(questions.Count, result.Count);

        for (int i = 0; i < questions.Count; i++)
        {
            Assert.Equal(questions[i].Id, result[i].Id);
            Assert.Equal(questions[i].Text, result[i].Text);
            Assert.Equal(questions[i].Type.ToString(), result[i].Type);
            Assert.Equal(questions[i].Options, result[i].Options);
        }
    }

    [Fact]
    public void GetQuestions_ReturnsEmptyList_WhenNoQuestionsAvailable()
    {
        // Act
        var result = _quizService.GetQuestions();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void CalculateScore_ShouldReturnCorrectScore_ForRadioButtonAndCheckboxQuestions()
    {
        // Arrange
        var questions = new List<Question>
        {
            new Question
            {
                Id = 1,
                Text = "Question 1",
                Type = QuestionType.RadioButton,
                CorrectAnswer = new List<string> { "A" }
            },
            new Question
            {
                Id = 2,
                Text = "Question 2",
                Type = QuestionType.Checkbox,
                CorrectAnswer = new List<string> { "A", "B" }
            }
        };

        var submission = new QuizSubmissionDto
        {
            Email = "test@example.com",
            Answers = new List<AnswerDto>
            {
                new AnswerDto { QuestionId = 1, AnswerValue = new List<string> { "A" } },
                new AnswerDto { QuestionId = 2, AnswerValue = new List<string> { "A", "B" } }
            }
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var score = _quizService.CalculateScore(submission);

        // Assert
        Assert.Equal(200, score);
    }

    [Fact]
    public void CalculateScore_ShouldReturnCorrectScore_ForPartialyCorrectCheckboxAnswers()
    {
        // Arrange
        var questions = new List<Question>
        {
            new Question
            {
                Id = 1,
                Text = "Question 1",
                Type = QuestionType.Checkbox,
                CorrectAnswer = new List<string> { "A", "B" }
            }
        };

        var submission = new QuizSubmissionDto
        {
            Email = "test@example.com",
            Answers = new List<AnswerDto>
            {
                new AnswerDto { QuestionId = 1, AnswerValue = new List<string> { "A" } }
            }
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var score = _quizService.CalculateScore(submission);

        // Assert
        Assert.Equal(50, score);
    }

    [Fact]
    public void CalculateScore_ShouldReturnCorrectScore_ForTextboxQuestions()
    {
        // Arrange
        var questions = new List<Question>
        {
            new Question
            {
                Id = 1,
                Text = "Question 1",
                Type = QuestionType.Textbox,
                CorrectAnswer = new List<string> { "Correct Answer" }
            }
        };

        var submission = new QuizSubmissionDto
        {
            Email = "test@example.com",
            Answers = new List<AnswerDto>
            {
                new AnswerDto { QuestionId = 1, AnswerValue = new List<string> { "Correct Answer" } }
            }
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var score = _quizService.CalculateScore(submission);

        // Assert
        Assert.Equal(100, score);
    }

    [Fact]
    public void CalculateScore_ShouldReturnZero_ForIncorrectTextBoxAnswers()
    {
        // Arrange
        var questions = new List<Question>
        {
            new Question
            {
                Id = 1,
                Text = "Question 1",
                Type = QuestionType.Textbox,
                CorrectAnswer = new List<string> { "Correct Answer" }
            }
        };

        var submission = new QuizSubmissionDto
        {
            Email = "test@example.com",
            Answers = new List<AnswerDto>
            {
                new AnswerDto { QuestionId = 1, AnswerValue = new List<string> { "Incorrect Answer" } }
            }
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var score = _quizService.CalculateScore(submission);

        // Assert
        Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateScore_ShouldReturnCorrectScore_ForMixedAnswers()
    {
        // Arrange
        var questions = new List<Question>
    {
        new Question { Id = 1, Text = "Question 1", Type = QuestionType.RadioButton, Options = new List<string> { "Option 1", "Option 2" }, CorrectAnswer = new List<string> { "Option 1" } },
        new Question { Id = 2, Text = "Question 2", Type = QuestionType.Checkbox, Options = new List<string> { "Option 1", "Option 2" }, CorrectAnswer = new List<string> { "Option 1" } }
    };

        var submission = new QuizSubmissionDto
        {
            Email = "test@example.com",
            Answers = new List<AnswerDto>
            {
                new AnswerDto { QuestionId = 1, AnswerValue = new List<string> { "Option 1" } },
                new AnswerDto { QuestionId = 2, AnswerValue = new List<string> { "Option 2" } }
            }
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var score = _quizService.CalculateScore(submission);

        // Assert
        Assert.Equal(100, score);
    }

    [Fact]
    public void CalculateScore_ShouldReturnZero_WhenNoAnswersProvided()
    {
        // Arrange
        var questions = new List<Question>
        {
            new Question
            {
                Id = 1,
                Text = "Question 1",
                Type = QuestionType.RadioButton,
                CorrectAnswer = new List<string> { "A" }
            }
        };

        var submission = new QuizSubmissionDto
        {
            Email = "test@example.com",
            Answers = new List<AnswerDto>()
        };

        _context.Questions.AddRange(questions);
        _context.SaveChanges();

        // Act
        var score = _quizService.CalculateScore(submission);

        // Assert
        Assert.Equal(0, score);
    }

    [Fact]
    public void AddQuizEntry_ShouldAddQuizEntry()
    {
        // Arrange
        var quizEntry = new QuizEntry
        {
            Id = 1,
            Email = "test@example.com",
            Score = 100,
            CompletedAt = DateTime.UtcNow,
        };

        // Act
        _quizService.AddQuizEntry(quizEntry);

        // Assert
        var result = _context.QuizEntries.Find(1);

        Assert.NotNull(result);
    }

    [Fact]
    public void GetHighScores_ReturnsCorrectHighScores()
    {
        // Arrange
        var quizEntries = new List<QuizEntry>
        {
            new QuizEntry { Email = "user1@example.com", Score = 90, CompletedAt = DateTime.Now.AddDays(-1) },
            new QuizEntry { Email = "user2@example.com", Score = 95, CompletedAt = DateTime.Now.AddDays(-2) },
            new QuizEntry { Email = "user3@example.com", Score = 85, CompletedAt = DateTime.Now.AddDays(-3) }
        };

        _context.QuizEntries.AddRange(quizEntries);
        _context.SaveChanges();

        // Act
        var result = _quizService.GetHighScores(2);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(95, result[0].Score);
        Assert.Equal(90, result[1].Score);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void GetHighScores_ReturnsCorrectNumberOfHighScores(int count, int expectedCount)
    {
        // Arrange
        var quizEntries = new List<QuizEntry>
        {
            new QuizEntry { Email = "user1@example.com", Score = 90, CompletedAt = DateTime.Now.AddDays(-1) },
            new QuizEntry { Email = "user2@example.com", Score = 95, CompletedAt = DateTime.Now.AddDays(-2) },
            new QuizEntry { Email = "user3@example.com", Score = 85, CompletedAt = DateTime.Now.AddDays(-3) }
        };

        _context.QuizEntries.AddRange(quizEntries);
        _context.SaveChanges();

        // Act
        var result = _quizService.GetHighScores(count);

        // Assert
        Assert.Equal(expectedCount, result.Count);
    }
}
