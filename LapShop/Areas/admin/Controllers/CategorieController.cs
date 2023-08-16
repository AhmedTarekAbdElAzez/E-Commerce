using LapShop.Bl;
using LapShop.Models;
using LapShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin, DataEntry")]
    public class CategoriesController : Controller
    {
        ICategories oClsCategories;
        public CategoriesController(ICategories category)
        {
            oClsCategories = category;
        }
        [AllowAnonymous]
        public IActionResult List()
        {
            return View(oClsCategories.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            
            var category = new TbCategory();
            if(id!=null)
            {
                category = oClsCategories.GetById(Convert.ToInt32(id));
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category,List<IFormFile>Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", category);
            category.ImageName = await Helper.UploadImage(Files, "Categories");
            oClsCategories.Save(category);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            oClsCategories.Delete(id);
            return RedirectToAction("List");
        }
    }
}
