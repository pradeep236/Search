using System.Collections.Generic;
using System.Linq;
using Core_MVC_AutoComplete.Models;
using Microsoft.AspNetCore.Mvc;
namespace Core_MVC_AutoComplete.Controllers
{
    public class HomeController : Controller
    {
        //private DBCtx Context { get; }
        //public HomeController(DBCtx _context)
        //{
        //    this.Context = _context;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            //var customers = (from customer in this.Context.Customers
            //                 where customer.ContactName.StartsWith(prefix)
            //                 select new
            //                 {
            //                     label = customer.ContactName,
            //                     val = customer.CustomerID
            //                 }).ToList();

            //return Json(customers);
            return null;
        }

        [HttpPost]
        public ActionResult Index(string CustomerName, string CustomerId)
        {
            ViewBag.Message = "CustomerName: " + CustomerName + " CustomerId: " + CustomerId;
            return View();
        }
    }
}