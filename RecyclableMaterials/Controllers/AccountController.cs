using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecyclableMaterials.Models;
using RecyclableMaterials.ViewModels;

namespace RecyclableMaterials.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _singInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<AppUserModel> userManager,
            SignInManager<AppUserModel> signInManager,
            RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Register

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegiserViewModel model)
        {
            
            if (ModelState.IsValid)
            {

                string profilePictureUrl = null;

                if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                {
                    string folder = Path.Combine("Images", "ProfilePictures");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fullPath = Path.Combine(filePath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(stream);
                    }

                    profilePictureUrl = Path.Combine(folder, fileName); 
                }
                else
                {
            
                    profilePictureUrl = "/Images/ProfilePictures/avatar-1.png";
                }


                var user = new AppUserModel
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePictureUrl = profilePictureUrl,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber

                };

                var respon = await _userManager.CreateAsync(user, model.Password);

                if (respon.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Account");
                }
                foreach (var err in respon.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }


        #endregion

        #region login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _singInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (respone.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Email or pass");
                return View(model);
            }
            return View(model);
        }

        #endregion

        #region Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        #endregion

        #region Manage
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var currentUserName = await _userManager.GetUserAsync(User);
            
            if (currentUserName!= null )
            {
                var viewModel = new ManageUserViewModel
                {
                    UserName = currentUserName.UserName,

                    //FirstName = currentUserName.FirstName,
                    //LastName = currentUserName.LastName,
                    PhoneNumber = currentUserName.PhoneNumber


                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if(currentUser!= null )
                {
                    currentUser.PhoneNumber = model.PhoneNumber;
                    //currentUser.FirstName = model.FirstName;
                    //currentUser.LastName = model.LastName;
                    await _userManager.UpdateAsync(currentUser);
                   
                }
            }
            return View("Index", "Home");
        }

        

        #endregion



        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }
    }
}
