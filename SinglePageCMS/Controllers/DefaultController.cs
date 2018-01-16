using System.Linq;
using System.Web.Mvc;

public class DefaultController : BaseController {

    public ActionResult Index() {

        var menus = (
            db.Menu
            .Include("MenuItem")
            .ToList()
        );

        foreach (var i in menus) {
            ViewData[i.Name] = i.MenuItem.OrderBy(j => j.No);
        }

        ViewBag.sections = (
            db.Section
            .OrderBy(i => i.No)
            .ToList()
        );

        return View();
    }

}
