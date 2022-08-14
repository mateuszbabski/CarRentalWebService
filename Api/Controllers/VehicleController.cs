using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
