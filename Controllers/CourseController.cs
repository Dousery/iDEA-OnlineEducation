using iDEA.Models;
using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{

    public class CourseController : Controller
    {
        public async Task<IActionResult> Index(UserDataModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }

}