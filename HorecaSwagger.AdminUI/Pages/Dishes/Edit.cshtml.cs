using HorecaSwagger.AdminUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HorecaSwagger.AdminUI.Pages.Dishes
{
    public class EditModel : PageModel
    {
        private readonly string URL = "http://localhost:5135/api/Dishes";
        public DishDTO Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            OnCallApiGet(id);
            return Page();
        }

        public IActionResult OnPost(int id, DishDTO dish)
        {
            OnCallApiEdit(id, dish);
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
                    request.RequestUri = new Uri(URL + "/" + id);
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

        public void OnCallApiEdit(int id, DishDTO dish)
        {
            try
            {
                var client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
                using (client)
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri($"{URL}/{id}");
                    request.Method = HttpMethod.Put;


                    var jsonPayload = JsonConvert.SerializeObject(dish);
                    request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.Send(request);
                    Console.WriteLine($"Request URL: {request.RequestUri}");
                    Console.WriteLine($"Request Method: {request.Method}");
                    Console.WriteLine($"Request Headers: {request.Headers}");
                    Console.WriteLine($"Request Payload: {jsonPayload}");
                    Console.WriteLine($"Request Status Code: {response.StatusCode}");
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Successfully edited!");
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
