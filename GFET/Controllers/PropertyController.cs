using GFET.Models.Property;
using GFET.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GFET.Controllers;

public class PropertyController : Controller
{
    // GET
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public IActionResult CreateProperty(long id)
    {
        var propertyModel = new CreatePropertyViewModel()
        {
            FoodId = id,
            Login = User.Identity.Name,
            Quantity = 0
        };
        return View(propertyModel);
    }
        
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreatePropertyViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _propertyService.Create(model);
            if (response.StatusCode == GFET.Enum.StatusCode.OK)
            {
                return Json(new { description = response.Description });
            }
        }
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
        
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _propertyService.Delete(id);
        if (response.StatusCode == GFET.Enum.StatusCode.OK)
        {
            return RedirectToAction("Index", "Home");
        }
        return View("Error", $"{response.Description}");
    }
}   