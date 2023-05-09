
using HorecaSwagger.DLEF;
using Microsoft.EntityFrameworkCore;

namespace HorecaSwagger.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //string conn = builder.Configuration.GetConnectionString("MySqlConn");//"Server=mysql.affiche.me;Database=web4db_ADBI;Uid=Web4U-ADBI;Pwd=web4P455w0rd!;"
            //var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            //builder.Services.AddDbContext<HorecaSwaggerContext>(
            //    dbCTXopt => dbCTXopt
            //    .UseMySql(conn, serverVersion)
            //    .LogTo(Console.WriteLine, LogLevel.Information)
            //    );


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}