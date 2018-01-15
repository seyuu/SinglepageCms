using SinglePageCMS.Models;
using System.Linq;
using System.Web.Mvc;

public class PageController : BaseController {

    [ChildActionOnly]
    public ActionResult AdminMenu() {
        ViewBag.menus = db.Page.OrderBy(i => i.Title).ToList();
        return PartialView();
    }


    [Route("Admin/Page/Add")]
    public ActionResult Add() {
        ViewBag.model = new Page() {
            Active = true
        };
        return View("Edit");
    }

    [HttpPost]
    [Route("Admin/Page/Add")]
    public ActionResult Add(Page model) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View("Edit");
        }

        //yeni
        db.Insert(model);

        //section listele
        return RedirectToAction("Index", "Section", new {
            PageID = model.ID
        });
    }

    [Route("Admin/Page/Edit/{ID:int}")]
    public ActionResult Edit(int ID) {
        ViewBag.model = db.Page.Find(ID);
        return View();
    }

    [HttpPost]
    [Route("Admin/Page/Edit/{ID:int}")]
    public ActionResult Edit(Page model) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View();
        }

        //yeni
        db.Update(model);

        //section listele
        return RedirectToAction("Index", "Section", new {
            PageID = model.ID
        });

    }

    [Route("Admin/Page/Delete/{ID:int}")]
    public ActionResult Delete(int ID) {

        //silinecek model
        var model = db.Page.Find(ID);

        //sil 
        db.Delete(model);

        //anasayfaya yönlendir
        return RedirectToAction("Index", "Admin");
    }

}