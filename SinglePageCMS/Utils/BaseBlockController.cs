using System.Web.Mvc;
using System.Linq;
using SinglePageCMS.Models;
using System.Reflection;
using System;
using System.Data.Entity;

public class BaseBlockController<T> : BaseController where T : Block, new() {

    [ChildActionOnly]
    public PartialViewResult View(int ID) {
        ViewBag.model = db.Block.Find(ID);
        return PartialView();
    }

    [Oturum]
    [Route("Admin/{asd}/Add/{SectionID:int}")]
    public ActionResult Add(int SectionID) {
        var model = new T();
        ViewBag.model = new T() {
            SectionID = SectionID,
            Width = 12
        };
        return View("Edit");
    }

    [Oturum]
    [HttpPost]
    [Route("Admin/{asd}/Add/{SectionID:int}")]
    public ActionResult Add(T model) {

        //hata
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View("Edit");
        }

        //kaydet
        model.No = db.Block.Any() ? db.Block.Max(i => i.No) + 1 : 1;
        db.Insert(model);

        //başarılı
        var section = db.Section.Find(model.SectionID);
        return RedirectToAction("Index", "Section", new {
            PageID = section.PageID
        });

    }

    [Oturum]
    [Route("Admin/{asd}/Edit/{ID:int}")]
    public ActionResult Edit(int ID) {
        ViewBag.model = db.Block.Find(ID);
        return View();
    }

    [Oturum]
    [HttpPost]
    [Route("Admin/{asd}/Edit/{ID:int}")]
    public ActionResult Edit(T model) {

        //hata
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View();
        }

        //kaydet
        db.Update(model, "No");

        //başarılı
        var section = db.Section.Find(model.SectionID);
        return RedirectToAction("Index", "Section", new {
            PageID = section.PageID
        });

    }
}
