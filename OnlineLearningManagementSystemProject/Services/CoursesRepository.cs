using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public class CoursesRepository : ICoursesRepository
    {
        LMSContext context;

        public CoursesRepository(LMSContext _context)
        {
            context = _context;
        }

        public int CreateCourse(IFormFile formFile, Course crs)
        {
            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(crs.formFile.FileName);
                string relativePath = Path.Combine("courseimage", fileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                crs.CourseImage = relativePath;
                context.Courses.Add(crs);
                int raw = context.SaveChanges();
                return raw;
            }
            else if (formFile == null || formFile.Length < 0)
            {
                string relativePath = "images/cat-8.jpg";
                crs.CourseImage = relativePath;
                context.Courses.Add(crs);
                int raw = context.SaveChanges();
                return raw;
            }
            return -1;
        }
        public int deleteCourse(int id)
        {
            Course crs = context.Courses.FirstOrDefault(c => c.CourseId == id);
            context.Courses.Remove(crs);
            int raw = context.SaveChanges();
            return raw;
        }

        public DoctorStudentViewModel DoctorStudentName(int courseid)
        {
            var doctorStudentViewModel = new DoctorStudentViewModel();
            doctorStudentViewModel.CourseName = context.Courses
     .Where(c => c.CourseId == courseid)
     .Select(c => c.CourseName)
     .FirstOrDefault();

            // استعلام لاسترداد أسماء وصور الأطباء المرتبطين بالمادة
            var doctorQuery = from staffCourse in context.StaffCourses
                              join staff in context.Staffs on staffCourse.StaffId equals staff.StaffId
                              where staffCourse.CourseID == courseid && staffCourse.Staff != null
                              select new
                              {
                                  DoctorName = staff.FirstName + " " + staff.LastName,
                                  DoctorImage = staff.StaffImage
                              };

            doctorStudentViewModel.DoctorNames = doctorQuery.Select(d => d.DoctorName).ToList();
            doctorStudentViewModel.DoctorImages = doctorQuery.Select(d => d.DoctorImage).ToList();

            // استعلام لاسترداد أسماء وصور الطلاب المرتبطين بالمادة
            var studentQuery = from studentCourse in context.StudentCourses
                               join student in context.Students on studentCourse.StudentID equals student.StudentID
                               where studentCourse.CourseID == courseid && studentCourse.Student != null
                               select new
                               {
                                   StudentName = student.FirstName + " " + student.LastName,
                                   StudentImage = student.StudentPicture
                               };

            doctorStudentViewModel.StudentNames = studentQuery.Select(s => s.StudentName).ToList();
            doctorStudentViewModel.StudentImages = studentQuery.Select(s => s.StudentImage).ToList();

            return doctorStudentViewModel;

        }

        public List<Course> GetCourse()
        {
            return context.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return context.Courses.FirstOrDefault(c => c.CourseId == id);
        }

        public int updateCourse(int id, Course crs)
        {
            Course oldcrs = context.Courses.FirstOrDefault(c => c.CourseId == id);
            oldcrs.Lecture = crs.Lecture;
            oldcrs.CourseCode = crs.CourseCode;
            oldcrs.Lab = crs.Lab;
            oldcrs.Credit = crs.Credit;
            oldcrs.Training = crs.Training;
            oldcrs.CourseName = crs.CourseName;
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
