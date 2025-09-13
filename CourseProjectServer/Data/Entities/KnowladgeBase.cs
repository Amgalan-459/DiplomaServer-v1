using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class KnowladgeBase {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Author { get; set; }
        [Required] public int TrainerId { get; set; } = 1;
        [Required] public string Topic { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Type { get; set; }
        [Required] public string TextRaw { get; set; }
        public string? VideoUrl { get; set; }

        public KnowladgeBase (string title, string author, int trainerId, string topic, string description,
                string type, string textRaw, string? videoUrl) {
            Title = title;
            Author = author;
            TrainerId = trainerId;
            Topic = topic;
            Description = description;
            Type = type;
            TextRaw = textRaw;
            VideoUrl = videoUrl;
        }
    }
}
