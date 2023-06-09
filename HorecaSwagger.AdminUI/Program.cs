using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace HorecaSwagger.AdminUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            //culture (set [DataType(DataType.Currency)] to eur)
            var cultureInfo = new CultureInfo("en-US");//var cultureInfo = new CultureInfo("nl-BE");//creates problems => cultureInfo.NumberFormat.CurrencyDecimalSeperator = "."; does not fix read . as decimal seperator
            cultureInfo.NumberFormat.CurrencySymbol = "�";
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = new[] { cultureInfo },
                SupportedUICultures = new[] { cultureInfo },
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}