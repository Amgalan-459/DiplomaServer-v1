using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Lesson {
        [Key] public int Id { get; set; }
        [Required] public string Title { get;set; }
        [Required] public bool Completed { get; set; } = false; 
        public string? VideoUrl { get; set; }
        [Required] public string Content { get; set; }
        [Required] public bool IsAvailable { get; set; }
        [Required] public int ModuleId { get; set; }

        public Lesson (string title, bool completed, string? videoUrl, string content, bool isAvailable, int moduleId) {
            Title = title;
            Completed = completed;
            VideoUrl = videoUrl;
            Content = content;
            IsAvailable = isAvailable;
            ModuleId = moduleId;
        }
    }
}
