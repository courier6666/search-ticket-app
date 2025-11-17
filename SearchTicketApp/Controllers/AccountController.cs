using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Exceptions;
using SearchTicketApp.Extensions;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Command;
using SearchTicketApp.Models.Constants;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SearchTicketApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IUserContextAccessor userContextAccessor;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IUserContextAccessor userContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userContextAccessor = userContextAccessor;
        }

        private async Task<bool> UserWithEmailExistsAsync(string email)
        {
            return (await this.userManager.FindByEmailAsync(email)) != null;
        }

        private void ValidateIdentityResult(IdentityResult createUserResult)
        {
            if (!createUserResult.Succeeded)
            {
                foreach (var error in createUserResult.Errors)
                    this.ModelState.AddModelError(error.Code, error.Description);

                ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut(string returnUrl)
        {
            if(this.HttpContext.User.IsAuthenticated())
                await this.signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View("Login", new LoginCommand());
        }

        private async Task ValidateUserLoginCredentialsAsync(LoginCommand loginCommand)
        {
            var foundUser = await this.userManager.FindByEmailAsync(loginCommand.Email);

            if (foundUser == null)
            {
                this.ModelState.AddModelError(nameof(loginCommand.Email), $"Email '{loginCommand.Email}' does not exist.");
                ModelNotValidException.Throw(this.ModelState);
            }

            if (!await userManager.CheckPasswordAsync(foundUser, loginCommand.Password))
            {
                this.ModelState.AddModelError(nameof(loginCommand.Password), $"Wrong password.");
                ModelNotValidException.Throw(this.ModelState);
            }
        }

        private async Task<IActionResult> LogInUserAsync(LoginCommand loginCommand)
        {
            ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
            await ValidateUserLoginCredentialsAsync(loginCommand);

            var signInResult = await this.signInManager.PasswordSignInAsync(loginCommand.Email, loginCommand.Password, true, false);
            ValidateSignInResult(signInResult);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginCommand loginCommand)
        {
            try
            {
                return await LogInUserAsync(loginCommand);
            }
            catch (ModelNotValidException ex)
            {
                return View("Login", loginCommand);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register", new RegisterCommand());
        }

        #region RegisterPostRequest
        private async Task ValidateRegisterCommandAsync(RegisterCommand registerCommand)
        {
            if (await UserWithEmailExistsAsync(registerCommand.Email))
            {
                this.ModelState.AddModelError(nameof(registerCommand.Email), $"Email '{registerCommand.Email}' is taken.");
            }

            if (registerCommand.Password != registerCommand.ConfirmPassword)
            {
                this.ModelState.AddModelError(nameof(registerCommand.ConfirmPassword), "Confirm password is not equal to password.");
            }

            ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
        }

        private void ValidateSignInResult(SignInResult signInResult) 
        {
            if (!signInResult.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, "Failed to sign in.");
                ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
            }
        }

        private async Task<IActionResult> RegisterUserAsync(RegisterCommand registerCommand)
        {
            ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
            await ValidateRegisterCommandAsync(registerCommand);

            var registeredUser = new User()
            {
                Email = registerCommand.Email,
                UserName = registerCommand.Email,
            };

            var createUserResult = await this.userManager.CreateAsync(registeredUser, registerCommand.Password);
            var addToRoleResult = await this.userManager.AddToRoleAsync(registeredUser, UserRoles.User);
            ValidateIdentityResult(createUserResult);
            ValidateIdentityResult(addToRoleResult);

            var signInResult = await this.signInManager.PasswordSignInAsync(registeredUser.UserName, registerCommand.Password, true, false);
            ValidateSignInResult(signInResult);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterCommand registerCommand)
        {
            try
            {
                return await RegisterUserAsync(registerCommand);
            }
            catch (ModelNotValidException ex)
            {
                return View("Register", registerCommand);
            }
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
