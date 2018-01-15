using SinglePageCMS.Models;
using System.Web.Mvc;

public class AdminController : BaseController {

    [Route("Admin")]
    public ActionResult Index() {
        return View();
    }

    public int noruyon = 1;
}
