namespace LapShop.Models
{
    public class VmItemDetails
    {
        public VmItemDetails()
        {
            Item = new VwItem();
            lstRecommendedItems = new List<VwItem>();
        }
        public VwItem Item { get; set; }
        public List<VwItem> lstRecommendedItems { get; set; }
    }
}
