using System.Linq;
using System.Web.Mvc;
using SinglePageCMS.Models;

public class MenuController : BaseController {

    [ChildActionOnly]
    public ActionResult AdminMenu() {
        ViewBag.menus = db.Menu.Include("MenuItem").OrderBy(i => i.Name).ToList();
        return PartialView();
    }

    [Route("Admin/Menu/{MenuID:int}")]
    public ActionResult Index(int MenuID) {
        var menu = db.Menu.Find(MenuID);
        ViewBag.menu = menu;
        ViewBag.model = menu.MenuItem.OrderBy(i => i.No).ToList();
        return View();
    }

    [Route("Admin/Menu/Add/{MenuID:int}")]
    public ActionResult Add(int MenuID) {
        ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
        ViewBag.model = new MenuItem() {
            MenuID = MenuID
        };
        return View("Edit");
    }

    [HttpPost]
    [Route("Admin/Menu/Add/{MenuID:int}")]
    public ActionResult Add(MenuItem model, string ExternalUrl) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
            ViewBag.model = model;
            return View("Edit");
        }

        //yeni
        model.Url = string.IsNullOrEmpty(model.Url) ? ExternalUrl : model.Url;
        model.No = db.MenuItem.Any() ? db.MenuItem.Max(i => i.No) + 1 : 1;
        db.Insert(model);

        //tamamdır
        return RedirectToAction("Index", new {
            MenuID = model.MenuID
        });
    }

    [Route("Admin/Menu/Edit/{ID:int}")]
    public ActionResult Edit(int ID) {
        var model = db.MenuItem.Find(ID);
        ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
        ViewBag.model = model;
        ViewBag.ExternalUrl = model.Url;
        return View();
    }

    [HttpPost]
    [Route("Admin/Menu/Edit/{ID:int}")]
    public ActionResult Edit(MenuItem model, string ExternalUrl) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
            ViewBag.model = model;
            ViewBag.externalUrl = model.Url;
            return View();
        }

        //yeni
        model.Url = string.IsNullOrEmpty(model.Url) ? ExternalUrl : model.Url;
        db.Update(model, "No");

        //tamamdır
        return RedirectToAction("Index", new {
            MenuID = model.MenuID
        });

    }

    [Route("Admin/Menu/Delete/{ID:int}")]
    public ActionResult Delete(int ID) {

        //silinecek model
        var model = db.MenuItem.Find(ID);

        //sil 
        db.Delete(model);

        //geri git
        return RedirectToAction("Index", new {
            MenuID = model.MenuID
        });
    }

    [Route("Admin/Menu/Up/{ID:int}")]
    public ActionResult Up(int ID) {


        //tüm kayıtlar
        var all = db.MenuItem.OrderBy(i => i.No).ToList();

        //taşınacak kayıt
        var current = all.FirstOrDefault(i => i.ID == ID);

        //en üstte değilse
        var index = all.IndexOf(current);
        if (index > 0) {

            //önceki kayıt
            var previous = all[index - 1];

            //noları değiştir
            var temp = previous.No;
            previous.No = current.No;
            current.No = temp;

            //kaydet
            db.Update(previous);
            db.Update(current);

        }

        //yeniden listele
        return RedirectToAction("Index", new {
            MenuID = current.MenuID
        });

    }

    [Route("Admin/Menu/Down/{ID:int}")]
    public ActionResult Down(int ID) {

        //tüm kayıtlar
        var all = db.MenuItem.OrderBy(i => i.No).ToList();

        //taşınacak kayıt
        var current = all.FirstOrDefault(i => i.ID == ID);

        //en üstte değilse
        var index = all.IndexOf(current);
        if (index < all.Count - 1) {

            //önceki kayıt
            var next = all[index + 1];

            //noları değiştir
            var temp = next.No;
            next.No = current.No;
            current.No = temp;

            //kaydet
            db.Update(next);
            db.Update(current);

        }

        //yeniden listele
        return RedirectToAction("Index", new {
            MenuID = current.MenuID
        });

    }

}