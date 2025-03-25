using Business.Interfaces;
using Business.Models;
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

    [Route("members")]
    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetAllMembers();
        return View(members);
    }

    [Route("clients")]
    public IActionResult Clients()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddClient(AddClientForm form)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Clients");
        }

        return View();
    }
}
