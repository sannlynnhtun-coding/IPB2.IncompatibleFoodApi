using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB2.IncompatibleFood.Domain.Features.IncompatibleFood;

namespace IPB2.IncompatibleFood.MvcApp.Controllers
{
    public class IncompatibleFoodController : Controller
    {
        private readonly HttpClient _httpClient;

        public IncompatibleFoodController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BackendApi");
        }

        public async Task<IActionResult> IndexAsync()
        {
            IncompatibleFoodListResponse model = new IncompatibleFoodListResponse();
            string endpoint = "api/IncompatibleFood/List?PageNo=1&PageSize=10";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<IncompatibleFoodListResponse>(json)!;
            }
            return View();
        }
    }
}
