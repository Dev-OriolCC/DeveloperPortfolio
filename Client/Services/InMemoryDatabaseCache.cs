using Client.Static;
using Shared.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    internal sealed class InMemoryDatabaseCache
    {
        private readonly HttpClient _httpClient;

        public InMemoryDatabaseCache(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private List<Category> _categories = null;
        internal List<Category> categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                NotifyCateforiesDataChanged();
            }
        }

        private void NotifyCateforiesDataChanged()
        {
            OnCategoriesDataChanged?.Invoke();
        }
        // Propertie to track categories
        private bool _gettingCategoriesFromDatabaseAndCaching = false;
        internal async Task GetCategoriesFromDatabaseAndCache()
        {
            // Only 1 request at the time [Anti-Bots]
            if (_gettingCategoriesFromDatabaseAndCaching == false)
            {
                _gettingCategoriesFromDatabaseAndCaching = true;
                _categories = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categories);
                _gettingCategoriesFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnCategoriesDataChanged;
        private void NotifyCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();
        
    }
}
