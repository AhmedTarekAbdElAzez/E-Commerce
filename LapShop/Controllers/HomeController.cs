using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IItems oClsItems;
        
        public HomeController(ILogger<HomeController> logger, IItems item)
        {
            _logger = logger;
            oClsItems = item;
        }

        public IActionResult Index()
        {
            VmHomePage vm = new VmHomePage();
            vm.lstAllItems = oClsItems.GetAllItemsData(null).Take(30).ToList();
            vm.lstRecommendedItems = oClsItems.GetAllItemsData(null).Skip(30).Take(8).ToList();
            vm.lstFreeDeliveryItems = oClsItems.GetAllItemsData(null).Skip(38).Take(4).ToList();
            vm.lstNewItems = oClsItems.GetAllItemsData(null).Skip(50).Take(5).ToList();
            return View(vm);
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