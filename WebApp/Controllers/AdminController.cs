using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly IMemberService _memberService;
    private readonly IClientService _clientService;

    public AdminController(IMemberService memberService, IClientService clientService)
    {
        _memberService = memberService;
        _clientService = clientService;
    }

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
    public async Task<IActionResult> Clients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return View(clients);
    }
}
