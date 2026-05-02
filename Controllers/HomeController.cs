using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mf_dev_back_end_2026_e2_t1_g5.Models;

namespace mf_dev_back_end_2026_e2_t1_g5.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}