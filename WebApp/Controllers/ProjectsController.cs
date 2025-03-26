using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    public IActionResult Index()
    {
        return View();
    }
}
