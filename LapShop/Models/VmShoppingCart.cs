namespace LapShop.Models
{
    public class VmShoppingCart
    {
        public VmShoppingCart()
        {
            lstItems = new List<VmShoppingCartItem>();
        }
        public List<VmShoppingCartItem> lstItems { get; set; }
        public decimal Total { get; set; }
        public string PromoCode { get; set; }
    }
}
