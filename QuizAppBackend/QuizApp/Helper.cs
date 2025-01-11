using QuizApp.Data;

namespace QuizApp
{
    public class Helper
    {
        public static void SetupDb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationContext>();
                context.Database.EnsureCreated();
                if (!context.Questions.Any())
                {
                    var questions = new List<Question>
                    {
                        // Single answer questions
                        new Question
                        {
                            Text = "What is the capital of France?",
                            Type = QuestionType.RadioButton,
                            Options = new List<string> { "Paris", "London", "Berlin", "Madrid" },
                            CorrectAnswer = new List<string> { "Paris" }
                        },
                        new Question
                        {
                            Text = "What is 2 + 2?",
                            Type = QuestionType.RadioButton,
                            Options = new List<string> { "3", "4", "5", "6" },
                            CorrectAnswer = new List<string> { "4" }
                        },
                        new Question
                        {
                            Text = "Which planet is known as the Red Planet?",
                            Type = QuestionType.RadioButton,
                            Options = new List<string> { "Earth", "Mars", "Jupiter", "Saturn" },
                            CorrectAnswer = new List<string> { "Mars" }
                        },
                        new Question
                        {
                            Text = "What is the largest ocean on Earth?",
                            Type = QuestionType.RadioButton,
                            Options = new List<string> { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean", "Pacific Ocean" },
                            CorrectAnswer = new List<string> { "Pacific Ocean" }
                        },

                        // Text input questions
                        new Question
                        {
                            Text = "Who wrote 'To Kill a Mockingbird'?",
                            Type = QuestionType.Textbox,
                            CorrectAnswer = new List<string> { "Harper Lee" }
                        },
                        new Question
                        {
                            Text = "What is the chemical symbol for water?",
                            Type = QuestionType.Textbox,
                            CorrectAnswer = new List<string> { "H2O" }
                        },

                        // Multiple answer questions
                        new Question
                        {
                            Text = "Which of the following are programming languages?",
                            Type = QuestionType.Checkbox,
                            Options = new List<string> { "Python", "HTML", "Java", "CSS" },
                            CorrectAnswer = new List<string> { "Python", "Java" }
                        },
                        new Question
                        {
                            Text = "Which of the following are fruits?",
                            Type = QuestionType.Checkbox,
                            Options = new List<string> { "Apple", "Carrot", "Banana", "Potato" },
                            CorrectAnswer = new List<string> { "Apple", "Banana" }
                        },
                        new Question
                        {
                            Text = "Which of the following are prime numbers?",
                            Type = QuestionType.Checkbox,
                            Options = new List<string> { "2", "3", "4", "5" },
                            CorrectAnswer = new List<string> { "2", "3", "5" }
                        },
                        new Question
                        {
                            Text = "Which of the following are colors?",
                            Type = QuestionType.Checkbox,
                            Options = new List<string> { "Red", "Dog", "Blue", "Cat" },
                            CorrectAnswer = new List<string> { "Red", "Blue" }
                        }
                    };

                    context.Questions.AddRange(questions);
                    context.SaveChanges();
                }
            }
        }
    }
}
