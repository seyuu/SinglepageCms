using SinglePageCMS.Models;

public class QuoteController : BaseBlockMultiController<Quote, QuoteItem> {
}

//using System.Web.Mvc;
//using SinglePageCMS.Models;
//using System.Linq;

//public class QuoteController : BaseController {

//    [ChildActionOnly]
//    public ActionResult View(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return PartialView();
//    }

//    [Route("Admin/Quote/Add/{SectionID:int}")]
//    public ActionResult Add(int SectionID) {
//        ViewBag.model = new Quote() {
//            SectionID = SectionID,
//            Width = 12
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/Quote/Add/{SectionID:int}")]
//    public ActionResult Add(Quote model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.model = model;
//            return View("Edit");
//        }

//        //kaydet
//        model.No = db.Block.Any() ? db.Block.Max(i => i.No) + 1 : 1;
//        db.Insert(model);

//        //başarılı
//        return RedirectToAction("Index", "Section", new {
//            SectionID = model.SectionID
//        });

//    }


//    [Route("Admin/Quote/Edit/{ID:int}")]
//    public ActionResult Edit(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Quote/Edit/{ID:int}")]
//    public ActionResult Edit(Quote model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.model = model;
//            return View();
//        }

//        //kaydet
//        db.Update(model);

//        //başarılı
//        return RedirectToAction("Index", "Section", new {
//            SectionID = model.SectionID
//        });

//    }

//    //items
//    [Route("Admin/Quote/Items/{QuoteID:int}")]
//    public ActionResult Items(int QuoteID) {
//        ViewBag.QuoteID = QuoteID;
//        ViewBag.model = db.QuoteItem.Where(i => i.QuoteID == QuoteID).OrderBy(i => i.No).ToList();
//        return View();
//    }

//    [Route("Admin/Quote/ItemAdd/{QuoteID:int}")]
//    public ActionResult ItemAdd(int QuoteID) {
//        ViewBag.sectionID = db.Block.Find(QuoteID).SectionID;
//        ViewBag.model = new QuoteItem() {
//            QuoteID = QuoteID
//        };
//        return View("ItemEdit");
//    }

//    [HttpPost]
//    [Route("Admin/Quote/ItemAdd/{QuoteID:int}")]
//    public ActionResult ItemAdd(QuoteItem model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.sectionID = db.Block.Find(model.QuoteID).SectionID;
//            ViewBag.model = model;
//            return View("ItemEdit");
//        }

//        //kaydet
//        db.Insert(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            QuoteID = model.QuoteID
//        });

//    }

//    [Route("Admin/Quote/ItemEdit/{ID:int}")]
//    public ActionResult ItemEdit(int ID) {
//        ViewBag.model = db.QuoteItem.Find(ID);
//        ViewBag.sectionID = ViewBag.model.Quote.SectionID;
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Quote/ItemEdit/{ID:int}")]
//    public ActionResult ItemEdit(QuoteItem model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.model = model;
//            ViewBag.sectionID = db.Block.Find(model.QuoteID).SectionID;
//            return View();
//        }

//        //kaydet
//        db.Update(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            QuoteID = model.QuoteID
//        });

//    }

//    [Route("Admin/Quote/ItemDelete/{ID:int}")]
//    public ActionResult Delete(int ID) {

//        //silinecek model
//        var model = db.QuoteItem.Find(ID);
//        var QuoteID = model.Quote.ID;

//        //sil
//        db.Delete(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            QuoteID = QuoteID
//        });

//    }
//}
