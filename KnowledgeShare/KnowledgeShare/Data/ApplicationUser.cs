//using Microsoft.AspNetCore.Identity;
//using System.ComponentModel.DataAnnotations;

//namespace KnowledgeShare.Data
//{
//    public class ApplicationUser : IdentityUser
//    {
//        [Required] // Makes the Name field mandatory during registration
//        public string Name { get; set; }

//        [Required] // Makes the Role field mandatory
//        public string Role { get; set; } // Will store "Tutor" or "Learner"

//        public string? Bio { get; set; } // The '?' makes the Bio optional (nullable)
//    }
//}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeShare.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        // The '?' has been removed to be compatible with .NET Core 3.1 defaults.
        public string Bio { get; set; }
    }
}

