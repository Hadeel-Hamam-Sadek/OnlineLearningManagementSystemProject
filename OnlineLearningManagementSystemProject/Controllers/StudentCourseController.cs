using Microsoft.AspNetCore.Mvc;
using OnlineLearningManagementSystemProject.Services;

namespace OnlineLearningManagementSystemProject.Controllers
{
    public class StudentCourseController : Controller
    {
        IStudentCourseRepository studentCourseRepository;
        IdentityRepository identityservice;

        public StudentCourseController(IStudentCourseRepository _studentCourseRepository, IdentityRepository _identityservice)
        {
            studentCourseRepository = _studentCourseRepository;
            identityservice = _identityservice;
        }

        public IActionResult Index()
        {
            return View(studentCourseRepository.GetAllStusent().ToList());

        }



        public IActionResult GetCoursesStudent()
        {
            var studentUserId = identityservice.GetUserId("UserId");

            return View(studentCourseRepository.GetCoursesStudent(studentUserId));
        }
        public IActionResult addcoursetostudent(int id)
        {
            ViewBag.studentId = id;
            return View(studentCourseRepository.AddCoursesToStudent(id));
        }
        [HttpPost]
        public IActionResult addcoursetostudent(int studentId, int courseId)
        {
            //var user = identityservice.GetUserId("UserId");
            studentCourseRepository.AddCoursesToStudent(studentId, courseId);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetCoursesStudentByAdmin", new { id = studentId });
        }
        public IActionResult GetCoursesStudentByAdmin(int id)
        {
            ViewBag.studentId = id;
            return View(studentCourseRepository.GetCoursesStudentByAdmin(id));
        }
        [HttpPost]
        public IActionResult GetCoursesStudentByAdmin(int studentId, int courseId)
        {

            studentCourseRepository.GetCoursesStudentByAdmin(studentId, courseId);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetCoursesStudentByAdmin");
        }
        public IActionResult GetStudentsByCourseId(int id)
        {
            return View(studentCourseRepository.GetStudentsByCourseId(id));
        }
    }
}

