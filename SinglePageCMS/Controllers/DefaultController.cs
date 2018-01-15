using System.Linq;
using System.Web.Mvc;

public class DefaultController : BaseController {

    public ActionResult Index() {

        ViewBag.sections = (
            db.Section
            .OrderBy(i => i.No)
            .ToList()
        );

        return View();
    }

}
