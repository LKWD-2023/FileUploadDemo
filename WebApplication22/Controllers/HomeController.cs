using ImageUploads.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=UploadedImages; Integrated Security=true;";

        private IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var repo = new ImagesRepository(_connectionString);
            return View(repo.GetAll());
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image, string title)
        {
            var fileName = $"{Guid.NewGuid()}-{image.FileName}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            image.CopyTo(fs);


            var repo = new ImagesRepository(_connectionString);
            repo.Add(title, fileName);

            return RedirectToAction("index");
        }
    }
}