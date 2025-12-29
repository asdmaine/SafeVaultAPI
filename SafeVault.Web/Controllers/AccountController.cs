using Microsoft.AspNetCore.Mvc;
using SafeVault.Web.Models;
using SafeVault.Web.Repositories;
using SafeVault.Web.Security;

namespace SafeVault.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var hash = PasswordHasher.HashPassword(model.Password);

            await _repository.CreateUserWithAuthAsync(
                model.Username,
                model.Email,
                hash,
                model.Role
            );

            return RedirectToAction("Login");
        }
    }
}
