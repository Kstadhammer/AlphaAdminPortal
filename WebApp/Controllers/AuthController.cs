using System.Threading.Tasks;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult Login()
    {
        ViewBag.ErrorMessage = null;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(MemberLoginForm form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Invalid email address";
            return View(form);
        }

        var result = await _authService.LoginAsync(form);
        if (result)
        {
            return Redirect(returnUrl);
        }

        ViewBag.ErrorMessage = "Unable to login right now. Please try again later.";
        return View(form);
    }
}
