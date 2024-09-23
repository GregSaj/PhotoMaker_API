using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoMagic.ChangePhoto;
using PhotoMagic.Database;
using PhotoMagic.Models;

namespace PhotoMagic.Controllers
{
    public class PhotoMagicController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly PhotoMagicDbContext _dbContext;

        public PhotoMagicController(IWebHostEnvironment environment, PhotoMagicDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        // GET: PhotoMagicController
        public IActionResult Welcome()
        {
            ViewData["FooterIsVisible"] = true;
            return View();
        }

        public IActionResult Page(int page = 1)
        {
            
            string viewName = page switch
            {
                1 => "Ready",
                2 => "Camera",
                3 => "Approve",
                4 => "ChooseAIEffect",
                5 => "SaveOrShare",
                _ => "Welcome"
            };

            ViewData["CurrentPage"] = page;

            if (page == 3 && TempData["ImageModel"] != null)
            {
               return RedirectToAction(viewName, new ImageModel{FileName = TempData["ImageModel"].ToString() } );
            }

            return  RedirectToAction(viewName);
        }

        // GET: PhotoMagicController
        public IActionResult Ready()
        {
            ViewData["CurrentPage"] = 1;
            ViewData["FooterIsVisible"] = true;
            return View();
        }

        // GET: PhotoMagicController
        public IActionResult Camera()
        {
            ViewData["CurrentPage"] = 2;
            return View(new ImageModel());
        }
        
        [HttpPost]
        public IActionResult SaveImage()
        {
            ViewData["CurrentPage"] = 3;
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var fileNameToStore = string.Concat(Convert.ToString(Guid.NewGuid()),
                                Path.GetExtension(fileName));
                            //  Path to store the snapshot in local folder
                            var filepath = Path.Combine(_environment.WebRootPath, "Photos") + $@"\{fileNameToStore}";

                            // Save image file in local folder
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                using (FileStream fileStream = System.IO.File.Create(filepath))
                                {
                                    file.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                            }

                            return Json(fileNameToStore);

                            //// Save image file in database
                            //var imgBytes = System.IO.File.ReadAllBytes(filepath);
                            //if (imgBytes != null)
                            //{
                            //    if (imgBytes != null)
                            //    {
                            //        string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                            //        string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                            //        // Code to store into database
                            //        // save filename and image url(base 64 string) to the database
                            //    }
                            //}
                        }
                    }
                    return Json(null);

                }

            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Camera");

        }

        public IActionResult Approve(ImageModel imageModel)
        {
            ViewData["CurrentPage"] = 3;
            return View(imageModel);
        }



        public IActionResult ChooseAIEffect(ImageModel imageModel)
        {
            PhotoRoom photoRoom = new PhotoRoom(_environment);
            photoRoom.ChangePhoto(imageModel);

            ViewData["CurrentPage"] = 4;
            TempData["ImageModel"] = imageModel.FileName;
            return View(imageModel);
        }

        public IActionResult SaveOrShare(int page = 5)
        {
            ViewData["CurrentPage"] = 5;
            return View();
        }

        public IActionResult Finish()
        {
            ViewData["FooterIsVisible"] = true;
            return View();
        }

        public IActionResult BackToStart()
        {
            ViewData["FooterIsVisible"] = true;
            return RedirectToAction("Index", "Home");
        }

        //// GET: PhotoMagicController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PhotoMagicController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PhotoMagicController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PhotoMagicController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PhotoMagicController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PhotoMagicController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PhotoMagicController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
