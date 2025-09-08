using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Lesson {
        [Key] public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string TextRaw { get; set; }
        [Required] public bool IsAvailable { get; set; }
    }
}
