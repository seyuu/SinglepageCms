//using System.Web.Mvc;

//public class SubscribeController: BaseController {

//    //abone olma formu
//    [ChildActionOnly]
//    public ActionResult AdminMenu() {
//        ViewBag.menus = db.Menu.Include("MenuItem").OrderBy(i => i.Name).ToList();
//        return PartialView();
//    }

//    //önceki gönderilmiş epostalar
//    [Route("Admin/Menu/{MenuID:int}")]
//    public ActionResult Index(int MenuID) {
//        var menu = db.Menu.Find(MenuID);
//        ViewBag.menu = menu;
//        ViewBag.model = menu.MenuItem.OrderBy(i => i.No).ToList();
//        return View();
//    }

//    //yeni eposta gödner
//    [Route("Admin/Menu/Add/{MenuID:int}")]
//    public ActionResult Add(int MenuID) {
//        ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
//        ViewBag.model = new MenuItem() {
//            MenuID = MenuID
//        };
//        return View("Edit");
//    }

//    [HttpPost]
//    [Route("Admin/Menu/Add/{MenuID:int}")]
//    public ActionResult Add(MenuItem model, string ExternalUrl) {

//        //hatalar hatalar
//        if (!ModelState.IsValid) {
//            ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
//            ViewBag.model = model;
//            return View("Edit");
//        }

//        //yeni
//        model.Url = string.IsNullOrEmpty(model.Url) ? ExternalUrl : model.Url;
//        model.No = db.MenuItem.Any() ? db.MenuItem.Max(i => i.No) + 1 : 1;
//        db.Insert(model);

//        //tamamdır
//        return RedirectToAction("Index", new {
//            MenuID = model.MenuID
//        });
//    }




//}