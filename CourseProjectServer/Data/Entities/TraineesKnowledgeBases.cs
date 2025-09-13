using System.ComponentModel.DataAnnotations;

namespace CourseProjectServer.Data.Entities {
    public class TraineesKnowledgeBases {
        [Key] public int Id { get; set; }
        [Required] public int TraineeId { get; set; }
        [Required] public int KnowledgeId { get; set; }

        public TraineesKnowledgeBases (int traineeId, int knowledgeId) {
            TraineeId = traineeId;
            KnowledgeId = knowledgeId;
        }
    }
}
