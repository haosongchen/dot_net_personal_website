using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoursesApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoursesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment_;
        private string webRootPath = null;
        private string filePath = null;

        public FilesController(IHostingEnvironment hostingEnvironment)
        {
            hostingEnvironment_ = hostingEnvironment;
            webRootPath = hostingEnvironment_.WebRootPath;
            filePath = Path.Combine(webRootPath, "FileStorage");
        }

 
        //----< download single file in wwwroot\FileStorage >------

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IActionResult> Download()
        {
            List<string> files = null;
            string file = "";
           
           
                files = Directory.GetFiles(filePath).ToList<string>();
                int LastFileIndex = files.Count - 1;
                if (LastFileIndex>=0)
                    file = Path.GetFileName(files[LastFileIndex]);
                else
                    return NotFound();
            file = files[LastFileIndex];

            var memory = new MemoryStream();
           
            using (var stream = new FileStream(file, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(file), Path.GetFileName(file));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
      {
        {".cs", "application/C#" },
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"}
      };
        }
        //----< upload file >--------------------------------------

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var request = HttpContext.Request;
            foreach (var file in request.Form.Files)
            {
                if (file.Length > 0)
                {
                    var path = Path.Combine(filePath, file.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.UploadErrorHandle), "Home");
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //// POST api/<controller>
        //// This is the usual technique for uploading files, but I never got more than
        //// one file.  Something wrong with the configuration of my IFormFile model?
        //
        //[HttpPost]
        //public async Task<IActionResult> Post(IList<IFormFile> files)
        //{
        //  var dummy = HttpContext.Request;  // statement for debugging
        //  foreach (var file in files)
        //  {
        //    if (file.Length > 0)
        //    {
        //      var path = Path.Combine(filePath, file.FileName);
        //      using (var fileStream = new FileStream(path, FileMode.Create))
        //      {
        //        await file.CopyToAsync(fileStream);
        //      }
        //    }
        //  }
        //  return Ok();
        //}

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // ToDo
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // ToDo
        }
    }
}
