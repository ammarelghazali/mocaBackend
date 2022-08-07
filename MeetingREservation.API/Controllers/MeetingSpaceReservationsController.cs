using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingREservation.API.Controllers
{
    //[ApiVersion("1.0")]
    //[ApiExplorerSettings(GroupName = "Admin")]
    [Authorize]
    public class MeetingSpaceReservationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
