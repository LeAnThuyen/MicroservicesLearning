using Microsoft.AspNetCore.Mvc;

namespace Ordering.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
