using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class UserInfo {
        [Key] public int Id { get; set; }
        [Required] public DateOnly DateOnly { get; set; }
        //тут параметры
    }
}
