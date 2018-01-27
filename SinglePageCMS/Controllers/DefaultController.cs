using System.Linq;
using System.Web.Mvc;

public class DefaultController : BaseController {

    [Route("~/")]
    public ActionResult Index() {

        var menus = (
            db.Menu
            .Include("MenuItem")
            .ToList()
        );

        foreach (var i in menus) {
            ViewData[i.Name] = i.MenuItem.OrderBy(j => j.No);
        }

        var page = db.Page.FirstOrDefault();
        ViewBag.page = page;
        ViewBag.sections = (
            page.Section
            .OrderBy(i => i.No)
            .ToList()
        );

        var settings = db.Setting.FirstOrDefault();
        ViewBag.title = page.Title ?? settings.Title;
        ViewBag.description  = page.Description ?? settings.Description;
        ViewBag.keywords = page.Keywords ?? settings.Keywords;
        return View();
    }

    [Route("~/{title}-{id:int}")]
    public ActionResult Index(string title, int id) {

        var menus = (
            db.Menu
            .Include("MenuItem")
            .ToList()
        );

        foreach (var i in menus) {
            ViewData[i.Name] = i.MenuItem.OrderBy(j => j.No);
        }

        var page = db.Page.Find(id);
        ViewBag.page = page;
        ViewBag.sections = (
            page.Section
            .OrderBy(i => i.No)
            .ToList()
        );

        var settings = db.Setting.FirstOrDefault();
        ViewBag.title = page.Title ?? settings.Title;
        ViewBag.description = page.Description ?? settings.Description;
        ViewBag.keywords = page.Keywords ?? settings.Keywords;
        return View();

    }
}
