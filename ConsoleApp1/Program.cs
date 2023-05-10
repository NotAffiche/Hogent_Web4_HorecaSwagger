using HorecaSwagger.BL.Model;
using HorecaSwagger.BL.Services;
using HorecaSwagger.DLEF;
using HorecaSwagger.DLEF.Repositories;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //=================================----------------=================================//
            string conn = "Server=mysql.affiche.me;Database=web4db_ADBI;Uid=Web4U-ADBI;Pwd=web4P455w0rd!;";
            //=================================----------------=================================//
            /*
            //Gen db
            DataContext ctx = new DataContext(conn);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            */
            Console.WriteLine("====================================================");
            Console.WriteLine("====================================================");
            Console.WriteLine("====================================================");
            //services
            CustomerService cs = new CustomerService(new CustomerRepositoryEF(conn));
            DishService ds = new DishService(new DishRepositoryEF(conn));
            OrderService os = new OrderService(new OrderRepositoryEF(conn));

            //cs.Create(new Customer("Biedny", "Jeremi", "Plezantstraat", 120, null, "Sint-Niklaas", 9100, "BE", "0000/000.000", "jer.bie@gmail.com", "daknam123"));
            //cs.Delete(cs.Read(4));
            //cs.Create(new Customer("Name", "FirstName", "Street", 1, null, "City", 9999, "Country", "0000/000.000", "e@mail.com", "password"));
            //Order o2 = new Order(DateTime.Now, null, cs.Read(3), new List<Dish> { ds.Read(1), ds.Read(1), ds.Read(3) });
            //os.Create(o2);


            foreach (var item in cs.ReadAll())
            {
                Console.WriteLine($"{item.FirstName} | {item.Name}");
            }
            foreach (var item in ds.ReadAll())
            {
                Console.WriteLine($"{item.Name} | {item.Description}");
            }
            foreach (var item in os.ReadAll())
            {
                Console.Write($"{item.OrderUUID} | {item.Customer.FirstName} {item.Customer.Name} | ");
                foreach (Dish d in item.Dishes)
                {
                    Console.Write($"{d.Name} ");
                }
                Console.WriteLine();
            }
        }
    }
}