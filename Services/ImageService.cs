using System;
using System.IO;
using System.Web;

namespace Services
{
    public class ImageService
    {

        public static string GetImagePathRelative(string imagepath)
        {
            return $"/UploadedImages/{imagepath}";
        }

        public string SaveImageToDisk(HttpPostedFileBase httpPostedFile)
        {
            var imagefolder = HttpContext.Current.Server.MapPath("~/UploadedImages");
            var filename = Guid.NewGuid().ToString().Substring(0, 4) + httpPostedFile.FileName;
            var fullpath = Path.Combine(imagefolder, filename);
            if (!Directory.Exists(imagefolder)) Directory.CreateDirectory(imagefolder);
            httpPostedFile.SaveAs(fullpath);
            return filename;
        }

        public bool RemoveImageFromDiskIfExists(string imagefilename)
        {
            var imagefolder = HttpContext.Current.Server.MapPath("~/UploadedImages");
            var fullpath = Path.Combine(imagefolder, imagefilename);
            try
            {
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
