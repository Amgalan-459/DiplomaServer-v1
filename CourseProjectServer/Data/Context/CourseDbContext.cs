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
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TraineesKnowledgeBases> TraineesKnowledgeBasess { get; set; }

        public CourseDbContext (DbContextOptions options) : base(options) {
        }

        public CourseDbContext () {
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=dpg-d2ov6g24d50c73aeji20-a.oregon-postgres.render.com;Port=5432;Database=dbdiploma;Username=amga;Password=ELxWKDMnCRbrTaE1NA7kdYZyT5cZqXhv;");
        }
    }
}
