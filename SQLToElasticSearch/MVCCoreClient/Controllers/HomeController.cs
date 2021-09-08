using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCCoreClient.IServices;
using MVCCoreClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IElasticServiceClient _elasticServiceClient;
        public HomeController(ILogger<HomeController> logger, IElasticServiceClient elasticServiceClient)
        {
            _logger = logger;
            _elasticServiceClient = elasticServiceClient;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public  JsonResult AutoComplete(string term)
        {
            var resultList = _elasticServiceClient.GetAutocompleteCustomers(term).Result;

            return Json(new { data = resultList });

           // return Json(resultList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
