using System.Web.Mvc;
using System.Linq;
using SinglePageCMS.Models;
using System;

public class BlockController : BaseController {

    //[Route("Admin/Block/{SectionID:int}")]
    //public ActionResult Index(int SectionID) {

    //    ViewBag.sectionID = SectionID;

    //    ViewBag.model = (
    //        db.Block
    //        .Where(i => i.SectionID == SectionID)
    //        .OrderBy(i => i.No)
    //        .ToList()
    //    );

    //    ViewBag.types = new[]  {
    //        "Banner",
    //        "Feature",
    //        "Form",
    //        "Gallery",
    //        "Map",
    //        "Quote",
    //        "Slide",
    //        "Stage",
    //        "Text",
    //        "Video"
    //    };

    //    return View();
    //}

    //[Route("Admin/Block/Add/{SectionID:int}")]
    //public ActionResult Add(int SectionID) {

    //    ViewBag.model = new Block() {
    //        SectionID = SectionID
    //    };

    //    ViewBag.types = new[]  {
    //        "Accordion",
    //        "Banner",
    //        "Feature",
    //        "Form",
    //        "Gallery",
    //        "Map",
    //        "Person",
    //        "Pricing",
    //        "Quote",
    //        "Slide",
    //        "Stage",
    //        "Subscribe",
    //        "Table",
    //        "Text",
    //        "Timeline"
    //    };

    //    return View("Edit");
    //}


    //[HttpPost]
    //[Route("Admin/Block/Add/{SectionID:int}")]
    //public ActionResult Add(Block model) {

    //    //hatalar hatalar
    //    if (!ModelState.IsValid) {
    //        return View();
    //    }

    //    //yeni
    //    model.No = db.Block.Any() ? db.Block.Max(i => i.No) + 1 : 1;
    //    db.Insert(model);

    //    //tamamdır
    //    return RedirectToAction("Index", new {
    //        SectionID = model.SectionID
    //    });

    //}


    //[Route("Admin/Block/Edit/{ID:int}")]
    //public ActionResult Edit(int ID) {
    //    ViewBag.model = db.Block.Find(ID);
    //    ViewBag.types = new[]  {
    //        "Accordion",
    //        "Banner",
    //        "Feature",
    //        "Form",
    //        "Gallery",
    //        "Map",
    //        "Person",
    //        "Pricing",
    //        "Quote",
    //        "Slide",
    //        "Stage",
    //        "Subscribe",
    //        "Table",
    //        "Text",
    //        "Timeline"
    //    };
    //    return View("Edit");
    //}

    //[HttpPost]
    //[Route("Admin/Block/Edit/{ID:int}")]
    //public ActionResult Edit(Block model) {

    //    //hatalar hatalar
    //    if (!ModelState.IsValid) {
    //        ViewBag.model = model;
    //        return View();
    //    }

    //    //yeni
    //    db.Update(model, "No");

    //    //tamamdır
    //    return RedirectToAction("Index", new { SectionID = model.SectionID });
    //}

    [Route("Admin/Block/Delete/{ID:int}")]
    public ActionResult Delete(int ID) {

        //silinecek model
        var model = db.Block.Find(ID);

        //sil
        db.Delete(model);

        //geri git
        var section = db.Section.Find(model.SectionID);
        return RedirectToAction("Index", "Section", new {
            PageID = section.PageID
        });
    }

    [Route("Admin/Block/Up/{ID:int}")]
    public ActionResult Up(int ID) {

        //tüm kayıtlar
        var all = db.Block.OrderBy(i => i.No).ToList();

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
        var section = db.Section.Find(current.SectionID);
        return RedirectToAction("Index", "Section", new {
            PageID = section.PageID
        });

    }

    [Route("Admin/Block/Down/{ID:int}")]
    public ActionResult Down(int ID) {

        //tüm kayıtlar
        var all = db.Block.OrderBy(i => i.No).ToList();

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
        var section = db.Section.Find(current.SectionID);
        return RedirectToAction("Index", "Section", new {
            PageID = section.PageID
        });

    }

    [Route("Admin/Block/Select/{SectionID:int}")]
    public ActionResult Select(int SectionID) {
        ViewBag.sectionID = SectionID;
        ViewBag.model = new string[] {
            "Accordion",
            "Banner",
            "Feature",
            "Form",
            "Gallery",
            "Map",
            "Person",
            "Pricing",
            "Quote",
            "Slide",
            "Stage",
            "Subscribe",
            "Table",
            "Text",
            "Timeline"
        };
        return View();
    }

}
