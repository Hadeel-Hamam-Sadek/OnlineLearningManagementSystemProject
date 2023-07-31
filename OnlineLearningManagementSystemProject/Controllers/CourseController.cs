using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.Services;

namespace OnlineLearningManagementSystemProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly LMSContext context;
        ICoursesRepository crservice;
        //IdentityRepository identityservice;

        public CourseController(LMSContext context, ICoursesRepository _crservice)/* IdentityRepository _identityservice*/
        {
            this.context = context;
            crservice = _crservice;
            //identityservice = _identityservice;
        }


        public IActionResult Index(string id)
        {
            //ViewBag.StaffId = id;
            return View(crservice.GetCourse().ToList());
        }
        public IActionResult addCourse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add(Course crs, IFormFile? Picture)
        {
            crservice.CreateCourse(crs.formFile, crs);
            return RedirectToAction("Index");
        }
        public IActionResult update(int id)
        {
            return View(crservice.GetCourseById(id));
        }
        [HttpPost]
        public IActionResult update(int id, Course crs)
        {
            //var user = identityservice.Getuserid();
            //    if (user != null) {
            //    crservice.updateCourse(id, crs);
            //    return RedirectToAction("Index",user);
            //}
            return View();
        }
        public IActionResult delete(int id)
        {
            try
            {
                crservice.deleteCourse(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        //[Route("Course/DoctorStudentName/{id:int}/{staffid:int}")]
        public IActionResult DoctorStudentName(int id)
        {
            ViewBag.CourseId = id;
            return View(crservice.DoctorStudentName(id));
        }

        [HttpGet]
        public async Task<IActionResult> SearchCourses(string name)
        {
            name = name ?? "";
            //var students = await context.Students.Where(s => (s.FirstName + " " + s.LastName).Contains(name)).ToListAsync();
            var courses = await context.Courses.Where(s => (s.CourseName).Contains(name)).ToListAsync();
            ViewBag.search = name;

            return View("Index", courses);

        }
    }
}
