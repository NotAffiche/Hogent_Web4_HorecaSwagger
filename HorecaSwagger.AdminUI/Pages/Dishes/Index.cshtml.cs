using HorecaSwagger.AdminUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace HorecaSwagger.AdminUI.Pages.Dishes
{
    public class IndexModel : PageModel
    {
        private readonly string URL = "http://localhost:5135/api/Dishes";
        public IList<DishDTO> Dishes { get; set; }

        public IndexModel()
        {
            Dishes = new List<DishDTO>();
            OnCallAPI();
        }

        public void OnCallAPI()
        {
            try
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(httpClientHandler);
                using (client)
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(URL);
                    request.Method = HttpMethod.Get;
                    HttpResponseMessage response = client.Send(request);
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        Console.WriteLine(jsonContent);
                        Dishes = JsonConvert.DeserializeObject<List<DishDTO>>(jsonContent)!;
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                RedirectToPage("./Error");
            }
        }
    }
}
