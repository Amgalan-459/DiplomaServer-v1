using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Course {
        [Key] public int Id { get; set; }
        public int[] ModuleIds { get; set; }
        [Required] public string Instructions { get; set; }
    }
}
