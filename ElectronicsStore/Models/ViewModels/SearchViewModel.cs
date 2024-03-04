namespace ElectronicsStore.Models.ViewModels
{
    public class SearchViewModel
    {
        public List<Product>? Products { get; set; }
        public string searchQuery { get; set; } = string.Empty;
    }
}
