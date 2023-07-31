using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public class StaffCourseRepository : IStaffCourseRepository
    {
        LMSContext context;

        public StaffCourseRepository(LMSContext _context)
        {
            context = _context;
        }

        public List<StaffCourseViewModel> AddCoursesToStaff(int staffid)
        {
            var stff = context.Staffs.FirstOrDefault(s => s.StaffId == staffid);
            var availableCourses = context.Courses.Where(c => !c.staffcourses.Any(sc => sc.StaffId == staffid));

            var vmdList = availableCourses.Select(c => new StaffCourseViewModel
            {
                StaffId = staffid,
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode
            }).ToList();

            return vmdList;
        }

        public int AddCoursesToStaff(int staffid, int courseId)
        {
            var staff = context.Staffs.FirstOrDefault(s => s.StaffId == staffid);
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (staff != null && course != null)
            {
                var staffcourse = new Models.StaffCourse
                {
                    StaffId = staff.StaffId,
                    CourseID = course.CourseId,
                    Staff = staff,
                    course = course
                };

                context.StaffCourses.Add(staffcourse);
                int raw = context.SaveChanges();
                return raw;
            }
            return 0;
        }
        public List<CourseViewModel> GetCoursesStaff(string staffUserId)
        {
            var staff = context.Staffs.FirstOrDefault(s => s.UserID == staffUserId);
            var availableCourses = context.Courses.Where(c => c.staffcourses.Any(sc => sc.Staff.UserID == staffUserId));

            var vmdList = availableCourses.Select(c => new CourseViewModel
            {
                courseid = c.CourseId,
                courseCode = c.CourseCode,
                courseName = c.CourseName,
                pathimage = c.CourseImage
            }).ToList();
            return vmdList;
        }
        public List<StaffCourseViewModel> GetCoursesStaffByAdmin(int staffid)
        {
            var staff = context.Staffs.FirstOrDefault(s => s.StaffId == staffid);
            var availableCourses = context.Courses.Where(c => c.staffcourses.Any(sc => sc.StaffId == staffid));

            var vmdList = availableCourses.Select(c => new StaffCourseViewModel
            {
                StaffId = staffid,
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode
            }).ToList();

            return vmdList;
        }
        public int GetCoursesStaffByAdmin(int staffid, int courseId)
        {
            var staffcourse = context.StaffCourses.
                FirstOrDefault(sc => sc.StaffId == staffid && sc.CourseID == courseId);
            if (staffcourse != null)
            {
                context.StaffCourses.Remove(staffcourse);
                return context.SaveChanges();
            }
            return 0;
        }
        public List<Staff> GetStaffsByCourseId(int courseId)
        {
            var staff = (from sc in context.StaffCourses
                         where sc.CourseID == courseId
                         select sc.Staff).ToList();
            return staff;
        }
    }
}