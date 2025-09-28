using System.ComponentModel.DataAnnotations;

namespace KnowledgeShare.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TutorId { get; set; } // Foreign key to ApplicationUser Id
    }
}
