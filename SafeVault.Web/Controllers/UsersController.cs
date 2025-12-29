using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SafeVault.Web.Models;
using SafeVault.Web.Security;
using SafeVault.Web.Repositories;

namespace SafeVault.Web.Controllers
{
    public class UsersController : Controller
    {
      private readonly IUserRepository _repository;

      public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Create() => View(new UserInputModel());
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var safeUsername = InputSanitizer.Sanitize(model.Username);
            var safeEmail    = InputSanitizer.Sanitize(model.Email);

            await _repository.CreateUserAsync(safeUsername, safeEmail);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success() => View();


    }
}