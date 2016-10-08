using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using PhotoUpload.Server.Models;

namespace PhotoUpload.Server.Controllers
{
    public class PhotoController : ApiController
    {
        public ResponseModel Get()
        {

            List<string> photos = new List<string>();
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Content/photos"));
            foreach (var file in files)
            {
                if (file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".jpeg") || file.ToLower().EndsWith(".png"))
                {
                    var virtualPath = file.Replace(HttpContext.Current.Server.MapPath("~"), "");
                    virtualPath = "/" + virtualPath;
                    virtualPath = virtualPath.Replace("\\", "/");
                    photos.Add(virtualPath);
                }
            }
            return new ResponseModel { Data = photos };
        }

        // GET: api/Photo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Photo
        public ResponseModel Post()
        {
            try
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/Content/photos");
                HttpFileCollection images = HttpContext.Current.Request.Files;

                if (images.Count > 0)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        var image = images[i];

                        if (image.ContentLength > 0)
                        {
                            try
                            {
                                string path = Path.Combine(directoryPath, image.FileName);
                                image.SaveAs(path);
                            }
                            catch (DirectoryNotFoundException e)
                            {
                                Directory.CreateDirectory(directoryPath);
                                string path = Path.Combine(directoryPath, image.FileName);
                                image.SaveAs(path);
                            }
                            catch (Exception e)
                            {
                                return new ResponseModel(isSuccess: false, message: e.Message);
                            }
                        }
                    }
                    return new ResponseModel(isSuccess: true, message: "Successfull");
                }
            }
            catch (Exception e)
            {
                return new ResponseModel(isSuccess: false, message: e.Message);
            }
            return new ResponseModel(isSuccess: false, message: "False");
        }

        // PUT: api/Photo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Photo/5
        public void Delete(int id)
        {
        }
    }
}
