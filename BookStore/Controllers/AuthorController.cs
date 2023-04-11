using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.IO;
namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        //inject service(Dependency)
        public AuthorController(IBookStoreRepository<Author> authorRepository,
               IHostingEnvironment hosting)
        {
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = authorRepository.GetAll();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepository.Get(id);
            return View(author);
        }

        // GET: AuthorController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {

                string fileName = UploadFile(author.File) ?? string.Empty;
                
                author.ImageUrl= fileName;
                authorRepository.Add(author);
                return RedirectToAction("Index");  
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var author=authorRepository.Get(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                string fileName = UploadFile(author.File, author.ImageUrl);

                author.ImageUrl = fileName;
                authorRepository.Update(id, author);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var author = authorRepository.Get(id);
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        string UploadFile(IFormFile file)
        {

            if (file != null)
            {
                //hosting.WebRootPath:=wwwroot
                //the path of th file
                string uploads = Path.Combine(hosting.WebRootPath, "writers");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }
            else
                return null;
        }

        string UploadFile(IFormFile file, string ImageUrl)
        {

            if (file != null)
            {
                //hosting.WebRootPath:=wwwroot
                //the path of th file
                string uploads = Path.Combine(hosting.WebRootPath, "writers");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, ImageUrl);
                if (oldPath != newPath)
                {
                    //delete the old file
                    System.IO.File.Delete(oldPath);
                    //save the new file
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;
            }
            else
                return ImageUrl;
        }
    }
}

