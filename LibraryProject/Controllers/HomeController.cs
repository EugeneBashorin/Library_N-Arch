using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using ConfigurationData.Configurations;
using Entities.Entities;
using LibraryProject.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        IHomeService homeService;

        public HomeController(IHomeService homeservice)
        {
            homeService = homeservice;
        }

        [HttpGet]
        public ActionResult Index(string bookPublisher = FilterConfiguration._ALL_PUBLISHER, string magazinePublisher = FilterConfiguration._ALL_PUBLISHER, string newspaperPublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            CheckRole();
            IndexModel indexModel = new IndexModel();
            Initialize(indexModel);
            indexModel = CheckPublisher(indexModel, bookPublisher, magazinePublisher, newspaperPublisher);

            return View(indexModel);
        }

        private void CheckRole()
        {
            if (User.IsInRole(IdentityConfiguration._USER_ROLE))
            {
                ViewBag.hideElement = ViewsElementsConfiguration._ATTRIBUTES_STATE_OFF;
            }

            if (User.IsInRole(IdentityConfiguration._ADMIN_ROLE) & User.IsInRole(IdentityConfiguration._USER_ROLE))
            {
                ViewBag.hideElement = ViewsElementsConfiguration._ATTRIBUTES_STATE_ON;
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.accountElementState = ViewsElementsConfiguration._ATTRIBUTES_STATE_OFF;
                ViewBag.logoutLinkElement = ViewsElementsConfiguration._ATTRIBUTES_STATE_ON;
            }

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.accountElementState = ViewsElementsConfiguration._ATTRIBUTES_STATE_ON;
                ViewBag.logoutLinkElement = ViewsElementsConfiguration._ATTRIBUTES_STATE_OFF;
            }
        }

        private IndexModel Initialize(IndexModel model)
        {
            List<string> bookPublisherList = homeService.GetBooksPublishers();
            List<string> magazinePublisherList = homeService.GetMagazinesPublishers();
            List<string> newspaperPublisherList = homeService.GetNewspapersPublishers();

            model.BooksFilterModel = new BooksFilterModel();
            model.BooksFilterModel.BooksPublisher = new SelectList(bookPublisherList, FilterConfiguration._ALL_PUBLISHER);
            model.BooksFilterModel.Books = new List<Book>();

            model.MagazineFilterModel = new MagazineFilterModel();
            model.MagazineFilterModel.MagazinesPublisher = new SelectList(magazinePublisherList, FilterConfiguration._ALL_PUBLISHER);           
            model.MagazineFilterModel.Magazines = new List<Magazine>();

            model.NewspaperFilterModel = new NewspaperFilterModel();
            model.NewspaperFilterModel.NewspapersPublisher = new SelectList(newspaperPublisherList, FilterConfiguration._ALL_PUBLISHER);
            model.NewspaperFilterModel.Newspapers = new List<Newspaper>();

            return model;
        }

        public IndexModel CheckPublisher(IndexModel model, string bookPublisher = FilterConfiguration._ALL_PUBLISHER, string magazinePublisher = FilterConfiguration._ALL_PUBLISHER, string newspaperPublisher = FilterConfiguration._ALL_PUBLISHER)
        {
            List<Book> bookList = homeService.CheckBookPublisher(bookPublisher);
            List<Magazine> magazineList = homeService.CheckMagazinePublisher(magazinePublisher);
            List<Newspaper> newspaperList = homeService.CheckNewspaperPublisher(newspaperPublisher);

            model.BooksFilterModel.Books = bookList;
            model.MagazineFilterModel.Magazines = magazineList;
            model.NewspaperFilterModel.Newspapers = newspaperList; 

            return model;
        }

        public ActionResult GetBooksList()
        {
            homeService.GetBooksTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksXmlList()
        {
            homeService.GetBooksXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersList()
        {
            homeService.GetNewspapersTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersXmlList()
        {
            homeService.GetNewspapersXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesList()
        {
            homeService.GetMagazinesTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesXmlList()
        {
            homeService.GetMagazinesXmlList();
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            homeService.AddBook(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowBook(int id)
        {
            Book book = homeService.GetBook(id);
            return View(book);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult EditBook(int id)
        {
            Book book = homeService.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(int Id, Book newBook)
        {
            homeService.UpdateBook(Id, newBook);
            return RedirectToAction("Index");
        }


        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            Book book = homeService.GetBook(id);
            return PartialView(book);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmedBook(int id)
        {
            homeService.DeleteBook(id);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult CreateMagazine()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMagazine(Magazine magazine)
        {
            homeService.AddMagazine(magazine);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowMagazine(int id)
        {
            Magazine magazine = homeService.GetMagazine(id);           
            return View(magazine);
        }

        [HttpGet]
        public ActionResult EditMagazine(int id)
        {
            Magazine magazine = homeService.GetMagazine(id);           
            return View(magazine);
        }

        [HttpPost]
        public ActionResult EditMagazine(int Id, Magazine newMagazine)
        {
            homeService.UpdateMagazine(Id, newMagazine);

            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteMagazine(int? id)
        {
            Magazine magazine = homeService.GetMagazine(id);          
            return PartialView(magazine);
        }

        [HttpPost, ActionName("DeleteMagazine")]
        public ActionResult DeleteConfirmedMagazine(int id)
        {
            homeService.DeleteMagazine(id);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult CreateNewspaper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewspaper(Newspaper newspaper)
        {
            homeService.AddNewspaper(newspaper);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowNewspaper(int id)
        {
            Newspaper newspaper = homeService.GetNewspaper(id);           
            return View(newspaper);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult EditNewspaper(int id)
        {
            Newspaper newspaper = homeService.GetNewspaper(id);           
            return View(newspaper);
        }

        [HttpPost]
        public ActionResult EditNewspaper(int Id, Newspaper newNewspaper)
        {
            homeService.UpdateNewspaper(Id, newNewspaper);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteNewspaper(int? id)
        {
            Newspaper newspaper = homeService.GetNewspaper(id);          
            return PartialView(newspaper);
        }

        [HttpPost, ActionName("DeleteNewspaper")]
        public ActionResult DeleteConfirmedNewspaper(int id)
        {
            homeService.DeleteNewspaper(id);
            return RedirectToAction("Index");
        }
    }
}