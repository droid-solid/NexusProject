using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ResolveIQ.WebUI.Services.Auth;

namespace ResolveIQ.WebUI.Controllers
{
    public class AuthController(IAuthenticationService authService) : Controller
    {
        private readonly IAuthenticationService _authService = authService;
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request) 
        {
            var loginSuccessful = _authService.Login(request.Email, request.Password);
            if (loginSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }          
    }
}
