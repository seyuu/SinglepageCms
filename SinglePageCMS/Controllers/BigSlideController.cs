using SinglePageCMS.Models;

public class BigSlideController : BaseBlockMultiController<BigSlide, BigSlideItem> {
}

//using System.Web.Mvc;
//using SinglePageCMS.Models;
//using System.Linq;

//public class BigSlideController : BaseController {

//    [ChildActionOnly]
//    public ActionResult View(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return PartialView();
//    }

//    [Route("Admin/BigSlide/Add/{SectionID:int}")]
//    public ActionResult Add(int SectionID) {
//        ViewBag.model = new Slide() {
//            SectionID = SectionID,
//            Width = 12
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/BigSlide/Add/{SectionID:int}")]
//    public ActionResult Add(Slide model) {

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


//    [Route("Admin/BigSlide/Edit/{ID:int}")]
//    public ActionResult Edit(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/BigSlide/Edit/{ID:int}")]
//    public ActionResult Edit(Slide model) {

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
//    [Route("Admin/BigSlide/Items/{SlideID:int}")]
//    public ActionResult Items(int SlideID) {
//        ViewBag.slideID = SlideID;
//        ViewBag.model = db.SlideItem.Where(i => i.SlideID == SlideID).OrderBy(i => i.No).ToList();
//        return View();
//    }

//    [Route("Admin/BigSlide/ItemAdd/{SlideID:int}")]
//    public ActionResult ItemAdd(int SlideID) {
//        ViewBag.sectionID = db.Block.Find(SlideID).SectionID;
//        ViewBag.model = new SlideItem() {
//            SlideID = SlideID
//        };
//        return View("ItemEdit");
//    }

//    [HttpPost]
//    [Route("Admin/BigSlide/ItemAdd/{SlideID:int}")]
//    public ActionResult ItemAdd(SlideItem model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.sectionID = db.Block.Find(model.SlideID).SectionID;
//            ViewBag.model = model;
//            return View("ItemEdit");
//        }

//        //kaydet
//        model.Image = Util.resimYukle("NewImage", model.Image);
//        db.Insert(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            SlideID = model.SlideID
//        });

//    }

//    [Route("Admin/BigSlide/ItemEdit/{ID:int}")]
//    public ActionResult ItemEdit(int ID) {
//        ViewBag.model = db.SlideItem.Find(ID);
//        ViewBag.sectionID = ViewBag.model.Slide.SectionID;
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/BigSlide/ItemEdit/{ID:int}")]
//    public ActionResult ItemEdit(SlideItem model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.model = model;
//            ViewBag.sectionID = db.Block.Find(model.SlideID).SectionID;
//            return View();
//        }

//        //kaydet
//        model.Image = Util.resimYukle("NewImage", model.Image);
//        db.Update(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            SlideID = model.SlideID
//        });

//    }

//    [Route("Admin/BigSlide/ItemDelete/{ID:int}")]
//    public ActionResult Delete(int ID) {

//        //silinecek model
//        var model = db.SlideItem.Find(ID);
//        var slideID = model.Slide.ID;

//        //sil
//        db.Delete(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            SlideID = slideID
//        });

//    }
//}