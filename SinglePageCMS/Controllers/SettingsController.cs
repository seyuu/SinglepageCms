using SinglePageCMS.Models;
using System.Linq;
using System.Web.Mvc;

public class SettingsController : BaseController {

    [Route("Admin/Settings")]
    public ActionResult Edit() {
        ViewBag.model = db.Setting.FirstOrDefault();
        return View();
    }

    [HttpPost]
    [Route("Admin/Settings")]
    public ActionResult Edit(Setting model, string AdminSifreTekrar, string SuperAdminSifreTekrar) {

        var existing = db.Setting.FirstOrDefault();
        if (existing == null) {
            alertDanger("Hata", "Ayar kaydı bulunamadı");
            ViewBag.model = model;
            return View();
        }

        if (!ModelState.IsValid) {
            ViewBag.model = existing;
            return View();
        }

        existing.Title = model.Title;
        existing.Description = model.Description;
        existing.Keywords = model.Keywords;
        if (model.Favicon != null) {
            existing.Favicon = model.Favicon;
        }

        if (!string.IsNullOrWhiteSpace(model.AdminKullaniciAdi)) {
            existing.AdminKullaniciAdi = model.AdminKullaniciAdi.Trim();
        }

        if (!string.IsNullOrWhiteSpace(model.SuperAdminKullaniciAdi)) {
            existing.SuperAdminKullaniciAdi = model.SuperAdminKullaniciAdi.Trim();
        }

        if (!string.IsNullOrEmpty(model.AdminSifre)) {
            if (model.AdminSifre != AdminSifreTekrar) {
                alertDanger("Hata", "Yönetici parolası ve tekrarı farklı");
                ViewBag.model = existing;
                return View();
            }
            existing.AdminSifre = PasswordHelper.HashPassword(model.AdminSifre);
        }

        if (!string.IsNullOrEmpty(model.SuperAdminSifre)) {
            if (model.SuperAdminSifre != SuperAdminSifreTekrar) {
                alertDanger("Hata", "Süper yönetici parolası ve tekrarı farklı");
                ViewBag.model = existing;
                return View();
            }
            existing.SuperAdminSifre = PasswordHelper.HashPassword(model.SuperAdminSifre);
        }

        db.SaveChanges();
        alertSuccess("Başarılı", "İşlem tamamlandı");
        return RedirectToAction("Edit");
    }

}
