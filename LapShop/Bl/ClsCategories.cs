using LapShop.Models;

namespace LapShop.Bl
{
    public interface ICategories
    {
        public List<TbCategory> GetAll();
        public TbCategory GetById(int id);
        public bool Save(TbCategory category);
        public bool Delete(int id);
    }
    public class ClsCategories:ICategories
    {
        LapShopContext ctx = new LapShopContext();
        public ClsCategories(LapShopContext context)
        {
            ctx = context;
        }
        public List<TbCategory> GetAll()
        {
            try
            {
                var lstCategories = ctx.TbCategories.Where(a=>a.CurrentState==1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<TbCategory>();
            }            
        }
        public TbCategory GetById(int id)
        {
            try
            {
                var category = ctx.TbCategories.FirstOrDefault(a => a.CategoryId == id && a.CurrentState==1);
                return category;
            }
            catch
            {
                return new TbCategory();
            }            
        }
        public bool Save(TbCategory category)
        {
            try
            {
                category.ImageName = "";
                if (category.CategoryId == 0)
                {
                    category.CreatedBy = "1";
                    category.CreatedDate = DateTime.Now;
                    ctx.TbCategories.Add(category);
                }
                else
                {
                    category.UpdatedBy = "1";
                    category.UpdatedDate = DateTime.Now;
                    ctx.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var category = GetById(id);
                category.CurrentState = 0;
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
