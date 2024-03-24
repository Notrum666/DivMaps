using System.Diagnostics;

using DivMaps.Models;

using Microsoft.AspNetCore.Mvc;

namespace DivMaps.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly DataContainer dataContainer;

        public HomeController(DataContainer dataContainer)
        {
            this.dataContainer = dataContainer;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View(model: dataContainer.LastUpdate.ToString("HH:mm:ss") + " utc");
        }
    }
}
