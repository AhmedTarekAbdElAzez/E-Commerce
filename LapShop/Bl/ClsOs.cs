using LapShop.Models;

namespace LapShop.Bl
{
    public interface IOs
    {
        public List<TbO> GetAll();
        public TbO GetById(int id);
        public bool Save(TbO os);
        public bool Delete(int id);
    }
    public class ClsOs:IOs
    {
        LapShopContext ctx = new LapShopContext();
        public ClsOs(LapShopContext context)
        {
            ctx = context;
        }
        public List<TbO> GetAll()
        {
            try
            {
                var lstOs = ctx.TbOs.Where(a=>a.CurrentState==1).ToList();
                return lstOs;
            }
            catch
            {
                return new List<TbO>();
            }            
        }
        public TbO GetById(int id)
        {
            try
            {
                var os = ctx.TbOs.FirstOrDefault(a => a.OsId == id && a.CurrentState==1);
                return os;
            }
            catch
            {
                return new TbO();
            }            
        }
        public bool Save(TbO os)
        {
            try
            {
                os.ImageName = "";
                if (os.OsId == 0)
                {
                    os.CreatedBy = "1";
                    os.CreatedDate = DateTime.Now;
                    ctx.TbOs.Add(os);
                }
                else
                {
                    os.UpdatedBy = "1";
                    os.UpdatedDate = DateTime.Now;
                    ctx.Entry(os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var os = GetById(id);
                os.CurrentState = 0;
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
