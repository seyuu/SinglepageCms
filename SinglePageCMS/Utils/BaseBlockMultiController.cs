using System.Web.Mvc;
using System.Linq;
using SinglePageCMS.Models;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
public class BaseBlockMultiController<BlockT, ItemT> : 
    BaseBlockController<BlockT>
    where BlockT : Block, new()
    where ItemT : BlockItem, new() {

    public ActionResult Items(int BlockID) {
        ViewBag.BlockID = BlockID;
        ViewBag.model = db.BlockItem.Where(i => i.BlockID == BlockID).ToList();
        return View();
    }

    public ActionResult ItemAdd(int BlockID) {
        ViewBag.sectionID = db.Block.Find(BlockID).SectionID;
        ViewBag.model = new ItemT() {
            BlockID = BlockID
        };
        return View("ItemEdit");
    }

    [HttpPost]
    public ActionResult ItemAdd(ItemT model) {

        //hata
        if (!ModelState.IsValid) {
            ViewBag.sectionID = db.Block.Find(model.Block).SectionID;
            ViewBag.model = model;
            return View("ItemEdit");
        }

        //resim yükle
        foreach (string name in Request.Files.Keys) {
            var type = model.GetType();
            var prop = type.GetProperty(name);
            if (prop.PropertyType == typeof(string)) {
                prop.SetValue(model, Util.resimYukle(name, (string)prop.GetValue(model)));
            }
        }

        //kaydet
        db.Insert(model);

        //başarılı
        return RedirectToAction("Items", new {
            BlockID = model.BlockID
        });
    }

    public ActionResult ItemEdit(int ID) {
        ViewBag.model = db.BlockItem.Find(ID);
        ViewBag.sectionID = ViewBag.model.Block.SectionID;
        return View();
    }

    [HttpPost]
    public ActionResult ItemEdit(ItemT model) {

        //hata
        if (!ModelState.IsValid) {
            ViewBag.model = model;
            ViewBag.sectionID = db.Block.Find(model.BlockID).SectionID;
            return View();
        }

        //resim yükle

        //model tipi ne
        var type = model.GetType();

        //formdan gelen dosyaları şey yap
        foreach (string name in Request.Files.Keys) {

            //file input isminde bir property var mı ki ne?
            var prop = type.GetProperty(name);

            //var sa kesin stringdir yoksa sie
            if (prop.PropertyType != typeof(string)) {
                continue;
            }

            //dosyayı al
            var value = "";
            var httpFile = Request.Files[name];

            //dosya yoksa eski dosyayı ata
            if (httpFile == null || httpFile.ContentLength == 0) {

                //şuanki kayıt
                var oldModel = db.BlockItem.Find(model.ID);

                //modelin database ile olan ilişkisini kes yoksa hata verecek
                var entry = db.Entry((ItemT)oldModel);
                entry.State = EntityState.Detached;

                //eski resmi oku
                value = (string)prop.GetValue(oldModel);
            }

            //yeni resmi oku
            else {
                value = (string)prop.GetValue(model);
            }

            //okunan resmi modele yaz
            prop.SetValue(model, Util.resimYukle(name, value));

        }

        //kaydet
        db.Update(model);

        //başarılı
        return RedirectToAction("Items", new {
            BlockID = model.BlockID
        });

    }

    public ActionResult ItemDelete(int ID) {

        //silinecek model
        var model = db.BlockItem.Find(ID);
        var blockID = model.Block.ID;

        //sil
        db.Delete(model);

        //başarılı
        return RedirectToAction("Items", new {
            BlockID = blockID
        });

    }
}
