using HorecaSwagger.AdminUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HorecaSwagger.AdminUI.Pages.Dishes
{
    public class DeleteModel : PageModel
    {
        private readonly string URL = "http://localhost:5135/api/Dishes";
        public DishDTO Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            OnCallApiGet(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            OnCallApiDelete(id);
            return RedirectToPage("./Index");
        }

        public void OnCallApiGet(int id)
        {
            try
            {
                var client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
                using (client)
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri($"{URL}/{id}");
                    request.Method = HttpMethod.Get;
                    HttpResponseMessage response = client.Send(request);
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        Dish = JsonConvert.DeserializeObject<DishDTO>(jsonContent)!;
                    }
                }
            }
            catch
            {
                RedirectToPage("./Error");
            }
        }

        public void OnCallApiDelete(int id)
        {
            try
            {
                var client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
                using (client)
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(URL + "/" + id);
                    request.Method = HttpMethod.Delete;
                    HttpResponseMessage response = client.Send(request);
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Successfully deleted!");
                    }
                }
            }
            catch
            {
                RedirectToPage("./Error");
            }
        }
    }
}
