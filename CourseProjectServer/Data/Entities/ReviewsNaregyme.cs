using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class ReviewsNaregyme {
        [Key] public int Id { get; set; }
        [Required] public string Text { get; set; }
        [Required] public string UserName { get; set; }
    }
}
