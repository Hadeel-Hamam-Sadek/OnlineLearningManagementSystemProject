using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineLearningManagementSystemProject.Models
{
    public class LMSContext : IdentityDbContext
    {
        public LMSContext(DbContextOptions<LMSContext> options)
      : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StaffCourse> StaffCourses { get; set; }
        public DbSet<Material> Materials { get; set; }
        //public DbSet<Assignment> Assignments { get; set; }
        //public DbSet<StudentFile> studentFiles { get; set; }
    }
}
