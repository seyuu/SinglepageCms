using SinglePageCMS.Models;

public class GalleryController : BaseBlockMultiController<Gallery, GalleryItem> {
}



//using System.Web.Mvc;
//using SinglePageCMS.Models;
//using System.Linq;

//public class GalleryController : BaseController {

//    [ChildActionOnly]
//    public ActionResult View(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return PartialView();
//    }

//    [Route("Admin/Gallery/Add/{SectionID:int}")]
//    public ActionResult Add(int SectionID) {
//        ViewBag.model = new Gallery() {
//            SectionID = SectionID,
//            Width = 12
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/Gallery/Add/{SectionID:int}")]
//    public ActionResult Add(Gallery model) {

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


//    [Route("Admin/Gallery/Edit/{ID:int}")]
//    public ActionResult Edit(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Gallery/Edit/{ID:int}")]
//    public ActionResult Edit(Gallery model) {

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
//    [Route("Admin/Gallery/Items/{GalleryID:int}")]
//    public ActionResult Items(int GalleryID) {
//        ViewBag.GalleryID = GalleryID;
//        ViewBag.model = db.GalleryItem.Where(i => i.GalleryID == GalleryID).ToList();
//        return View();
//    }

//    [Route("Admin/Gallery/ItemAdd/{GalleryID:int}")]
//    public ActionResult ItemAdd(int GalleryID) {
//        ViewBag.sectionID = db.Block.Find(GalleryID).SectionID;
//        ViewBag.model = new GalleryItem() {
//            GalleryID = GalleryID
//        };
//        return View("ItemEdit");
//    }

//    [HttpPost]
//    [Route("Admin/Gallery/ItemAdd/{GalleryID:int}")]
//    public ActionResult ItemAdd(GalleryItem model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.sectionID = db.Block.Find(model.GalleryID).SectionID;
//            ViewBag.model = model;
//            return View("ItemEdit");
//        }

//        //kaydet
//        model.Image = Util.resimYukle("NewImage", model.Image);
//        db.Insert(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            GalleryID = model.GalleryID
//        });

//    }

//    [Route("Admin/Gallery/ItemEdit/{ID:int}")]
//    public ActionResult ItemEdit(int ID) {
//        ViewBag.model = db.GalleryItem.Find(ID);
//        ViewBag.sectionID = ViewBag.model.Gallery.SectionID;
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Gallery/ItemEdit/{ID:int}")]
//    public ActionResult ItemEdit(GalleryItem model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.model = model;
//            ViewBag.sectionID = db.Block.Find(model.GalleryID).SectionID;
//            return View();
//        }

//        //kaydet
//        model.Image = Util.resimYukle("NewImage", model.Image);
//        db.Update(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            GalleryID = model.GalleryID
//        });

//    }

//    [Route("Admin/Gallery/ItemDelete/{ID:int}")]
//    public ActionResult Delete(int ID) {

//        //silinecek model
//        var model = db.GalleryItem.Find(ID);
//        var GalleryID = model.Gallery.ID;

//        //sil
//        db.Delete(model);

//        //başarılı
//        return RedirectToAction("Items", new {
//            GalleryID = GalleryID
//        });

//    }
//}
