using SinglePageCMS.Models;

public class BannerController : BaseBlockController<Banner> {

}

//using System.Web.Mvc;
//using System.Linq;
//using SinglePageCMS.Models;

//public class BannerController : BaseController {

//    [ChildActionOnly]
//    public PartialViewResult View(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return PartialView();
//    }

//    [Route("Admin/Banner/Add/{SectionID:int}")]
//    public ActionResult Add(int SectionID) {
//        ViewBag.model = new Banner() {
//            SectionID = SectionID,
//            Width = 12
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/Banner/Add/{SectionID:int}")]
//    public ActionResult Add(Banner model) {

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


//    [Route("Admin/Banner/Edit/{ID:int}")]
//    public ActionResult Edit(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Banner/Edit/{ID:int}")]
//    public ActionResult Edit(Banner model) {

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
//}
