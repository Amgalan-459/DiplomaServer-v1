using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Module {
        [Key] public int Id { get; set; }
        public int[] LessonIds { get; set; }
        public int[] TestIds {  get; set; }
    }
}
