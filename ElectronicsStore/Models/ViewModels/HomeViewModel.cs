namespace ElectronicsStore.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> DealProducts { get; set; }

        public HomeViewModel(IEnumerable<Product> dealProducts) 
        {
            DealProducts = dealProducts;
        }
    }
}
