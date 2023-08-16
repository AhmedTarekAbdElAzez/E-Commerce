using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {
        IItems oIItem;
        public ItemsController(IItems item)
        {
            oIItem = item;
        }
        public IActionResult Details(int id)
        {
            var item = oIItem.GetItemById(id);
            VmItemDetails vm = new VmItemDetails();
            vm.Item = item;
            vm.lstRecommendedItems = oIItem.GetRecommendedItemsData(id);
            return View(vm);
        }
    }
}
