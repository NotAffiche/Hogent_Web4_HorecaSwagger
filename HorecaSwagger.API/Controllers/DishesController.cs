using Microsoft.AspNetCore.Mvc;

namespace HorecaSwagger.API.Controllers;

public class DishesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
