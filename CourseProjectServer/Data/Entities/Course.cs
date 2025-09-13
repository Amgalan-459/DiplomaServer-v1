using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Course {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Author { get; set; }
        [Required] public float Raiting { get; set; }
        [Required] public string ProgressText { get; set; }
        [Required] public string Type { get; set; }
        [Required] public string Image { get; set; }
        [Required] public bool IsBuyed { get; set; }
        [Required] public string Instructions { get; set; }
        [Required] public bool IsAvaibale { get; set; } = true;
        [Required] public int TrainerId { get; set; }
        [Required] public int UserId { get; set; }

        public Course (string title, string author, float raiting, string progressText,
                string type, string image, bool isBuyed, string instructions, bool isAvaibale, int trainerId, int userId) {
            Title = title;
            Author = author;
            Raiting = raiting;
            ProgressText = progressText;
            Type = type;
            Image = image;
            IsBuyed = isBuyed;
            Instructions = instructions;
            IsAvaibale = isAvaibale;
            TrainerId = trainerId;
            UserId = userId;
        }
    }
}
