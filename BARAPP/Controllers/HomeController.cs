using System.Diagnostics;
using BARAPP.DataAccess;
using BARAPP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BARAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Credentials creds)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (datalayer.validateCredentials(creds) == false)
                {
                    ModelState.AddModelError("Password", "You have entered invalid userid or password.");
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("UserID", creds.UserID);
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    return RedirectToAction("SetShop");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Login failed with exception : " + ex.Message);
                return View("error");

            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
      
        public IActionResult SetShop()
        {
            try
            {
                if(HttpContext.Session.GetString("UserID")==null)
                {
                    return RedirectToAction("Login");
                }
                List<Shop> ShopsList = datalayer.getShops();
                return View(ShopsList);
            }
            catch(Exception ex)
            {
                _logger.LogError("Set Shop operation failed with exception : " + ex.Message);
                return View("error");
            }
        }

        public IActionResult SetCurrentShop(string ShopName)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login");
            }
            HttpContext.Session.SetString("ShopName", ShopName);
            return RedirectToAction("HomePage");
        }

        public IActionResult HomePage()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddShop()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddShop(Shop shop)
        {
            try
            {
                if (HttpContext.Session.GetString("UserID") == null)
                {
                    return RedirectToAction("Login");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }
                if(datalayer.ShopExists(shop))
                {
                    ModelState.AddModelError("Description", "This Shop Already Exists");
                    return View();
                }
                else if(datalayer.AddShop(shop))
                {
                    return RedirectToAction("SetShop");
                }
                else
                {
                    return View();
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError("Add Shop operation failed with exception : " + ex.Message);
                return View("error");
            }
        }


        public IActionResult Main()
        {
            return View();
        }

        public IActionResult AddCategory()
        {
            try
            {
                if (HttpContext.Session.GetString("UserID") == null)
                {
                    return RedirectToAction("Login");
                }
                List<Category> list = datalayer.getCategories();
                return View(list);
            }
            catch(Exception ex)
            {
                _logger.LogError("Add Category operation failed with exception : " + ex.Message);
                return View("error");
            }
        }

        public IActionResult AddBrand()
        {
            try
            {
                if (HttpContext.Session.GetString("UserID") == null)
                {
                    return RedirectToAction("Login");
                }

                List<Brand> list = datalayer.getBrands();
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Add Brand operation failed with exception : " + ex.Message);
                return View("error");
            }
        }

        [HttpGet]
        public IActionResult AddNewCategory()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(Category category)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            else if(datalayer.CategoryExists(category))
            {
                ModelState.AddModelError("Description", "Category already exists");
                return View();
            }
            else
            {
                datalayer.AddCategory(category);
                return RedirectToAction("AddCategory");
            }
        }

     
        public IActionResult AddUser()
        {
            try
            {
                if (HttpContext.Session.GetString("UserID") == null)
                {
                    return RedirectToAction("Login");
                }
                List<Credentials> list = datalayer.getUsers();
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Add User operation failed with exception : " + ex.Message);
                return View("error");
            }
        }


        [HttpGet]
        public IActionResult AddNewBrand()
        {
            ViewBag.Categories = new SelectList(datalayer.getCategories(), "categoryname", "categoryname");

            var model = new Brand
            {
                ModifiedDate=DateTime.Now,
                OpenStockQ = 0,
                CostPerCaseQ = 0,
                CostPerBottleQ = 0,
                SalePriceQ = 0,

                OpenStockN = 0,
                CostPerCaseN = 0,
                CostPerBottleN = 0,
                SalePriceN = 0,

                OpenStockP = 0,
                CostPerCaseP = 0,
                CostPerBottleP = 0,
                SalePriceP = 0,

                OpenStockD = 0,
                CostPerCaseD = 0,
                CostPerBottleD = 0,
                SalePriceD = 0,

                OpenStockL = 0,
                CostPerCaseL = 0,
                CostPerBottleL = 0,
                SalePriceL = 0,                
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddNewBrand(Brand brand)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }            
            if(datalayer.BrandExists(brand.BrandName))
            {
                ModelState.AddModelError("BrandName", "Brand Already Exists");
                return View();
            }
            else
            {
                datalayer.AddBrand(brand);
                return RedirectToAction("AddBrand");
            }
            
        }
        public IActionResult EditBrand(string BrandName)
        {

            try
            {
                if (HttpContext.Session.GetString("UserID") == null)
                {
                    return RedirectToAction("Login");
                }
                Brand brand = datalayer.getBrandDetails(BrandName);
                return View(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError("Edit  Brand operation failed with exception : " + ex.Message);
                return View("error");
            }
        }

        [HttpPost]
        public IActionResult EditBrand(Brand brand)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            else
            {
                datalayer.updateBrand(brand);
                return RedirectToAction("AddBrand");
            }
        }

        public IActionResult DeleteBrand(string BrandName)
        {
            datalayer.deleteBrand(BrandName);
            return RedirectToAction("AddBrand");
        }

        public IActionResult CreateUser()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateUser(Credentials creds)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (datalayer.UserExists(creds.UserID))
            {
                ModelState.AddModelError("UserID", "User ID Already Exists");
                return View();
            }
            else
            {
                datalayer.AddUser(creds);
                return RedirectToAction("AddUser");
            }
        }

        public IActionResult EditUser(string UserID)
        {
            Credentials user = datalayer.getUser(UserID);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(Credentials creds)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
             datalayer.saveUser(creds);
            return RedirectToAction("AddUser");
        }


        public IActionResult DeleteUser(string UserID)
        {
            datalayer.deleteUser(UserID);
            return RedirectToAction("AddUser");

        }
    }
}
