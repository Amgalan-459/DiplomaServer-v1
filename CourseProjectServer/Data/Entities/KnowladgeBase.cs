using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class KnowladgeBase {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Author { get; set; }
        [Required] public string Topic { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Type { get; set; }
        [Required] public string TextRaw { get; set; }
        public string? VideoUrl { get; set; }
    }
}
