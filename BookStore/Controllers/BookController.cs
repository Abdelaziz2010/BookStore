using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepository<Book> bookRepository,
            IBookStoreRepository<Author> authorRepository,
            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.GetAll();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Get(id);
            return View(book);
        }

        // GET: BookController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            try
            {
                string fileName=UploadFile(model.File)??string.Empty;
                if (model.AuthorId == -1)
                {
                    ViewBag.Message = "Please select an author from the list!";                   
                    return View(GetAllAuthors());
                }
                var author = authorRepository.Get(model.AuthorId);
                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = author,
                    ImageUrl=fileName
                };
                bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
            /*
            //using ModelState
            if (ModelState.IsValid)
            {
                try
                {
                   //logic here
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You have to fill all the required fields!!");
            return View(GetAllAuthors());
             */
        }

        // GET: BookController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Get(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.GetAll().ToList(),
                ImageUrl=book.ImageUrl
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                string fileName = UploadFile(viewModel.File,viewModel.ImageUrl);              
                
                var author = authorRepository.Get(viewModel.AuthorId);
                Book book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageUrl = fileName
                };
                bookRepository.Update(viewModel.AuthorId, book);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Get(id);

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        List<Author> FillSelectList()
        {
            var author = authorRepository.GetAll().ToList();
            author.Insert(0, new Author { Id = -1, FullName = "--- Please select an author ---" });
            return author;
        }

        //return all authors
        BookAuthorViewModel GetAllAuthors()
        {
            var Vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return Vmodel;
        }
        string UploadFile(IFormFile file)
        {

            if (file != null)
            {
                //hosting.WebRootPath:=wwwroot
                //the path of th file
                string uploads = Path.Combine(hosting.WebRootPath, "books");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }
            else
                return null;
        }

        string UploadFile(IFormFile file,string ImageUrl)
        {

            if (file != null)
            {
                //hosting.WebRootPath:=wwwroot
                //the path of th file
                string uploads = Path.Combine(hosting.WebRootPath, "books");
               
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
        public ActionResult Search(string term)
        {
            var result= bookRepository.Search(term);

            return View("Index",result);
        }
    }
}
