using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class ProgrammCourse {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public IList<int> WorkoutIds { get; set; }
    }
}
