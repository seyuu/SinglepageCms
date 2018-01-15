using SinglePageCMS.Models;

public class FormController : BaseBlockMultiController<Form, FormItem> {
}


//using System.Web.Mvc;
//using SinglePageCMS.Models;
//using System.Linq;

//public class FormController : BaseController {

//    [ChildActionOnly]
//    public ActionResult View(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return PartialView();
//    }

//    [Route("Admin/Form/Add/{SectionID:int}")]
//    public ActionResult Add(int SectionID) {
//        ViewBag.model = new Form() {
//            SectionID = SectionID,
//            Width = 12
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/Form/Add/{SectionID:int}")]
//    public ActionResult Add(Form model) {

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


//    [Route("Admin/Form/Edit/{ID:int}")]
//    public ActionResult Edit(int ID) {
//        ViewBag.model = db.Block.Find(ID);
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Form/Edit/{ID:int}")]
//    public ActionResult Edit(Form model) {

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

//    //Fields
//    [Route("Admin/Form/Fields/{FormID:int}")]
//    public ActionResult Fields(int FormID) {
//        ViewBag.FormID = FormID;
//        ViewBag.model = db.FormField.Where(i => i.FormID == FormID).ToList();
//        return View();
//    }

//    [Route("Admin/Form/FieldAdd/{FormID:int}")]
//    public ActionResult FieldAdd(int FormID) {
//        ViewBag.sectionID = db.Block.Find(FormID).SectionID;
//        ViewBag.model = new FormField() {
//            FormID = FormID
//        };
//        return View("FieldEdit");
//    }

//    [HttpPost]
//    [Route("Admin/Form/FieldAdd/{FormID:int}")]
//    public ActionResult FieldAdd(FormField model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.sectionID = db.Block.Find(model.FormID).SectionID;
//            ViewBag.model = model;
//            return View("FieldEdit");
//        }

//        //kaydet
//        db.Insert(model);

//        //başarılı
//        return RedirectToAction("Fields", new {
//            FormID = model.FormID
//        });

//    }

//    [Route("Admin/Form/FieldEdit/{ID:int}")]
//    public ActionResult FieldEdit(int ID) {
//        ViewBag.model = db.FormField.Find(ID);
//        ViewBag.sectionID = ViewBag.model.Form.SectionID;
//        return View();
//    }

//    [HttpPost]
//    [Route("Admin/Form/FieldEdit/{ID:int}")]
//    public ActionResult FieldEdit(FormField model) {

//        //hata
//        if (!ModelState.IsValid) {
//            ViewBag.model = model;
//            ViewBag.sectionID = db.Block.Find(model.FormID).SectionID;
//            return View();
//        }

//        //kaydet
//        db.Update(model);

//        //başarılı
//        return RedirectToAction("Fields", new {
//            FormID = model.FormID
//        });

//    }

//    [Route("Admin/Form/FieldDelete/{ID:int}")]
//    public ActionResult Delete(int ID) {

//        //silinecek model
//        var model = db.FormField.Find(ID);
//        var FormID = model.Form.ID;

//        //sil
//        db.Delete(model);

//        //başarılı
//        return RedirectToAction("Fields", new {
//            FormID = FormID
//        });

//    }
//}
