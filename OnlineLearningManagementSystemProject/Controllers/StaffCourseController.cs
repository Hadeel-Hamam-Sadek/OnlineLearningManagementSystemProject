using Microsoft.AspNetCore.Mvc;
using OnlineLearningManagementSystemProject.Services;

namespace OnlineLearningManagementSystemProject.Controllers
{
    public class StaffCourseController : Controller
    {
        IStaffCourseRepository servicestaff;
        IdentityRepository identityservice;
        public StaffCourseController(IStaffCourseRepository _servicestaff, IdentityRepository _identityservice)
        {
            servicestaff = _servicestaff;
            identityservice = _identityservice;
        }
        public IActionResult GetCoursesStaff()
        {
            var staffUserId = identityservice.GetUserId("UserId");

            return View(servicestaff.GetCoursesStaff(staffUserId));
        }
        public IActionResult addcoursetostaff(int id)
        {
            ViewBag.StaffId = id;
            return View(servicestaff.AddCoursesToStaff(id));
        }
        [HttpPost]
        public IActionResult addcoursetostaff(int staffid, int courseId)
        {

            servicestaff.AddCoursesToStaff(staffid, courseId);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetCoursesStaffByAdmin", new { id = staffid });
        }
        public IActionResult GetCoursesStaffByAdmin(int id)
        {
            ViewBag.StaffId = id;
            return View(servicestaff.GetCoursesStaffByAdmin(id));
        }
        [HttpPost]
        public IActionResult GetCoursesStaffByAdmin(int staffid, int courseId)
        {
            servicestaff.GetCoursesStaffByAdmin(staffid, courseId);
            var user = identityservice.GetUserId("UserId");

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetCoursesStaffByAdmin");
        }
        public IActionResult GetStaffsByCourseId(int id)
        {
            ViewBag.StaffId = id;
            return View(servicestaff.GetStaffsByCourseId(id));
        }
    }
}