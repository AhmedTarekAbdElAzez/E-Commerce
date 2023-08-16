using LapShop.Bl;
using LapShop.Models;
using LapShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin, DataEntry")]
    public class ItemsController : Controller
    {
        IItems oClsItems;
        ICategories oClsCategories;
        IItemTypes oClsItemTypes;
        IOs oClsOs;
        public ItemsController(ICategories cat,IItems it,IItemTypes itType,IOs os)
        {
            oClsItems = it;
            oClsCategories = cat;
            oClsItemTypes = itType;
            oClsOs = os;
        }
        [AllowAnonymous]
        public IActionResult List()
        {
            ViewBag.lstCategories = oClsCategories.GetAll();
            var items = oClsItems.GetAllItemsData(null);
            return View(items);
        }
        public IActionResult Edit(int? id)
        {
            var item = new TbItem();
            ViewBag.lstCategories = oClsCategories.GetAll();
            ViewBag.lstItemTypes = oClsItemTypes.GetAll();
            ViewBag.lstOS = oClsOs.GetAll();
            if (id != null)
            {
                item = oClsItems.GetById(Convert.ToInt32(id));
            }
            return View(item);
        }
        public IActionResult Search(int id)
        {
            ViewBag.lstCategories = oClsItems.GetAll();
            var item = oClsItems.GetAllItemsData(id);
            return View("List",item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            //if (!ModelState.IsValid)
            //    return View("Edit", item);
            item.ImageName = await Helper.UploadImage(Files, "Items");
            oClsItems.Save(item);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            oClsItems.Delete(id);
            return RedirectToAction("List");
        }
    }
}
