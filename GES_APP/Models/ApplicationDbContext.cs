using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using GES_APP.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GES_APP.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ManagerStaffViewModel managerStaffViewModels { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<TeachingTopic> TeachingTopics { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}