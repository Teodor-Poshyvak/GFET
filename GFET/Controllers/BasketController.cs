using GFET.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GFET.Controllers;

public class BasketController : Controller
{
    // GET
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IActionResult> Detail()
    {
        var response = await _basketService.GetItems(User.Identity.Name);
        if (response.StatusCode == GFET.Enum.StatusCode.OK)
        {
            return View(response.Data.ToList());
        }
        return RedirectToAction("Index", "Home");
    }
        
    [HttpGet]
    public async Task<IActionResult> GetItem(long id)
    {
        var response = await _basketService.GetItem(User.Identity.Name, id);
        if (response.StatusCode == GFET.Enum.StatusCode.OK)
        {
            return PartialView(response.Data);
        }
        return RedirectToAction("Index", "Home");
    }
}   
