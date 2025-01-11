using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizEntry> QuizEntries { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Text).IsRequired();
                entity.Property(q => q.Type).IsRequired();
                entity.Property(q => q.Options).IsRequired();
                entity.Property(q => q.CorrectAnswer).IsRequired();
            });

            modelBuilder.Entity<QuizEntry>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Email).IsRequired();
                entity.Property(q => q.Score).IsRequired();
                entity.Property(q => q.CompletedAt).IsRequired();
                entity.HasMany(q => q.Answers)
                    .WithOne()
                    .HasForeignKey(a => a.QuizEntryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.AnswerValue).IsRequired();
                entity.HasOne<Question>()
                    .WithMany()
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
