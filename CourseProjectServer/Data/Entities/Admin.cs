using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Admin {
        [Key] public int Id {  get; set; }
        public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public bool IsActive { get; set; } = true;

        public Admin (int id, string name, string email, string password, bool isActive) {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            IsActive = isActive;
        }

        public Admin () {
        }
    }
}
