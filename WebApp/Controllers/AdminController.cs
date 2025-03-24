using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class AdminController(IMemberService memberService) : Controller
{
    private readonly IMemberService _memberService = memberService;

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Projects()
    {
        return View();
    }

    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetAllMembers();
        return View(members);
    }

    public IActionResult Clients()
    {
        return View();
    }
}
