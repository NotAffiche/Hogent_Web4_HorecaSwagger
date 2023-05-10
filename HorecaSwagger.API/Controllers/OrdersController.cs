using Microsoft.AspNetCore.Mvc;

namespace HorecaSwagger.API.Controllers;

public class OrdersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
