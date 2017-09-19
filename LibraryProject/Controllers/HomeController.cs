using AutoMapper;
using BusinessLogicLayer.DataTransferObject;
using BusinessLogicLayer.Interfaces;
using Entities.Entities;
using LibraryProject.Configurations;
using LibraryProject.Extention_Classes;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        IBookService bookService;
        IMagazineService magazineService;
        INewspaperService newspaperService;

        public HomeController(IBookService bookservice, IMagazineService magazineservice, INewspaperService newspaperservice)
        {
            bookService = bookservice;
            magazineService = magazineservice;
            newspaperService = newspaperservice;
        }

        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        public ActionResult Index(string bookPublisher = "All", string magazinePublisher = "All", string newspaperPublisher = "All")
        {
            CheckRole();
            IndexModel indexModel = new IndexModel();
            Initialize(indexModel);
            indexModel = CheckPublisher(indexModel, bookPublisher, magazinePublisher, newspaperPublisher);

            return View(indexModel);
        }

        private void CheckRole()
        {
            if (User.IsInRole(ConfigurationData._USER_ROLE))
            {
                ViewBag.hideElement = ConfigurationData._ATTRIBUTES_STATE_OFF;
            }
            if (User.IsInRole(ConfigurationData._ADMIN_ROLE) & User.IsInRole(ConfigurationData._USER_ROLE))
            {
                ViewBag.hideElement = ConfigurationData._ATTRIBUTES_STATE_ON;
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.accountElementState = ConfigurationData._ATTRIBUTES_STATE_OFF;
                ViewBag.logoutLinkElement = ConfigurationData._ATTRIBUTES_STATE_ON;
            }
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.accountElementState = ConfigurationData._ATTRIBUTES_STATE_ON;
                ViewBag.logoutLinkElement = ConfigurationData._ATTRIBUTES_STATE_OFF;
            }
        }

        private IndexModel Initialize(IndexModel model)
        {
            model.BooksFilterModel = new BooksFilterModel();
            model.BooksFilterModel.BooksPublisher = new SelectList(new List<string>() { "All", "O.Reilly", "Syncfusion", "Williams", "Wrox", "ITVDN" });
            model.BooksFilterModel.Books = new List<Book>();

            model.MagazineFilterModel = new MagazineFilterModel();
            model.MagazineFilterModel.MagazinesPublisher = new SelectList(new List<string>() { "All", "Williams", "Mag Group", "Stanley & Co" });
            model.MagazineFilterModel.Magazines = new List<Magazine>();

            model.NewspaperFilterModel = new NewspaperFilterModel();
            model.NewspaperFilterModel.NewspapersPublisher = new SelectList(new List<string>() { "All", "Red Octouber", "Ronald", "West-Cost", "Croxy" });
            model.NewspaperFilterModel.Newspapers = new List<Newspaper>();

            return model;
        }

        public IndexModel CheckPublisher(IndexModel model, string bookPublisher = ConfigurationData._ALL_PUBLISHER, string magazinePublisher = ConfigurationData._ALL_PUBLISHER, string newspaperPublisher = ConfigurationData._ALL_PUBLISHER)
        {
            //List<NewspaperDTO> newspaperDtos = newspaperService.GetNewspapers();
            //Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
            //var newspapers = Mapper.Map<List<NewspaperDTO>, List<Newspaper>>(newspaperDtos);
            //model.NewspaperFilterModel.Newspapers = newspapers;
            //model.BooksFilterModel.Books = bookService.CheckBookPublisher(model.BooksFilterModel.Books, bookPublisher);
            //*****************************************************************************
            if (!String.IsNullOrEmpty(bookPublisher) && !bookPublisher.Equals("All"))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection != null)
                    {
                        string bookSelectExpression = $"SELECT * FROM Books WHERE Publisher = '{bookPublisher}'";
                        SqlCommand command = new SqlCommand(bookSelectExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.BooksFilterModel.Books.Add(new Book { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Author = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                            }
                        }
                    }
                }
            }
            else
            {
                List<BookDTO> bookDtos = bookService.GetBooks();
                Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, Book>());
                var books = Mapper.Map<List<BookDTO>, List<Book>>(bookDtos);
                model.BooksFilterModel.Books = books;
            }
            //***********************************************************************************
            if (!String.IsNullOrEmpty(magazinePublisher) && !magazinePublisher.Equals("All"))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection != null)
                    {
                        string magazineSelectExpression = $"SELECT * FROM Magazines WHERE Publisher = '{magazinePublisher}'";
                        SqlCommand command = new SqlCommand(magazineSelectExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.MagazineFilterModel.Magazines.Add(new Magazine { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                            }
                        }
                    }
                }
            }
            else
            {
                List<MagazineDTO> magazineDtos = magazineService.GetMagazines();
                Mapper.Initialize(cfg => cfg.CreateMap<MagazineDTO, Magazine>());
                var magazines = Mapper.Map<List<MagazineDTO>, List<Magazine>>(magazineDtos);
                model.MagazineFilterModel.Magazines = magazines;
            }
            if (!String.IsNullOrEmpty(newspaperPublisher) && !newspaperPublisher.Equals("All"))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection != null)
                    {
                        string newspaperSelectExpression = $"SELECT * FROM Newspapers WHERE Publisher = '{newspaperPublisher}'";
                        SqlCommand command = new SqlCommand(newspaperSelectExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.NewspaperFilterModel.Newspapers.Add(new Newspaper { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                            }
                        }
                    }
                }
            }
            else
            {
                List<NewspaperDTO> newspaperDtos = newspaperService.GetNewspapers();
                Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
                var newspapers = Mapper.Map<List<NewspaperDTO>, List<Newspaper>>(newspaperDtos);
                model.NewspaperFilterModel.Newspapers = newspapers;
            }
            return model;
        }

        public ActionResult GetBooksList()
        {
            bookService.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksXmlList()
        {
            bookService.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersList()
        {
            newspaperService.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersXmlList()
        {
            newspaperService.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesList()
        {
            magazineService.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesXmlList()
        {
            magazineService.GetXmlList();
            return RedirectToAction("Index");
        }

        [HttpGet]/*****************************************?????Save Db To exist Db?????**************************************************/
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseBookList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.BooksFilterModel.Books;
            bookList.SetBookListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]/*****************************************?????Save Db To exist Db?????**************************************************/
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseMagazineList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> magazineList = indexModel.MagazineFilterModel.Magazines;
            magazineList.SetMagazineListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]/*****************************************?????Save Db To exist Db?????**************************************************/
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseNewspaperList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Newspaper> newspaperList = indexModel.NewspaperFilterModel.Newspapers;
            newspaperList.SetNewspaperListToDb(connectionString);

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
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
            var bookDto = Mapper.Map<Book, BookDTO>(book);
            bookService.AddItem(bookDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowBook(int id)
        {
            BookDTO bookDto = bookService.GetBook(id);
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, Book>());
            var book = Mapper.Map<BookDTO, Book>(bookDto);
            return View(book);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult EditBook(int id)
        {
            BookDTO bookDto = bookService.GetBook(id);
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, Book>());
            var book = Mapper.Map<BookDTO, Book>(bookDto);
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(int Id, Book newBook)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
            var bookDto = Mapper.Map<Book, BookDTO>(newBook);
            bookService.Update(Id, bookDto);

            return RedirectToAction("Index");
        }


        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            BookDTO bookDto = bookService.GetBook(id);
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, Book>());
            var book = Mapper.Map<BookDTO, Book>(bookDto);
            return PartialView(book);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmedBook(int id)
        {
            bookService.Delete(id);
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
            Mapper.Initialize(cfg => cfg.CreateMap<Magazine, MagazineDTO>());
            var magazineDto = Mapper.Map<Magazine, MagazineDTO>(magazine);
            magazineService.AddItem(magazineDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowMagazine(int id)
        {
            MagazineDTO magazineDto = magazineService.GetMagazine(id);
            Mapper.Initialize(cfg => cfg.CreateMap<MagazineDTO, Magazine>());
            var magazine = Mapper.Map<MagazineDTO, Magazine>(magazineDto);
            return View(magazine);
        }

        [HttpGet]
        public ActionResult EditMagazine(int id)
        {
            MagazineDTO magazineDto = magazineService.GetMagazine(id);
            Mapper.Initialize(cfg => cfg.CreateMap<MagazineDTO, Magazine>());
            var magazine = Mapper.Map<MagazineDTO, Magazine>(magazineDto);
            return View(magazine);         
        }

        [HttpPost]
        public ActionResult EditMagazine(int Id, Magazine newMagazine)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Magazine, MagazineDTO>());
            var magazineDto = Mapper.Map<Magazine, MagazineDTO>(newMagazine);
            magazineService.Update(Id, magazineDto);

            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteMagazine(int? id)
        {
            MagazineDTO magazineDTO = magazineService.GetMagazine(id);
            Mapper.Initialize(cfg => cfg.CreateMap<MagazineDTO, Magazine>());
            var magazine = Mapper.Map<MagazineDTO, Magazine>(magazineDTO);
            return PartialView(magazine);
        }

        [HttpPost, ActionName("DeleteMagazine")]
        public ActionResult DeleteConfirmedMagazine(int id)
        {
            magazineService.Delete(id);
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
            Mapper.Initialize(cfg => cfg.CreateMap<Newspaper, NewspaperDTO>());
            var newspaperDto = Mapper.Map<Newspaper, NewspaperDTO>(newspaper);
            newspaperService.AddItem(newspaperDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowNewspaper(int id)
        {
            NewspaperDTO newspaperDto = newspaperService.GetNewspaper(id);
            Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
            var newspaper = Mapper.Map<NewspaperDTO, Newspaper>(newspaperDto);
            return View(newspaper);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult EditNewspaper(int id)
        {
            NewspaperDTO newspaperDto = newspaperService.GetNewspaper(id);
            Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
            var newspaper = Mapper.Map<NewspaperDTO, Newspaper>(newspaperDto);
            return View(newspaper);           
        }

        [HttpPost]
        public ActionResult EditNewspaper(int Id, Newspaper newNewspaper)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Newspaper, NewspaperDTO>());
            var newspaperDto = Mapper.Map<Newspaper, NewspaperDTO>(newNewspaper);
            newspaperService.Update(Id, newspaperDto);

            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteNewspaper(int? id)
        {
            NewspaperDTO newspaperDTO = newspaperService.GetNewspaper(id);
            Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
            var newspaper = Mapper.Map<NewspaperDTO, Newspaper>(newspaperDTO);
            return PartialView(newspaper);
        }

        [HttpPost, ActionName("DeleteNewspaper")]
        public ActionResult DeleteConfirmedNewspaper(int id)
        {
            newspaperService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}