using SinglePageCMS.Models;
using System.Linq;
using System.Web.Mvc;

public class SectionController : BaseController {

    [Route("Admin/Section/{PageID:int}")]
    public ActionResult Index(int PageID) {

        ViewBag.page = db.Page.Find(PageID);
        ViewBag.types = new[]  {
            "Accordion",
            "Banner",
            "Feature",
            "Form",
            "Gallery",
            "Map",
            "Quote",
            "Slide",
            "BigSlide",
            "Stage",
            "Text",
            "Video"
        };
        return View();
    }

    [Route("Admin/Section/Add/{PageID:int}")]
    public ActionResult Add(int PageID) {
        ViewBag.model = new Section() {
            PageID = PageID
        };
        return View("Edit");
    }

    [HttpPost]
    [Route("Admin/Section/Add/{PageID:int}")]
    public ActionResult Add(Section model) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View("Edit");
        }

        //yeni
        model.No = db.Section.Any() ? db.Section.Max(i => i.No) + 1 : 1;
        db.Insert(model);

        //tamamdır
        return RedirectToAction("Index", new{
            PageID = model.PageID
        });
    }

    [Route("Admin/Section/Edit/{ID:int}")]
    public ActionResult Edit(int ID) {
        ViewBag.model = db.Section.Find(ID);
        return View();
    }

    [HttpPost]
    [Route("Admin/Section/Edit/{ID:int}")]
    public ActionResult Edit(Section model) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View();
        }

        //yeni
        db.Update(model, "No");

        //tamamdır
        return RedirectToAction("Index", new {
            PageID = model.PageID
        });

    }

    [Route("Admin/Section/Delete/{ID:int}")]
    public ActionResult Delete(int ID) {

        //silinecek model
        var model = db.Section.Find(ID);

        //sil 
        db.Delete(model);

        //geri git
        return RedirectToAction("Index", "Section", new {
            PageID = model.PageID
        });
    }

    [Route("Admin/Section/Up/{ID:int}")]
    public ActionResult Up(int ID) {

  
        //tüm kayıtlar
        var all = db.Section.OrderBy(i => i.No).ToList();

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
        return RedirectToAction("Index", new {
            PageID = current.PageID
        });

    }
   
    [Route("Admin/Section/Down/{ID:int}")]
    public ActionResult Down(int ID) {

        //tüm kayıtlar
        var all = db.Section.OrderBy(i => i.No).ToList();

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
        return RedirectToAction("Index", new {
            PageID = current.PageID
        });

    }

}