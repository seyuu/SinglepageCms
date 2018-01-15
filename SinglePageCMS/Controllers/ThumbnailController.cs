using System.Web.Mvc;
using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public class ThumbnailController: Controller {

    [Route("Thumbnail")]
    public ActionResult Index(int w, int h, string f) {

        if (string.IsNullOrEmpty(f)) {
            f = "no-photo.png"; 
        }

        var dir = Server.MapPath("~/Content/uploads");
        var path = Path.Combine(dir, f);

        var img = Image.FromFile(path);
        var bmp = new Bitmap(img, w, h);

        var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);
        var data = ms.ToArray();

        ms.Close();
        bmp.Dispose();
        img.Dispose();

        return File(data, "image/png");
    }

}
