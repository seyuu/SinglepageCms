using System.Web.Mvc;
using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

[AllowAnonymous]
public class ThumbnailController: Controller {

    private static string ResolveUploadPath(string uploadsDir, string fileName) {
        if (string.IsNullOrEmpty(fileName)) {
            return null;
        }
        var safeName = Path.GetFileName(fileName);
        if (string.IsNullOrEmpty(safeName)) {
            return null;
        }
        var fullPath = Path.GetFullPath(Path.Combine(uploadsDir, safeName));
        if (!fullPath.StartsWith(uploadsDir, StringComparison.OrdinalIgnoreCase)) {
            return null;
        }
        return File.Exists(fullPath) ? fullPath : null;
    }

    private ActionResult RenderThumbnail(string fullPath, int w, int h) {
        Image img = null;
        Bitmap bmp = null;
        MemoryStream ms = null;
        try {
            img = Image.FromFile(fullPath);
            var _maxWidth = w > 0 ? w : img.Width;
            var _maxHeight = h > 0 ? h : img.Height;
            var _scaleWidth = (float)_maxWidth / img.Width;
            var _scaleHeight = (float)_maxHeight / img.Height;
            var _scale = Math.Min(1f, Math.Min(_scaleWidth, _scaleHeight));
            var _newWidth = (int)(_scale * img.Width);
            var _newHeight = (int)(_scale * img.Height);
            bmp = new Bitmap(img, _newWidth, _newHeight);
            ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }
        finally {
            ms?.Dispose();
            bmp?.Dispose();
            img?.Dispose();
        }
    }

    //[Route("Thumbnail")]
    //public ActionResult Index(int w, int h, string f) {

    //    if (string.IsNullOrEmpty(f)) {
    //        f = "no-photo.png"; 
    //    }

    //    var dir = Server.MapPath("~/Content/uploads");
    //    var path = Path.Combine(dir, f);

    //    var img = Image.FromFile(path);
    //    var bmp = new Bitmap(img, w, h);

    //    var ms = new MemoryStream();
    //    bmp.Save(ms, ImageFormat.Png);
    //    var data = ms.ToArray();

    //    ms.Close();
    //    bmp.Dispose();
    //    img.Dispose();

    //    return File(data, "image/png");
    //}

    [OutputCache(Duration = 30000)]
    [Route("Thumbnail")]
    public ActionResult Index(int w, int h, string f) {

        if (string.IsNullOrEmpty(f)) {
            f = "no-photo.png";
        }

        var dir = Path.GetFullPath(Server.MapPath("~/Content/uploads"));
        var path = ResolveUploadPath(dir, f);
        if (path == null) {
            return HttpNotFound();
        }
        return RenderThumbnail(path, w, h);
    }

    [Route("Crop")]
    public ActionResult Crop(int w, int h, string f) {

        var width = 2048;
        var height = 100;
        var maxWidth = 100;
        var maxHeight = 100;

        if (string.IsNullOrEmpty(f)) {
            f = "no-photo.png";
        }

        var dir = Path.GetFullPath(Server.MapPath("~/Content/uploads"));
        var path = ResolveUploadPath(dir, f);
        if (path == null) {
            return HttpNotFound();
        }
        return RenderThumbnail(path, w, h);
    }

    //vb kodu 
    //Dim _maxWidth As Integer = IIf(MaxWidth > 0, MaxWidth, i.Width) 'ToDo: Unsupported feature: conditional (?) operator.
    //Dim _maxHeight As Integer = IIf(MaxHeight > 0, MaxHeight, i.Height) 'ToDo: Unsupported feature: conditional (?) operator.
    //Dim _scaleWidth As Double = CDbl(_maxWidth) / CDbl(i.Width)
    //Dim _scaleHeight As Double = CDbl(_maxHeight) / CDbl(i.Height)
    //Dim _scale As Double = IIf(_scaleHeight < _scaleWidth, _scaleHeight, _scaleWidth) 'ToDo: Unsupported feature: conditional (?) operator.
    //If _scale > 1 Then _scale = 1


    //Dim _newWidth As Integer = CInt(_scale * i.Width)
    //Dim _newHeight As Integer = CInt(_scale * i.Height)


}
