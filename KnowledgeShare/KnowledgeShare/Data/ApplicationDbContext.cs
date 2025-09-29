//using KnowledgeShare.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//namespace KnowledgeShare.Data
//{
//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        // Add our custom models to the database context
//        public DbSet<Subject> Subjects { get; set; }
//        public DbSet<SessionRequest> SessionRequests { get; set; }
//        public DbSet<ApplicationUser> Users { get; set; }
//    }
//}

using KnowledgeShare.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.Data
{
    // We inherit from IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // These are correct.
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SessionRequest> SessionRequests { get; set; }

        // The DbSet for ApplicationUser has been REMOVED. This is essential.
    }
}

