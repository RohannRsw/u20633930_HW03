using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u20633930_HW03.Models;

namespace u20633930_HW03.Controllers
{
    public class MediaController : Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file, FormCollection collection)
        {
            //Recive option from radio button using form collection
            string value = Convert.ToString(collection["optradio"]);

            //check the option
            if (value == "Document")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Documents"), file.FileName));
            }
            else if (value == "Image")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Images"), file.FileName));
            }
            else
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Videos"), file.FileName));
            }
            return RedirectToAction("Home");
        }
        public ActionResult Files()
        {
            List<FileModel> files = new List<FileModel>();

            string[] Documents = Directory.GetFiles(Server.MapPath("~/Content/Media/Documents"));
            string[] Images = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            string[] Videos = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            foreach (var file in Documents)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "docs";
                files.Add(locatedFile);
            }
            foreach (var file in Images)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "imgs";
                files.Add(locatedFile);
            }
            foreach (var file in Videos)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "vids";
                files.Add(locatedFile);
            }
            return View(files);
        }
        public ActionResult Images()
        {
            List<FileModel> Images = new List<FileModel>();
            string[] Imagelocactions = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            foreach (var file in Imagelocactions)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "imgs";
                Images.Add(locatedFile);
            }


            return View(Images);
        }
        public ActionResult Videos()
        {
            List<FileModel> Videos= new List<FileModel>();
            string[] Videoslocation = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));
            foreach (var file in Videoslocation)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "vids";
                Videos.Add(locatedFile);
            }

            return View(Videos);
        }
        public ActionResult AboutMe()
        {
            return View();
        }
        public FileResult DownloadFile(string fileName, string fileType)
        {
            byte[] bytes = null;

            if (fileType == "docs")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Documents") + fileName);
            }
            else if (fileType == "vids")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Videos") + fileName);
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Images") + fileName);
            }



            return File(bytes, "application/octet-stream", fileName);
        }
        public ActionResult DeleteFile(string FileName, string FileType)
        {
            string filelocation = null;
            if (FileType == "docs")
            {
                filelocation = Server.MapPath("~/Content/Media/Documents/" + FileName);
            }
            else if (FileType == "vids")
            {
                filelocation = Server.MapPath("~/Content/Media/Videos/" + FileName);
            }
            else
            {
                filelocation = Server.MapPath("~/Content/Media/Images/" + FileName);
            }

            System.IO.File.Delete(filelocation);

            return RedirectToAction("Home");
        }
    }
}