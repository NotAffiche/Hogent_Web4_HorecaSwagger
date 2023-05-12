
using HorecaSwagger.DLEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;
using System.Threading.RateLimiting;

namespace HorecaSwagger.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:MySqlConn");

        builder.Services
            .AddHealthChecks()
            .AddCheck("mysql", () =>
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        return HealthCheckResult.Unhealthy();
                    }
                }
                return HealthCheckResult.Healthy();
            });

        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();


        app.UseRateLimiter();
        app.UseHealthChecks("/working");

        app.UseCors(builder => {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}