using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        IItems oItemService;
        IServices Services;
        UserManager<ApplicationUser> userManager;
        public OrderController(IItems service,UserManager<ApplicationUser> _userManager,IServices services)
        {
            oItemService = service;
            userManager = _userManager;
            Services = services;
        }
        public IActionResult Cart()
        {
            string cookieCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                cookieCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<VmShoppingCart>(cookieCart);
            return View(cart);
        }
        public IActionResult AddToCart(int id)
        {
            VmShoppingCart cart;
            
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<VmShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new VmShoppingCart();

            var item = oItemService.GetById(id);

            var itemInList = cart.lstItems.Where(a => a.ItemId == id).FirstOrDefault();
            if (itemInList != null)
            {
                itemInList.Quantity++;
                itemInList.TotalPrice = itemInList.Quantity * itemInList.Price;
            }
            else
            {
                cart.lstItems.Add(new VmShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.SalesPrice,
                    Quantity = 1,
                    TotalPrice = item.SalesPrice
                });
            }
            cart.Total = cart.lstItems.Sum(a => a.TotalPrice);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Cart");
        }

        public IActionResult MyOrders()
        {
            return View();
        }
        [Authorize]
        public IActionResult OrderSuccess()
        {
            return View();
        }

        async Task SaveOrder(VmShoppingCart oVmShoppingCart)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oVmShoppingCart.lstItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.Quantity,
                        InvoicePrice = item.Price
                    });
                }
                var user = await userManager.GetUserAsync(User);
                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                    TbSalesInvoiceItems = lstInvoiceItems
                };
                Services.Save(oSalesInvoice);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }
    }
}
