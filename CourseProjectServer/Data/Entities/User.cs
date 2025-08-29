using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public interface User {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Sex { get; set; } //0 - not stated, 1 - male, 2 - female
        public bool IsActive { get; set; }
    }
}
