using iDEA.Models;
using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{

    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            var kurs = new Course();
            kurs.ID = 1;
            kurs.Name = "Matematik";
            return View(kurs);
        }

        public IActionResult List()
        {
            var kursListesi = new List<Course>(){
                new Course(){ ID=1, Name="Matematik",Image="1.jpg"},
                new Course(){ ID=2, Name="Fen"},
                new Course(){ ID=3, Name="Kimya"}
            };

            return View(kursListesi);
        }

    }

}