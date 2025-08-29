using CourseProjectServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseProjectServer.Data.Context {
    public class CourseDbContext : DbContext {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ExerciseRaw> ExerciseRaws { get; set; }
        public DbSet<KnowladgeBase> KnowladgeBases { get; set; }

        public CourseDbContext (DbContextOptions options) : base(options) {
        }

        public CourseDbContext () {
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=diploma;Username=amga;Password=amga;");
        }
    }
}
