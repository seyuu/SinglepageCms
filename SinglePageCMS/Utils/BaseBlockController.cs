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
    [ValidateInput(false)]
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
    [ValidateInput(false)]
    public ActionResult Add(T model) {

        //hata
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View("Edit");
        }

        //resim yükle
        foreach (string name in Request.Files.Keys) {
            var type = model.GetType();
            var prop = type.GetProperty(name);
            if (prop.PropertyType == typeof(string)) {
                prop.SetValue(model, Util.resimYukle(name, (string)prop.GetValue(model)));
            }
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
    [ValidateInput(false)]
    public ActionResult Edit(int ID) {
        ViewBag.model = db.Block.Find(ID);
        return View();
    }

    [Oturum]
    [HttpPost]
    [ValidateInput(false)]
    public ActionResult Edit(T model) {

        //hata
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View();
        }

        //resim yükle
        foreach (string name in Request.Files.Keys) {
            var type = model.GetType();
            var prop = type.GetProperty(name);
            if (prop.PropertyType == typeof(string)) {
                prop.SetValue(model, Util.resimYukle(name, (string)prop.GetValue(model)));
            }
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
