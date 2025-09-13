using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Module {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public bool Expanded { get; set; }
        [Required] public bool IsAvailable { get; set; } = true;
        [Required] public int CourseId { get; set; }

        public Module (string title, bool expanded, bool isAvailable, int courseId) {
            Title = title;
            Expanded = expanded;
            IsAvailable = isAvailable;
            CourseId = courseId;
        }
    }
}
