using Azure.Messaging;
using Kursach.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Kursach.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext db;

        public HomeController(LibraryContext db)
        {
            this.db = db;
        }


        public async Task<IActionResult> Index(string SearchString)
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<Tovary> korzinas = db.Tovarys;

            // Передаем все объекты записи в ViewBag
            ViewBag.Tovarys = korzinas;

            ViewData["CurrentFilter"] = SearchString;

            var books = from b in db.Tovarys
                        select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                books = books.Where(b => b.BookName.Contains(SearchString) || b.Author.Contains(SearchString) || b.Genre.Contains(SearchString));
            }

            return View(books);
        }
        //public ActionResult Index(string searchBy, string search)
        //{
        //    IEnumerable<Tovary> korzinas = db.Tovarys;

        //    // Передаем все объекты записи в ViewBag
        //    ViewBag.Tovarys = korzinas;

        //    if (searchBy == "Bookname")
        //    {
        //        return View(db.Tovarys.Where(x => x.BookName.StartsWith(search) || search == null).ToList());
        //    }
        //    else
        //    {
        //        return View(db.Tovarys.Where(x => x.Genre.StartsWith(search) || search == null).ToList());
        //    }
        //}

        public ActionResult Korzina(int? id)
        {
            Tovary tovar = db.Tovarys.FirstOrDefault(p => p.Id == id);

            Korzina korzina = new Korzina()
            {
                // Если поле Id не определен атрибутом Identity
                //Id = tovar.Id, 

                Genre = tovar.Genre,
                BookName = tovar.BookName,
                Author = tovar.Author,
                Price = tovar.Price,
                Kol = tovar.Kol
            };

            db.Korzinas.Add(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("Index");
        }

        public ActionResult ViewKorzina()
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<Korzina> korzinas = db.Korzinas;

            // Передаем все объекты записи в ViewBag
            ViewBag.Korzinas = korzinas;

            IEnumerable<Customer> customers = db.Customers;

            // Передаем все объекты записи в ViewBag
            ViewBag.Customers = customers;

            // Возвращаем представление
            return View();
        }
        public ActionResult ViewOrder()
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<Korzina> korzinas = db.Korzinas;

            // Передаем все объекты записи в ViewBag
            ViewBag.Korzinas = korzinas;

            // Получаем из БД все записи таблицы Person
            IEnumerable<Order> orders = db.Orders;

            // Передаем все объекты записи в ViewBag
            ViewBag.Orders = orders;

            //IEnumerable<Customer> customers = db.Customers;

            //// Передаем все объекты записи в ViewBag
            //ViewBag.Customers = customers;

            // Возвращаем представление
            return View();
        }

        public ActionResult Order(int? id)
        {
            Korzina tovar = db.Korzinas.FirstOrDefault(p => p.Id == id);


            Korzina2 order = new Korzina2()
            {
                // Если поле Id не определен атрибутом Identity
                //Id = tovar.Id, 
                Genre = tovar.Genre,
                BookName = tovar.BookName,
                Author = tovar.Author,
                Price = tovar.Price,
                Kol = tovar.Kol
            };

            db.Korzinas2.Add(order);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("ViewOrder");
        }

        [HttpPost]
        public IActionResult ViewOrder(Customer customer)
        {
            Korzina korz2 =  db.Korzinas.FirstOrDefault(x=> x.Id == customer.CustomerId);

            Order order = new Order()
            {
                CustomerName = customer.CustomerName,
                Genre = korz2.Genre,
                BookName = korz2.BookName,
                Author = korz2.Author,
                Price = korz2.Price,
                Kol = korz2.Kol,
            };

            //Customer custom = new Customer()
            //{
            //    CustomerName = customer.CustomerName,
            //};

            db.Orders.Add(order);
            db.Korzinas.Remove(korz2);
            //db.Customers.Add(custom);
            db.SaveChanges();
     

            return View("Complete");
        }

        public ActionResult Complete()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        //[Authorize("RequireLogin")]

        public ActionResult Librarian1(int? bookId, int? authorId)
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<Tovary> tovarys = db.Tovarys;

            // Передаем все объекты записи в ViewBag
            ViewBag.Tovarys = tovarys;
            return View();
        }

        public ActionResult Customers()
        {
            IEnumerable<Customer> customers = db.Customers;

            // Передаем все объекты записи в ViewBag
            ViewBag.Customers = customers;
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Формируется список отделов для передачи в представление
            //SelectList deps = new SelectList(db.Deps, "DepId", "DepName");
            //ViewBag.Deps = deps;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tovary tovar)
        {
            //Добавляем служащего в таблицу
            db.Tovarys.Add(tovar);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Librarian1");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Tovary tovar = db.Tovarys.Find(id);
            //SelectList deps = new SelectList(db.Deps, "DepId",
            //                                          "DepName", person.DepId);
            //ViewBag.Deps = deps;
            return View(tovar);
        }

        [HttpPost]
        public ActionResult Edit(Tovary tovar)
        {
            db.Entry(tovar).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Librarian1");
        }

        public ActionResult Showed()
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<Order> orders = db.Orders;

            // Передаем все объекты записи в ViewBag
            ViewBag.Orders = orders;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                //using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password && u.Role == model.Role);
                    
                }
                //if (model.Role == "Админ")
                //{
                //    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password && u.Role == model.Role);
                //    return RedirectToAction("Admin1", "Home");
                //}
                
                //if (model.Role == "Библиотекарь")
                //{
                //    return RedirectToAction("Librarian1", "Home");
                //}
                if (user != null)
                {
                //    //FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Admin1", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
 
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;

                if (user == null)
                {
                    // создаем нового пользователя
                    db.Users.Add(new User { Email = model.Name, Password = model.Password,Role = model.Role, Age = model.Age });
                    db.SaveChanges();

                    user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        //FormsAuthentication.SetAuthCookie(model.Name, true);

                        return RedirectToAction("Login", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Admin1()
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<User> orders = db.Users;

            // Передаем все объекты записи в ViewBag
            ViewBag.Users = orders;
            return View();
        }

        public ActionResult Login1()
        {
           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login1(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                //using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password && u.Role == model.Role);

                }
                //if (model.Role == "Админ")
                //{
                //    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password && u.Role == model.Role);
                //    return RedirectToAction("Admin1", "Home");
                //}

                //if (model.Role == "Библиотекарь")
                //{
                //    return RedirectToAction("Librarian1", "Home");
                //}
                if (user != null)
                {
                    //    //FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Librarian1", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }













        public ActionResult Accept1(int? id)
        {
            Order korzina = db.Orders.FirstOrDefault(p => p.Id == id);

            db.Orders.Remove(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("Librarian1");
        }


        public ActionResult Cancel1(int? id)
        {
            Order korzina = db.Orders.FirstOrDefault(p => p.Id == id);

            db.Orders.Remove(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("ViewOrder");
        }

        public ActionResult Cancel(int? id)
        {
            Korzina korzina = db.Korzinas.FirstOrDefault(p => p.Id == id);

            db.Korzinas.Remove(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("ViewKorzina");
        }

        public ActionResult Cancel3(int? id)
        {
            Korzina2 korzina = db.Korzinas2.FirstOrDefault(p => p.Id == id);

            db.Korzinas2.Remove(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("ViewOrder");
        }

        public ActionResult Cancel4(int? id)
        {
            Tovar tovar = db.Tovars.FirstOrDefault(p => p.Id == id);

            db.Tovars.Remove(tovar);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("ViewOrder");
        }

        public ActionResult Cancel5(int? id)
        {
            Tovar tovar = db.Tovars.FirstOrDefault(p => p.Id == id);

            db.Tovars.Remove(tovar);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("Librarian1");
        }

        public ActionResult Cancel6(int? id)
        {
            User user = db.Users.FirstOrDefault(p => p.Id == id);

            db.Users.Remove(user);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("Admin1");
        }

        public ActionResult BackToTovars()
        {
            // Переход на главную страницу приложения
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}