using HorecaSwagger.BL.Model;
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
            CustomerRepositoryEF crepo = new CustomerRepositoryEF(conn);
            Customer c1 = new Customer("Jer", "Bie", "Ple", 120, "", "SN", 9100, "BE", "00000000", "x@x.x", "dak");
            Dish d1 = new Dish("Beef Burger", "Finest Irish Angus Beef Burger", 7.5, 87);
            Dish d2 = new Dish("Chicken Burger", "The Greatest Poultry From All The Lands On A Delicious Bun", 5.5, 93);
            Order o1 = new Order(DateTime.Now, DateTime.Now, c1, new List<Dish> { d1, d2 });
            //crepo.CreateCustomer(c1);
            var rc1 = crepo.Read(1);
            Console.WriteLine($" READ SINGLE WITH ID 1: {rc1.FirstName} {rc1.Name}");
            foreach (var item in crepo.ReadAll())
            {
                Console.WriteLine($"{item.FirstName} {item.Name}");
            }
            c1.FirstName = "Jeremi";
            //crepo.UpdateCustomer(c1);
            var rc2 = crepo.Read(2);
            Console.WriteLine($" READ SINGLE WITH ID 2: {rc2.FirstName} {rc2.Name}");
            crepo.DeleteCustomer(rc2);
        }
    }
}