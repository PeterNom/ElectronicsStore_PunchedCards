namespace ElectronicsStore.Models.ViewModels
{
    public class DealsViewModel
    {
        public IEnumerable<Product> dealProducts { get; set; }

        public DealsViewModel(IEnumerable<Product> _dealProducts) 
        {
            dealProducts = _dealProducts;
        }

    }
}
