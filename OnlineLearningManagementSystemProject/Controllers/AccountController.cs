using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult AdminRegistration() { return View(); }

        [HttpPost]

        public async Task<IActionResult> AdminRegistration(AdminRegistrationViewModel newAccount)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = newAccount.Email;
            // user.Email = newAccount.Email;

            if (ModelState.IsValid == true)
            {
                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);

                if (result.Succeeded)
                {
                    // create cookies
                    //=================================================
                    await userManager.AddToRoleAsync(user, "Admin");
                    // create cookies
                    await signInManager.SignInAsync(user, isPersistent: false);

                    //  ====================================================
                    await signInManager.SignInAsync(user, isPersistent: false);




                    return RedirectToAction("addStaff", "Staff");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

            }
            return View(newAccount);
        }

        //=========================  StudentRegistration  ==================================


        //public IActionResult StudentRegistration() { return View(); }

        //[HttpPost]

        //public async Task<IActionResult> StudentRegistration(StudentRegistrationViewModel newAccount)
        //{
        //    IdentityUser user = new IdentityUser();
        //    user.UserName = newAccount.UserName;
        //   // user.Email = newAccount.Email;

        //    if (ModelState.IsValid == true)
        //    {
        //        IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);

        //        if (result.Succeeded)
        //        {
        //            // create cookies
        //            //=================================================
        //            await userManager.AddToRoleAsync(user, "Student");
        //            // create cookies
        //            await signInManager.SignInAsync(user, isPersistent: false);

        //            await signInManager.SignInAsync(user, isPersistent: false);


        //            return RedirectToAction("Index", "Student");

        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }

        //        }

        //    }
        //    return View(newAccount);
        //}


        //====================================================================


        //===============================   StaffRegistration   ============================


        //public IActionResult StaffRegistration() { return View(); }

        //[HttpPost]

        //public async Task<IActionResult> StaffRegistration(StaffRegistrationViewModel newAccount)
        //{
        //    IdentityUser user = new IdentityUser();
        //    user.UserName = newAccount.UserName;
        //   // user.Email = newAccount.Email;

        //    if (ModelState.IsValid == true)
        //    {
        //        IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);

        //        if (result.Succeeded)
        //        {
        //            // create cookies
        //            //=================================================
        //            await userManager.AddToRoleAsync(user, "Staff");
        //            // create cookies
        //            await signInManager.SignInAsync(user, isPersistent: false);

        //            await signInManager.SignInAsync(user, isPersistent: false);


        //            return RedirectToAction("Index", "Staff");

        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }

        //        }

        //    }
        //    return View(newAccount);
        //}


        //====================================================================










        //==================================== Register Admin ===========================================

        public IActionResult AddAdmin() { return View("AddAdmin"); }




        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminRegistrationViewModel newAccount)
        {
            // map ViewModel to model
            IdentityUser user = new IdentityUser();
            user.UserName = newAccount.Email;
            // user.Email = newAccount.Email;
            if (ModelState.IsValid == true)
            {
                //How to save user and cookies
                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);

                if (result.Succeeded) // لو كل حاجه صح كريت كوكيز
                {

                    //Add Admin Role
                    await userManager.AddToRoleAsync(user, "Admin");
                    // create cookies
                    await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "student");

                    return RedirectToAction("addStaff", "Staff", new { userId = user.Id });

                }
                else
                {
                    // علشان اتعامل مع ايرور ايرور لوحده
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

            }
            return View("AddAdmin", newAccount);
        }









        public IActionResult AddStudent() { return View("AddStudent"); } // دا بيفتح فورمه فاضيه بس 




        [HttpPost]

        public async Task<IActionResult> AddStudent(StudentRegistrationViewModel newAccount)
        {
            // map ViewModel to model
            IdentityUser user = new IdentityUser();
            user.UserName = newAccount.Email;

            //   user.Email = newAccount.Email;
            if (ModelState.IsValid == true)
            {
                //How to save user and cookies
                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);

                if (result.Succeeded) // لو كل حاجه صح كريت كوكيز
                {

                    //Add Admin Role
                    await userManager.AddToRoleAsync(user, "Student");

                    // create cookies
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("AddStudent", "Student", new { userId = user.Id });

                }
                else
                {
                    // علشان اتعامل مع ايرور ايرور لوحده
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

            }
            return View("AddStudent", newAccount);
        }






        //==================================== Register Staff ===========================================

        public IActionResult AddStaff() { return View("AddStaff"); } // دا بيفتح فورمه فاضيه بس 




        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffRegistrationViewModel newAccount)
        {
            // map ViewModel to model
            IdentityUser user = new IdentityUser();
            user.UserName = newAccount.Email;
            //  user.Email = newAccount.Email;
            if (ModelState.IsValid == true)
            {
                //How to save user and cookies
                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);

                if (result.Succeeded) // لو كل حاجه صح كريت كوكيز
                {

                    //Add Admin Role
                    await userManager.AddToRoleAsync(user, "Staff");
                    // create cookies
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("addStaff", "Staff", new { userId = user.Id });

                }
                else
                {
                    // علشان اتعامل مع ايرور ايرور لوحده
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

            }
            return View("AddStaff", newAccount);
        }








        //====================== Login =============================
        public IActionResult Login(string ReturnUrl = "~/student/Add")
        {
            ViewData["RedirectUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginUser)
        {
            if (ModelState.IsValid == true)
            {
                IdentityUser user = await userManager.FindByNameAsync(LoginUser.UserName);

                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult resualt = await signInManager.PasswordSignInAsync(user, LoginUser.Password, LoginUser.IsPersistent, false);

                    if (resualt.Succeeded)
                    {
                        var role = await userManager.GetRolesAsync(user);
                        //return RedirectToAction("Index", "student");

                        //foreach(var Ro in role.Result)
                        //{
                        //    if (Ro=="Student")
                        //    {
                        //        return RedirectToAction("Index", "Student");
                        //    }
                        //    if (Ro == "Staff")
                        //    {
                        //        return RedirectToAction("Index", "Staff");
                        //    }
                        //}


                        //            if (role.Contains("Student"))
                        //            {
                        //                return RedirectToAction("getStudentById", "Student");
                        //            }

                        //            else if (role.Contains("Staff"))
                        //            {
                        //                return RedirectToAction("GetstaffById", "Staff");
                        //            }
                        //            else if (role.Contains("Admin"))
                        //            {
                        //                return RedirectToAction("GetStudentsASEnrollYear", "Student");
                        //            }
                        //        }

                        //        else { ModelState.AddModelError("", "InCorrect UserName or Password"); }
                        //    }
                        //    else
                        //    {
                        //        ModelState.AddModelError("", "Invalid UserName or Password");
                        //    }
                        //}
                        //return View(LoginUser);
                        HttpContext.Session.SetString("UserId", user.Id);
                        var id = HttpContext.Session.GetString("UserId");
                        if (role.Contains("Student"))
                        {
                            HttpContext.Session.SetString("usertype", "Student");
                            return RedirectToAction("GetStudentById", "Student");
                        }

                        else if (role.Contains("Staff"))
                        {
                            HttpContext.Session.SetString("usertype", "Staff");
                            return RedirectToAction("GetstaffById", "Staff");
                        }
                        else if (role.Contains("Admin"))
                        {
                            HttpContext.Session.SetString("usertype", "Admin");
                            return RedirectToAction("GetStudentsASEnrollYear", "Student");
                        }

                    }
                }
            }
            return View(LoginUser);
        }














        //====================== Logout =============================

        //بتروح تشوف لو الشخص دا معملخ كوكيز هتشيلها
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
