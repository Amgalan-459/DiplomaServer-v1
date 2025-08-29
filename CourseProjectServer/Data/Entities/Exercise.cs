using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class Exercise {
        [Key] public int Id {  get; set; }
        [Required] public string Name { get; set; }
        [Required] public int[] WeightPlan { get; set; }
        [Required] public int[] RepPlan { get; set; }
        public int[] WeightFact { get; set; }
        public int[] RepFact { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        [Required] public int WorkoutId {  get; set; }
        [Required] public Workout Workout { get; set; }

        public Exercise (int id, string name, int[] weightPlan, int[] repPlan, int[] weightFact, int[] repFact, string videoUrl, string description, int workoutId, Workout workout) {
            Id = id;
            Name = name;
            WeightPlan = weightPlan;
            RepPlan = repPlan;
            WeightFact = weightFact;
            RepFact = repFact;
            VideoUrl = videoUrl;
            Description = description;
            WorkoutId = workoutId;
            Workout = workout;
        }

        public Exercise () {
        }
    }
}
