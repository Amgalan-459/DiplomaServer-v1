using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Trainee : User {
        [Key] public int Id {  get; set; }
        [Required] public string Name {  get; set; }
        [Required] public string Surname {  get; set; }
        [Required] public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Sex { get; set; } = 0; //0 - not stated, 1 - male, 2 - female
        public string Password { get; set; }
        public int CountOfTrainsInWeek {  get; set; }
        [Required] public bool IsActive { get; set; } = true;
        [Required] public int TrainerId {  get; set; }
        [Required] public Trainer Trainer { get; set; }
        [Required] public IList<Workout> Workouts { get; } = new List<Workout>();
        //потом добавить класс TraineeInfo, где будут его данные о весе, обхвате и процентах

        public Trainee (int id, string name, string surname, string email, string phoneNum, int sex, string pass, int countOfTrainsInWeek, bool isActive, int trainerId, Trainer uTrainer, IList<Workout> workouts) {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            CountOfTrainsInWeek = countOfTrainsInWeek;
            TrainerId = trainerId;
            Trainer = uTrainer;
            Workouts = workouts;
            IsActive = isActive;
            PhoneNumber = phoneNum;
            Sex = sex;
            Password = pass;
        }

        public Trainee () {
        }
    }
}
