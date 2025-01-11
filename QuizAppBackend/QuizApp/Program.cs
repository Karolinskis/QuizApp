using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Services;
using QuizApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseInMemoryDatabase("QuizApp"));

builder.Services.AddControllers();

builder.Services.AddScoped<IQuizService, QuizService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

Helper.SetupDb(app);

app.Run();
