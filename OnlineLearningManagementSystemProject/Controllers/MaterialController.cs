using Microsoft.AspNetCore.Mvc;
using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.Services;

namespace OnlineLearningManagementSystemProject.Controllers
{
    public class MaterialController : Controller
    {

        IUploadStaffFileRepository uploadStaffFileServise;
        IdentityRepository identityservice;

        public MaterialController(IUploadStaffFileRepository _uploadStaffFileServise, IdentityRepository _identityservice)
        {
            uploadStaffFileServise = _uploadStaffFileServise;
            identityservice = _identityservice;
        }

        public IActionResult Index()
        {
            var model = uploadStaffFileServise.GetAll();
            return View(model);
        }
        [HttpGet]
        [Route("Material/CreateD/{courseId:int}")]
        public IActionResult CreateD(int courseId)
        {
            var UserID = identityservice.GetUserId("UserId");
            ViewBag.CourseId = courseId;
            ViewBag.UserID = UserID;
            ViewBag.MaterialTypes = Enum.GetNames(typeof(MaterialType)).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateDocument(Material model, int courseId)
        {
            var UserID = identityservice.GetUserId("UserId");
            uploadStaffFileServise.UploadDocument(model.formFile, model, UserID);
            return RedirectToAction("classwork", new { courseId = courseId });
        }



        [HttpGet]
        [Route("Material/CreateV/{courseId:int}/{staffId:int}")]
        public IActionResult CreateV(int courseId, int staffId)
        {
            ViewBag.CourseId = courseId;
            ViewBag.StaffId = staffId;
            ViewBag.MaterialTypes = Enum.GetNames(typeof(MaterialType)).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateVideo(Material model)
        {

            uploadStaffFileServise.UploadVideo(model.formFile, model);
            return RedirectToAction("Index");
        }




        [HttpGet]
        [Route("Material/CreateL/{courseId:int}/{staffId:int}")]
        public IActionResult CreateL(int courseId, int staffId)
        {
            ViewBag.CourseId = courseId;
            ViewBag.StaffId = staffId;
            ViewBag.MaterialTypes = Enum.GetNames(typeof(MaterialType)).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateLink(Material model)
        {

            uploadStaffFileServise.UploadLink(model.MaterialPath, model);
            return RedirectToAction("Index");
        }
        //[Route("Material/GetLecturesByMaterial/{id:int}/{stdid:int}")]
        public IActionResult GetLecturesByMaterial(int id)
        {
            //ViewBag.StudentID = stdid;
            var user = identityservice.GetUserId("UserId");
            var Lectures = uploadStaffFileServise.GetLecturesByMaterial(id);
            return View(Lectures);
        }
        [HttpGet]
        //[Route("Material/classwork/{courseId:int}/{staffId:int}")]
        public IActionResult classwork(int courseId)
        {
            ViewBag.CourseID = courseId;
            //ViewBag.StaffId = staffId;
            var user = identityservice.GetUserId("UserId");

            var Lectures = uploadStaffFileServise.GetLecturesByMaterialDoctor(courseId, user);
            if (Lectures == null || Lectures.Count == 0)
            {
                var course = uploadStaffFileServise.GetLecturesByMaterialCourse(courseId);
                var Lectures1 = new Material();

                Lectures1.Course = course;

                Lectures.Add(Lectures1);
            }
            return View(Lectures);
        }
        //[HttpPost]
        public IActionResult Delete(int materialId, int courseId)
        {
            uploadStaffFileServise.DeleteMaterial(materialId);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("classwork", new { courseId = courseId });
        }
    }
}