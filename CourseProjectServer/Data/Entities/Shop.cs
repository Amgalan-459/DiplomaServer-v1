using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Shop {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Price { get; set; }
        public string? Description { get; set; }
    }
}
