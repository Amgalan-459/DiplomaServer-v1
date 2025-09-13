using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Test {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public bool IsCompleted { get; set; }
        [Required] public string Url { get; set; }
        [Required] public bool IsAvailable { get; set; } = true;
        [Required] public int ModuleId { get; set; }

        public Test (string title, bool isCompleted, string url, bool isAvailable, int moduleId) {
            Title = title;
            IsCompleted = isCompleted;
            Url = url;
            IsAvailable = isAvailable;
            ModuleId = moduleId;
        }
    }
}
