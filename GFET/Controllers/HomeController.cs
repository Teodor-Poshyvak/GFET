using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GFET.Models;

namespace GFET.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Privacy()
    {
        throw new NotImplementedException();
    }
}