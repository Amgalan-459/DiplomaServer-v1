using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Workout {
        [Key] public int Id { get; set; }
        [Required] public DateOnly Date { get; set; }
        [Required] public IList<Exercise> Exercises { get; } = new List<Exercise>();
        [Required] public bool IsAvailable { get; set; }
        [Required] public int TraineeId {  get; set; }
        [Required] public Trainee Trainee { get; set; }
        //привязка к тренеру через ученика

        public Workout (int id, IList<Exercise> exercises,int traineeId, Trainee trainee, DateOnly date) {
            Id = id;
            Exercises = exercises;
            TraineeId = traineeId;
            Trainee = trainee;
            Date = date;
        }

        public Workout () {
        }
    }
}
