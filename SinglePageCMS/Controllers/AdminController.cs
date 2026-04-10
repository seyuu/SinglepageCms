using SinglePageCMS.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

public class AdminController : BaseController {

    [AllowAnonymous]
    [Route("Admin")]
    public ActionResult Index() {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Admin")]
    public ActionResult Index(string kullaniciAdi, string parola) {
        var settings = db.Setting.FirstOrDefault();
        var bootstrapUser = ConfigurationManager.AppSettings["BootstrapAdminUser"];
        var bootstrapPass = ConfigurationManager.AppSettings["BootstrapAdminPassword"];
        var useBootstrap = !string.IsNullOrEmpty(bootstrapUser)
            && !string.IsNullOrEmpty(bootstrapPass)
            && string.Equals(kullaniciAdi, bootstrapUser, StringComparison.Ordinal)
            && string.Equals(parola, bootstrapPass, StringComparison.Ordinal);

        if (useBootstrap) {
            Util.oturumAc(kullaniciAdi);
            return RedirectToAction("Anasayfa");
        }

        if (settings != null
            && string.Equals(kullaniciAdi ?? "", settings.AdminKullaniciAdi ?? "", StringComparison.Ordinal)
            && PasswordHelper.Verify(parola ?? "", settings.AdminSifre)) {
            if (settings.AdminSifre != null && !settings.AdminSifre.StartsWith("$2", StringComparison.Ordinal)) {
                settings.AdminSifre = PasswordHelper.HashPassword(parola);
                db.SaveChanges();
            }
            Util.oturumAc(kullaniciAdi);
            return RedirectToAction("Anasayfa");
        }

        if (settings != null
            && !string.IsNullOrEmpty(settings.SuperAdminKullaniciAdi)
            && string.Equals(kullaniciAdi ?? "", settings.SuperAdminKullaniciAdi ?? "", StringComparison.Ordinal)
            && PasswordHelper.Verify(parola ?? "", settings.SuperAdminSifre)) {
            if (settings.SuperAdminSifre != null && !settings.SuperAdminSifre.StartsWith("$2", StringComparison.Ordinal)) {
                settings.SuperAdminSifre = PasswordHelper.HashPassword(parola);
                db.SaveChanges();
            }
            Util.oturumAc(kullaniciAdi);
            return RedirectToAction("Anasayfa");
        }

        ViewBag.hata = "geçersiz kullanıcı bilgisi";
        return View();
    }

    [Route("Admin/Anasayfa")]
    public ActionResult Anasayfa() {
        return View();
    }

    [Route("Admin/Cikis")]
    public ActionResult Cikis() {
        Util.oturumKapat();
        return RedirectToAction("Index");
    }

}
