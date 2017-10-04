using BusinessLogicLayer.Interfaces;
using ConfigurationData.Configurations;
using Entities.Entities;
using LibraryProject.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IHomeService homeService;

        public HomeController(IHomeService homeservice)
        {
            homeService = homeservice;
        }

        [HttpGet]
        public ActionResult Books(string bookPublisher)
        {
            BooksFilterModel bookModel = new BooksFilterModel();
            InitializeBooks(bookModel);
            bookModel = CheckBooksPublisher(bookModel, bookPublisher);

            return View(bookModel);
        }

        [HttpGet]
        public ActionResult Magazines( string magazinePublisher )
        {
            MagazineFilterModel magazineModel = new MagazineFilterModel();
            InitializeMagazines(magazineModel);
            magazineModel = CheckMagazinesPublisher(magazineModel, magazinePublisher);

            return View(magazineModel);
        }

        [HttpGet]
        public ActionResult Newspapers(string newspaperPublisher)
        {
            NewspaperFilterModel newspaperModel = new NewspaperFilterModel();
            InitializeNewspapers(newspaperModel);
            newspaperModel = CheckNewspapersPublisher(newspaperModel,newspaperPublisher);

            return View(newspaperModel);
        }
      
        private BooksFilterModel InitializeBooks(BooksFilterModel model)
        {
            List<string> bookPublisherList = homeService.GetBooksPublishers();

            model.BooksPublisher = new SelectList(bookPublisherList);
            model.Books = new List<Book>();

            return model;
        }

        private MagazineFilterModel InitializeMagazines(MagazineFilterModel model)
        {
            List<string> magazinePublisherList = homeService.GetMagazinesPublishers();

            model.MagazinesPublisher = new SelectList(magazinePublisherList);
            model.Magazines = new List<Magazine>();

            return model;
        }

        private NewspaperFilterModel InitializeNewspapers(NewspaperFilterModel model)
        {
            List<string> newspaperPublisherList = homeService.GetMagazinesPublishers();

            model.NewspapersPublisher = new SelectList(newspaperPublisherList);
            model.Newspapers = new List<Newspaper>();

            return model;
        }

        public BooksFilterModel CheckBooksPublisher(BooksFilterModel model, string bookPublisher )
        {
            List<Book> bookList = homeService.CheckBookPublisher(bookPublisher);
            model.Books = bookList;
            return model;
        }

        public MagazineFilterModel CheckMagazinesPublisher(MagazineFilterModel model, string magazinePublisher )
        {
            List<Magazine> magazineList = homeService.CheckMagazinePublisher(magazinePublisher);
            model.Magazines = magazineList;
            return model;
        }

        public NewspaperFilterModel CheckNewspapersPublisher(NewspaperFilterModel model, string newspaperPublisher )
        {
            List<Newspaper> newspaperList = homeService.CheckNewspaperPublisher(newspaperPublisher);
            model.Newspapers= newspaperList;
            return model;
        }

        public ActionResult GetBooksList()
        {
            homeService.GetBooksTxtList();
            return RedirectToAction("Books");
        }

        public ActionResult GetBooksXmlList()
        {
            homeService.GetBooksXmlList();
            return RedirectToAction("Books");
        }

        public ActionResult GetNewspapersList()
        {
            homeService.GetNewspapersTxtList();
            return RedirectToAction("Books");
        }

        public ActionResult GetNewspapersXmlList()
        {
            homeService.GetNewspapersXmlList();
            return RedirectToAction("Newspapers");
        }

        public ActionResult GetMagazinesList()
        {
            homeService.GetMagazinesTxtList();
            return RedirectToAction("Magazines");
        }

        public ActionResult GetMagazinesXmlList()
        {
            homeService.GetMagazinesXmlList();
            return RedirectToAction("Magazines");
        }
             
        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult CreateNewBook(Book book)
        {
            if (book.Price < 0)
            {
                ModelState.AddModelError("Price", "Price should be positive");
            }
            if (ModelState.IsValid)
            {
                homeService.AddBook(book);

                return Json(book);
            }
            return Json(HttpStatusCode.NotModified);
        }
      
        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult EditBook(int? Id, Book newBook)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            if (newBook.Price < 0)
            {
                ModelState.AddModelError("Price", "Price should be positive");
            }
            if (ModelState.IsValid)
            {
                homeService.UpdateBook(Id, newBook);
                return Json(newBook);
            }
            return Json(HttpStatusCode.NotModified);
        }

        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult ConfirmedDeleteBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            homeService.DeleteBook(id);
            return Json(HttpStatusCode.OK);
        }
        
        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult CreateNewMagazine(Magazine magazine)
        {
            if (magazine.Price < 0)
            {
                ModelState.AddModelError("Price", "Price should be positive");
            }
            if (ModelState.IsValid)
            {
                homeService.AddMagazine(magazine);

                return Json(magazine);
            }
            return Json(HttpStatusCode.NotModified);
        }

        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult EditMagazine(int? Id, Magazine newMagazine)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            if (newMagazine.Price < 0)
            {
                ModelState.AddModelError("Price", "Price should be positive");
            }
            if (ModelState.IsValid)
            {
                homeService.UpdateMagazine(Id, newMagazine);
                return Json(newMagazine);
            }
            return Json(HttpStatusCode.NotModified);
        }

        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult ConfirmedDeleteMagazine(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            homeService.DeleteMagazine(id);
            return Json(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult CreateNewNewspaper(Newspaper newspaper)
        {
            if (newspaper.Price < 0)
            {
                ModelState.AddModelError("Price", "Price should be positive");
            }
            if (ModelState.IsValid)
            {
                homeService.AddNewspaper(newspaper);

                return Json(newspaper);
            }
            return Json(HttpStatusCode.NotModified);
        }

        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult EditNewspaper(int? Id, Newspaper newNewspaper)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            if (newNewspaper.Price < 0)
            {
                ModelState.AddModelError("Price", "Price should be positive");
            }
            if (ModelState.IsValid)
            {
                homeService.UpdateNewspaper(Id, newNewspaper);
                return Json(newNewspaper);
            }
            return Json(HttpStatusCode.NotModified);
        }

        [HttpPost]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        public ActionResult ConfirmedDeleteNewspaper(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            homeService.DeleteNewspaper(id);
            return Json(HttpStatusCode.OK);
        }      
    }
}