using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Exceptions;
using SearchTicketApp.Models.Command;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SearchTicketApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<User> roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<User> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        private async Task<bool> UserWithEmailExistsAsync(string email)
        {
            return (await this.userManager.FindByEmailAsync(email)) != null;
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
                this.ModelState.AddModelError("Email exists.", $"Email '{registerCommand.Email}' is taken!");
            }

            if (registerCommand.Password != registerCommand.ConfirmPassword)
            {
                this.ModelState.AddModelError("Confirm password not valid", "Confirm password is not equal to password.");
            }

            ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
        }

        private void ValidateCreateUserResult(IdentityResult createUserResult)
        {
            if (!createUserResult.Succeeded)
            {
                foreach (var error in createUserResult.Errors)
                    this.ModelState.AddModelError(error.Code, error.Description);

                ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
            }
        }

        private void ValidateSignInResult(SignInResult signInResult)
        {
            if (!signInResult.Succeeded)
            {
                this.ModelState.AddModelError("Sign in failure", "Failed to sign in. Try logging in from 'Log In' page.");
                ModelNotValidException.ThrowIfModelStateNotValid(this.ModelState);
            }
        }

        private async Task<IActionResult> RegisterUserAsync(RegisterCommand registerCommand)
        {
            await ValidateRegisterCommandAsync(registerCommand);

            var registeredUser = new User()
            {
                Email = registerCommand.Email,
                UserName = registerCommand.Email,
            };

            var createUserResult = await this.userManager.CreateAsync(registeredUser, registerCommand.Password);
            ValidateCreateUserResult(createUserResult);

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
