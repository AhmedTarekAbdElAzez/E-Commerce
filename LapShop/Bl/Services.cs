using LapShop.Models;

namespace LapShop.Bl
{
    public interface IServices
    {
        public List<TbSalesInvoice> GetAll();
        public TbSalesInvoice GetById(int id);
        public bool Save(TbSalesInvoice category);
        public bool Delete(int id);
    }
    public class Services : IServices
    {
        LapShopContext ctx = new LapShopContext();
        public Services(LapShopContext context)
        {
            ctx = context;
        }
        public List<TbSalesInvoice> GetAll()
        {
            try
            {
                var lstItems = ctx.TbSalesInvoices.ToList();
                return lstItems;
            }
            catch
            {
                return new List<TbSalesInvoice>();
            }            
        }
        public TbSalesInvoice GetById(int id)
        {
            try
            {
                var category = ctx.TbSalesInvoices.FirstOrDefault();
                return category;
            }
            catch
            {
                return new TbSalesInvoice();
            }            
        }
        public bool Save(TbSalesInvoice category)
        {
            try
            {
                if (category.InvoiceId == 0)
                {
                    category.CreatedBy = "1";
                    category.CreatedDate = DateTime.Now;
                    ctx.TbSalesInvoices.Add(category);
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
