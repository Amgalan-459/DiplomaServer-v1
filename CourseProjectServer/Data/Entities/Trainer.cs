using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Trainer : User {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int Sex { get; set; } = 0; //0 - not stated, 1 - male, 2 - female
        [Required] public bool IsActive { get; set; } = true;
        [Required] public IList<Trainee> Trainees { get;}  = new List<Trainee>();

        public Trainer (int id, string name, string surname, string email, string password, bool isActive, IList<Trainee> trainees) {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Trainees = trainees;
            IsActive = isActive;
        }

        public Trainer () {
        }
    }
}
