using SinglePageCMS.Models;
using System.Web.Mvc;
using System.Linq;

public class SettingsController : BaseController {

    [Route("Admin/Settings")]
    public ActionResult Edit() {
        ViewBag.model = db.Setting.FirstOrDefault();
        return View();
    }

    [HttpPost]
    [Route("Admin/Settings")]
    public ActionResult Edit(Setting model, string AdminSifreTekrar) {

        //hatalar hatalar
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            return View();
        }

        //şifre değiştirilmeyecek
        if (string.IsNullOrEmpty(model.AdminSifre)) {
            db.Update(model, "AdminKullaniciAdi", "AdminSifre");
            alertSuccess("Başarılı", "İşlem tamamlandı");
        }

        //parola depiştir
        else  if (model.AdminSifre == AdminSifreTekrar) {
            db.Update(model, "AdminKullaniciAdi");
            alertSuccess("Başarılı", "İşlem tamamlandı");
        }

        //hata
        else {
            alertDanger("Hata", "Parola ve tekrarı farklı");
        }

        //tamamdır
        return RedirectToAction("Edit");

    }

}


//using SinglePageCMS.Models;
//using System;
//using System.Data.Entity;
//using System.Linq;
//using System.Web.Mvc;

//namespace SinglePageCMS.Controllers {
//    public class SettingsController : BaseController {
//        // GET: Settings
//        public ActionResult Index()
//        {
//            ViewBag.model = db.Setting.First();
//            return View(ViewBag.model);
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult Index(Setting Model) {
//            try {
//                var kat = new Setting {

//                    AdminKullaniciAdi = Model.AdminKullaniciAdi,
//                    AdminSifre = Model.AdminSifre,
//                    SuperAdminKullaniciAdi = Model.SuperAdminKullaniciAdi,
//                    SuperAdminSifre = Model.SuperAdminSifre,
//                    Title = Model.Title,
//                    Description = Model.Description,
//                    Keywords = Model.Keywords,
//                    Favicon = Model.Favicon
//                };
//                db.Entry(Model).State = EntityState.Added;
//                db.SaveChanges();
//                TempData["message"] = "Added";
//            }
//            catch (Exception) {

//                TempData["message"] = "Error";
//            }

//            return View("Index");
//        }

//        public ActionResult Edit() {
//            var edit = db.Setting.First();
//            return View(edit);
//        }

//        [HttpPost]
//        public ActionResult Edit(Setting Model, int? Page) {
//            try {
//                db.Entry(Model).State = EntityState.Modified;
//                db.SaveChanges();
//                TempData["message"] = "Added";
//            }
//            catch (Exception) {
//                TempData["message"] = "Error";
//            }

//            return View("Index");
//        }
//    }
//}