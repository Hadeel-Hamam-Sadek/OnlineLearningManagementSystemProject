using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.Services;
using OnlineLearningManagementSystemProject.ViewModel;
namespace OnlineLearningManagementSystemProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly LMSContext context;
        IStudentRepository stdservice;
        IdentityRepository identityservice;

        public StudentController(LMSContext context, IStudentRepository _stdservice, IdentityRepository _identityservice)
        {
            this.context = context;
            stdservice = _stdservice;
            identityservice = _identityservice;
        }

        public IActionResult Index()
        {
            return View(stdservice.GetStudents().ToList());
        }
        public IActionResult AddStudent(string UserId)
        {
            ViewBag.UserId = UserId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Student std)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(std);
            //}
            //var user = await userManager.GetUserAsync(User);
            string UserId = ViewBag.UserId;
            stdservice.CreateStudent(std.formFile, std);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetStudentsASEnrollYear");

        }
        public IActionResult delete(int Id)
        {
            try
            {
                stdservice.deleteStudent(Id);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

            }
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetStudentsASEnrollYear");
        }
        public IActionResult update(int id)
        {

            return View(stdservice.GetStudentById(id));
        }
        [HttpPost]
        public IActionResult update(int id, Student std)
        {
            //var user = identityservice.GetUserId("UserId");
            stdservice.updateStudent(id, std);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetStudentsASEnrollYear");
        }
        public IActionResult getStudentById(int id)
        {
            if (id != 0)
            {
                return View(stdservice.GetStudentById(id));
            }
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(stdservice.GetStudentById(user));
        }
        public IActionResult GetStudentsASEnrollYear()
        {
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(stdservice.GetStudentsASEnrollYear().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> SearchStudents(string name)
        {
            name = name ?? "";
            var students = await context.Students.Where(s => (s.FirstName + " " + s.LastName).Contains(name)).ToListAsync();
            ViewBag.search = name;
            var studentsView = new List<EnrollYearStudentNameViewModel>();
            foreach (var student in students)
            {
                studentsView.Add(new EnrollYearStudentNameViewModel
                {
                    studentname = student.FirstName + " " + student.LastName,
                    enrollyear = student.EnrollYear,
                    studentid = student.StudentID
                });
            }

            return View("GetStudentsASEnrollYear", studentsView);

        }
    }
}