using SinglePageCMS.Models;
using System.Web.Mvc;

public class AdminController : BaseController {

    [Route("Admin")]
    public ActionResult Index() {
        return View();
    }

    [HttpPost]
    [Route("Admin")]
    public ActionResult Index(string kullaniciAdi, string parola) {
        if (kullaniciAdi == "admin" && parola == "asd") {
            Util.oturumAc(kullaniciAdi);
            return RedirectToAction("Anasayfa");
        }
        ViewBag.hata = "geçersiz kullanıcı bilgisi";
        return View();
    }

    [Oturum]
    [Route("Admin/Anasayfa")]
    public ActionResult Anasayfa() {
        return View();
    }

    [Oturum]
    [Route("Admin/Cikis")]
    public ActionResult Cikis() {
        Util.oturumKapat();
        return RedirectToAction("Index");
    }

}
