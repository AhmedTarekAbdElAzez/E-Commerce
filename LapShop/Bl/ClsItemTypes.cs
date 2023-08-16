using LapShop.Models;

namespace LapShop.Bl
{
    public interface IItemTypes
    {
        public List<TbItemType> GetAll();
        public TbItemType GetById(int id);
        public bool Save(TbItemType itemType);
        public bool Delete(int id);
    }
    public class ClsItemTypes:IItemTypes
    {
        LapShopContext ctx = new LapShopContext();
        public ClsItemTypes(LapShopContext context)
        {
            ctx = context;
        }
        public List<TbItemType> GetAll()
        {
            try
            {
                var lstItemTypes = ctx.TbItemTypes.Where(a=>a.CurrentState==1).ToList();
                return lstItemTypes;
            }
            catch
            {
                return new List<TbItemType>();
            }            
        }
        public TbItemType GetById(int id)
        {
            try
            {
                var itemType = ctx.TbItemTypes.FirstOrDefault(a => a.ItemTypeId == id && a.CurrentState==1);
                return itemType;
            }
            catch
            {
                return new TbItemType();
            }            
        }
        public bool Save(TbItemType itemType)
        {
            try
            {
                itemType.ImageName = "";
                if (itemType.ItemTypeId == 0)
                {
                    itemType.CreatedBy = "1";
                    itemType.CreatedDate = DateTime.Now;
                    ctx.TbItemTypes.Add(itemType);
                }
                else
                {
                    itemType.UpdatedBy = "1";
                    itemType.UpdatedDate = DateTime.Now;
                    ctx.Entry(itemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var itemType = GetById(id);
                itemType.CurrentState = 0;
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
