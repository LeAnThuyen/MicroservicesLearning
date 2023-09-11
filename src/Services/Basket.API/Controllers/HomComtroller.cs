using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class HomComtroller : ControllerBase
    {

        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
