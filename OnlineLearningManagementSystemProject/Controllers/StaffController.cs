using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.Services;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Controllers
{
    public class StaffController : Controller
    {
        private readonly LMSContext context;
        IStaffRepository stfservice;
        IdentityRepository identityservice;
        public StaffController(LMSContext context, IStaffRepository _stfservice, IdentityRepository _identityservice)
        {
            this.context = context;
            stfservice = _stfservice;
            identityservice = _identityservice;
        }

        public IActionResult Index()
        {
            return View(stfservice.GetStaff().ToList());
        }
        public IActionResult GetstaffById(int id)
        {
            if (id != 0)
            {
                return View(stfservice.GetStaffById(id));
            }
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(stfservice.GetStaffById(user));
        }
        public IActionResult addStaff(string userId)
        {
            HttpContext.Session.SetString("NewUserId", userId);
            return View();
        }
        [HttpPost]
        public IActionResult add(Staff sff, IFormFile Picture)
        {
            string UserId = HttpContext.Session.GetString("NewUserId");
            stfservice.CreateStaff(sff.formFile, sff, UserId);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetStaffASNationaliId");
        }
        public IActionResult update(int id)
        {
            //var user = identityservice.GetUserId("UserId");
            return View(stfservice.GetStaffById(id));
        }
        [HttpPost]
        public IActionResult update(int id, Staff stf)
        {
            stfservice.updateStaff(id, stf);
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("GetStaffASNationaliId");
        }
        public IActionResult delete(int id)
        {
            try
            {
                stfservice.deleteStaff(id);

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
            return RedirectToAction("GetStaffASNationaliId");
        }
        public IActionResult GetStaffASNationaliId()
        {
            var user = identityservice.GetUserId("UserId");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(stfservice.GetNationalIDStaffNames().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> SearchStaffs(string name)
        {
            name = name ?? "";
            var staffs = await context.Staffs.Where(s => (s.FirstName + " " + s.LastName).Contains(name)).ToListAsync();
            ViewBag.search = name;
            var staffsView = new List<NationalIDStaffNameViewModel>();
            foreach (var staff in staffs)
            {
                staffsView.Add(new NationalIDStaffNameViewModel
                {
                    staffname = staff.FirstName + " " + staff.LastName,

                    staffid = staff.StaffId,

                    nationalid = staff.NationalID
                });
            }
            return View("GetStaffASNationaliId", staffsView);

        }
    }
}
