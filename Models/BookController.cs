using Microsoft.AspNetCore.Mvc;

namespace WebAPIDiscussion.Models
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
