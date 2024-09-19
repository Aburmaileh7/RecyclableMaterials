using Microsoft.AspNetCore.Authorization;
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
        //[ActionFilter]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        //[ActionFilter]
        public async Task<IActionResult> Register(RegiserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string profilePictureUrl = null;

                if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(model.ProfilePicture.FileName);

                    if (!allowedExtensions.Contains(extension.ToLower()))
                    {
                        ModelState.AddModelError("", "Only image files (.jpg, .jpeg, .png, .gif) are allowed.");
                        return View(model);
                    }

                    string folder = Path.Combine("Images", "ProfilePictures");
                    string fileName = Guid.NewGuid().ToString() + extension;
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
                    profilePictureUrl = "Images/ProfilePictures/avatar-1.png";
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

                var response = await _userManager.CreateAsync(user, model.Password);

                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Account");
                }

                foreach (var err in response.Errors)
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
        //[ActionFilter]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ActionFilter]
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
            return RedirectToAction("Index", "Home");
        }


        #endregion

        #region Manage
        [HttpGet]
        //[ActionFilter]
        public async Task<IActionResult> Manage()
        {
            var currentUserName = await _userManager.GetUserAsync(User);

            if (currentUserName != null)
            {
                string profilePictureUrl = currentUserName.ProfilePictureUrl;

                if (string.IsNullOrEmpty(profilePictureUrl))
                {
                    profilePictureUrl = "Images/ProfilePictures/avatar-1.png";
                }


                var viewModel = new ManageUserViewModel
                {
                    UserName = currentUserName.UserName,

                    FirstName = currentUserName.FirstName,
                    LastName = currentUserName.LastName,
                    DateOfBirth = currentUserName.DateOfBirth,
                    ProfilePictureUrl = profilePictureUrl,
                    PhoneNumber = currentUserName.PhoneNumber


                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [Authorize]
        //[ActionFilter]
        public async Task<IActionResult> Profile()
        {
            var currentUserName = await _userManager.GetUserAsync(User);

            if (currentUserName != null)
            {
                string profilePictureUrl = currentUserName.ProfilePictureUrl;

                if (string.IsNullOrEmpty(profilePictureUrl))
                {
                    profilePictureUrl = "Images/ProfilePictures/avatar-1.png";
                }


                var viewModel = new ManageUserViewModel
                {
                    UserName = currentUserName.UserName,

                    FirstName = currentUserName.FirstName,
                    LastName = currentUserName.LastName,
                    DateOfBirth = currentUserName.DateOfBirth,
                    ProfilePictureUrl = currentUserName.ProfilePictureUrl,
                    PhoneNumber = currentUserName.PhoneNumber,
                    


                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        //[ActionFilter]
        public async Task<IActionResult> Manage(ManageUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {

                    currentUser.PhoneNumber = model.PhoneNumber;
                    currentUser.FirstName = model.FirstName;
                    currentUser.LastName = model.LastName;
                    currentUser.DateOfBirth = model.DateOfBirth;
                  

                   
                    //if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                    //{
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var extension = Path.GetExtension(model.ProfilePicture.FileName);

                        if (!allowedExtensions.Contains(extension.ToLower()))
                        {
                            ModelState.AddModelError("", "Only image files (.jpg, .jpeg, .png, .gif) are allowed.");
                            return View(model);
                        }

                        string folder = Path.Combine("Images", "ProfilePictures");
                        string fileName = Guid.NewGuid().ToString() + extension;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        string fullPath = Path.Combine(filePath, fileName);


                        if (!string.IsNullOrEmpty(currentUser.ProfilePictureUrl) && !currentUser.ProfilePictureUrl.Contains("avatar-1.png"))
                        {
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, currentUser.ProfilePictureUrl);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }


                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await model.ProfilePicture.CopyToAsync(stream);
                        }

                        currentUser.ProfilePictureUrl = Path.Combine(folder, fileName);
                    }


                    var result = await _userManager.UpdateAsync(currentUser);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Profile updated successfully.";
                        return RedirectToAction("Manage");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            

            return View(model);
        }

            #endregion

        [HttpGet]
        //[ActionFilter]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }
    }
} 

