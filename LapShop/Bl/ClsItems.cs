using Azure.Identity;
using LapShop.Models;

namespace LapShop.Bl
{
    public interface IItems
    {
        public List<TbItem> GetAll();
        public List<VwItem> GetAllItemsData(int? categoryId);
        public List<VwItem> GetRecommendedItemsData(int itemId);
        public TbItem GetById(int id);
        public VwItem GetItemById(int id);
        public bool Save(TbItem item);
        public bool Delete(int id);
    }
    public class ClsItems:IItems
    {
        LapShopContext ctx;
        public ClsItems(LapShopContext context)
        {
            ctx = context;
        }
        public List<TbItem> GetAll()
        {
            try
            {
                var lstItems = ctx.TbItems.Where(a=>a.CurrentState==1).ToList();
                return lstItems;
            }
            catch
            {
                return new List<TbItem>();
            }            
        }
        public List<VwItem> GetAllItemsData(int? categoryId)
        {
            try
            {
                var lstItems = ctx.VwItems.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0)&&a.CurrentState==1).ToList();
                return lstItems;
            }
            catch
            {
                return new List<VwItem>();
            }            
        }
        public TbItem GetById(int id)
        {
            try
            {
                var item = ctx.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState==1);
                return item;
            }
            catch
            {
                return new TbItem();
            }            
        }
        public bool Save(TbItem item)
        {
            try
            {
                item.ImageName = "";
                if (item.ItemId == 0)
                {
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                    ctx.TbItems.Add(item);
                }
                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                    ctx.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool Delete(int id)
        {
            try
            {
                var item = GetById(id);
                item.CurrentState = 0;
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public VwItem GetItemById(int id)
        {
            try
            {
                var item = ctx.VwItems.FirstOrDefault(a => a.ItemId == id);
                return item;
            }
            catch
            {
                return new VwItem();
            }
        }

        public List<VwItem> GetRecommendedItemsData(int id)
        {
            try
            {
                var item = GetById(id);
                var lstItems = ctx.VwItems.Where(a => a.SalesPrice > item.SalesPrice - 500 && a.SalesPrice < item.SalesPrice + 500).Take(20).ToList();
                return lstItems;
            }
            catch
            {
                return new List<VwItem>();
            }
        }
    }
}
