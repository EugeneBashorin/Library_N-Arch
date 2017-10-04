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

        //[HttpGet]
        //public ActionResult Index(string bookPublisher = FilterConfiguration._ALL_PUBLISHER, string magazinePublisher = FilterConfiguration._ALL_PUBLISHER, string newspaperPublisher = FilterConfiguration._ALL_PUBLISHER)
        //{
        //    IndexModel indexModel = new IndexModel();
        //    Initialize(indexModel);
        //    indexModel = CheckPublisher(indexModel, bookPublisher, magazinePublisher, newspaperPublisher);

        //    return View(indexModel);
        //}

        [HttpGet]
        public ActionResult Books(string bookPublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            BooksFilterModel bookModel = new BooksFilterModel();
            InitializeBooks(bookModel);
            bookModel = CheckBooksPublisher(bookModel, bookPublisher);

            return View(bookModel);
        }

        [HttpGet]
        public ActionResult Magazines( string magazinePublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            MagazineFilterModel magazineModel = new MagazineFilterModel();
            InitializeMagazines(magazineModel);
            magazineModel = CheckMagazinesPublisher(magazineModel, magazinePublisher);

            return View(magazineModel);
        }

        [HttpGet]
        public ActionResult Newspapers(string newspaperPublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            NewspaperFilterModel newspaperModel = new NewspaperFilterModel();
            InitializeNewspapers(newspaperModel);
            newspaperModel = CheckNewspapersPublisher(newspaperModel,newspaperPublisher);

            return View(newspaperModel);
        }

        //private IndexModel Initialize(IndexModel model)
        //{
        //    List<string> bookPublisherList = homeService.GetBooksPublishers();
        //    List<string> magazinePublisherList = homeService.GetMagazinesPublishers();
        //    List<string> newspaperPublisherList = homeService.GetNewspapersPublishers();

        //    model.BooksFilterModel = new BooksFilterModel();
        //    model.BooksFilterModel.BooksPublisher = new SelectList(bookPublisherList, FilterConfiguration._ALL_PUBLISHER);
        //    model.BooksFilterModel.Books = new List<Book>();

        //    model.MagazineFilterModel = new MagazineFilterModel();
        //    model.MagazineFilterModel.MagazinesPublisher = new SelectList(magazinePublisherList, FilterConfiguration._ALL_PUBLISHER);
        //    model.MagazineFilterModel.Magazines = new List<Magazine>();

        //    model.NewspaperFilterModel = new NewspaperFilterModel();
        //    model.NewspaperFilterModel.NewspapersPublisher = new SelectList(newspaperPublisherList, FilterConfiguration._ALL_PUBLISHER);
        //    model.NewspaperFilterModel.Newspapers = new List<Newspaper>();

        //    return model;
        //}

        private BooksFilterModel InitializeBooks(BooksFilterModel model)
        {
            List<string> bookPublisherList = homeService.GetBooksPublishers();

            model.BooksPublisher = new SelectList(bookPublisherList, FilterConfiguration._ALL_PUBLISHER);
            model.Books = new List<Book>();

            return model;
        }

        private MagazineFilterModel InitializeMagazines(MagazineFilterModel model)
        {
            List<string> magazinePublisherList = homeService.GetMagazinesPublishers();

            model.MagazinesPublisher = new SelectList(magazinePublisherList, FilterConfiguration._ALL_PUBLISHER);
            model.Magazines = new List<Magazine>();

            return model;
        }

        private NewspaperFilterModel InitializeNewspapers(NewspaperFilterModel model)
        {
            List<string> newspaperPublisherList = homeService.GetMagazinesPublishers();

            model.NewspapersPublisher = new SelectList(newspaperPublisherList, FilterConfiguration._ALL_PUBLISHER);
            model.Newspapers = new List<Newspaper>();

            return model;
        }

        //public IndexModel CheckPublisher(IndexModel model, string bookPublisher = FilterConfiguration._ALL_PUBLISHER, string magazinePublisher = FilterConfiguration._ALL_PUBLISHER, string newspaperPublisher = FilterConfiguration._ALL_PUBLISHER)
        //{
        //    List<Book> bookList = homeService.CheckBookPublisher(bookPublisher);
        //    List<Magazine> magazineList = homeService.CheckMagazinePublisher(magazinePublisher);
        //    List<Newspaper> newspaperList = homeService.CheckNewspaperPublisher(newspaperPublisher);

        //    model.BooksFilterModel.Books = bookList;
        //    model.MagazineFilterModel.Magazines = magazineList;
        //    model.NewspaperFilterModel.Newspapers = newspaperList;

        //    return model;
        //}

        public BooksFilterModel CheckBooksPublisher(BooksFilterModel model, string bookPublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            List<Book> bookList = homeService.CheckBookPublisher(bookPublisher);
            model.Books = bookList;
            return model;
        }

        public MagazineFilterModel CheckMagazinesPublisher(MagazineFilterModel model, string magazinePublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            List<Magazine> magazineList = homeService.CheckMagazinePublisher(magazinePublisher);
            model.Magazines = magazineList;
            return model;
        }

        public NewspaperFilterModel CheckNewspapersPublisher(NewspaperFilterModel model, string newspaperPublisher = FilterConfiguration._ALL_PUBLISHER)
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

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult CreateBook()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateBook(Book book)
        //{
        //    if (book.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price should be positive");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        homeService.AddBook(book);

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult ShowBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Book book = homeService.GetBook(id);
        //    return View(book);
        //}

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult EditBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Book book = homeService.GetBook(id);
        //    return View(book);
        //}

        //[HttpPost]
        //public ActionResult EditBook(int? Id, Book newBook)
        //{
        //    if (Id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (newBook.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price should be positive");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        homeService.UpdateBook(Id, newBook);

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //**************CRUD FOR KENDO GRID
        
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
        //************************CRUD FOR KENDO GRID

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult DeleteBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Book book = homeService.GetBook(id);
        //    return PartialView(book);
        //}

        
        //[HttpPost, ActionName("DeleteBook")]
        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //public ActionResult DeleteConfirmedBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    homeService.DeleteBook(id);
        //    return RedirectToAction("Index");
        //}

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult CreateMagazine()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateMagazine(Magazine magazine)
        //{
        //    if (magazine.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price should be positive");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        homeService.AddMagazine(magazine);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult ShowMagazine(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Magazine magazine = homeService.GetMagazine(id);
        //    return View(magazine);
        //}

        //[HttpGet]
        //public ActionResult EditMagazine(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Magazine magazine = homeService.GetMagazine(id);
        //    return View(magazine);
        //}

        //[HttpPost]
        //public ActionResult EditMagazine(int? id, Magazine newMagazine)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (newMagazine.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price should be positive");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        homeService.UpdateMagazine(id, newMagazine);

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult DeleteMagazine(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Magazine magazine = homeService.GetMagazine(id);
        //    return PartialView(magazine);
        //}

        //[HttpPost, ActionName("DeleteMagazine")]
        //public ActionResult DeleteConfirmedMagazine(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    homeService.DeleteMagazine(id);
        //    return RedirectToAction("Index");
        //}

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult CreateNewspaper()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateNewspaper(Newspaper newspaper)
        //{
        //    if (newspaper.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price should be positive");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        homeService.AddNewspaper(newspaper);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult ShowNewspaper(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Newspaper newspaper = homeService.GetNewspaper(id);
        //    return View(newspaper);
        //}

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult EditNewspaper(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Newspaper newspaper = homeService.GetNewspaper(id);
        //    return View(newspaper);
        //}

        //[HttpPost]
        //public ActionResult EditNewspaper(int? Id, Newspaper newNewspaper)
        //{
        //    if (Id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (newNewspaper.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price should be positive");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        homeService.UpdateNewspaper(Id, newNewspaper);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
        //[HttpGet]
        //public ActionResult DeleteNewspaper(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Newspaper newspaper = homeService.GetNewspaper(id);
        //    return PartialView(newspaper);
        //}

        //[HttpPost, ActionName("DeleteNewspaper")]
        //public ActionResult DeleteConfirmedNewspaper(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    homeService.DeleteNewspaper(id);
        //    return RedirectToAction("Index");
        //}
    }
}