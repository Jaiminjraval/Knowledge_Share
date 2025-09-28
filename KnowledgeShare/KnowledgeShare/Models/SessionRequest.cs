using System.ComponentModel.DataAnnotations;

namespace KnowledgeShare.Models
{
    public enum RequestStatus { Pending, Approved, Declined }
    public class SessionRequest
    {
        public int Id { get; set; }

        [Required]
        public string LearnerId { get; set; }

        [Required]
        public string TutorId { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;
    }
}
