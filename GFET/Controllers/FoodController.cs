using GFET.Models.Food;
using GFET.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GFET.Controllers;

public class FoodController : Controller
{
    // GET
    private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet]
        public IActionResult GetFoods()
        {
            var response = _foodService.GetFoods();
            if (response.StatusCode == GFET.Enum.StatusCode.OK)
            {
                return View(response.Data);   
            }
            return View("Error", $"{response.Description}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _foodService.DeleteFood(id);
            if (response.StatusCode == GFET.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetFoods");
            }
            return View("Error", $"{response.Description}");
        }

        public IActionResult Compare() => PartialView();
        
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0) 
                return PartialView();

            var response = await _foodService.GetFood(id);
            if (response.StatusCode == GFET.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(FoodViewModel viewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
                    }
                    await _foodService.Create(viewModel, imageData);
                }
                else
                {
                    await _foodService.Edit(viewModel.Id, viewModel);
                }
            }
            return RedirectToAction("GetFoods");
        }
        
        
        [HttpGet]
        public async Task<ActionResult> GetFood(int id, bool isJson)
        {
            var response = await _foodService.GetFood(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetFood", response.Data);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetFood(string term)
        {
            var response = await _foodService.GetFood(term);
            return Json(response.Data);
        }
        
        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _foodService.GetTypes();
            return Json(types.Data);
        }
}
