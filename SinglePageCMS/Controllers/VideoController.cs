using SinglePageCMS.Models;

public class VideoController : BaseBlockController<Video> {

}

//using System.Web.Mvc;
//using SinglePageCMS.Models;
//using System.Linq;

//public class VideoController : BaseController {

//    [ChildActionOnly]
//    public PartialViewResult View(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return PartialView();
//    }

//    [Route("Admin/Video/Add/{SectionID:int}")]
//    public ActionResult Add(int SectionID) {
//        ViewBag.model = new Video() {
//            SectionID = SectionID,
//            Width = 12
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/Video/Add/{SectionID:int}")]
//    public ActionResult Add(Video model) {

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

//    [Route("Admin/Video/Edit/{ID:int}")]
//    public ActionResult Edit(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Video/Edit/{ID:int}")]
//    public ActionResult Edit(Video model) {

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