using HorecaSwagger.BL.Model;
using HorecaSwagger.BL.Services;
using HorecaSwagger.DLEF;
using HorecaSwagger.DLEF.Repositories;
using System.Collections.Generic;

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
            
            //Gen db
            DataContext ctx = new DataContext(conn);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            
            Console.WriteLine("====================================================");
            Console.WriteLine("====================================================");
            Console.WriteLine("====================================================");
            //services
            CustomerService cs = new CustomerService(new CustomerRepositoryEF(conn));
            DishService ds = new DishService(new DishRepositoryEF(conn));
            OrderService os = new OrderService(new OrderRepositoryEF(conn));

            ////create data
            cs.Create(new Customer("Biedny", "Adrian", "Damstraat", 86, "B", "Sint-Niklaas", 9100, "BE", "0487/000.000", "adrian.biedny@gmail.com", "koekje"));
            //cs.Create(new Customer("Biedny", "Jeremi", "Plezantstraat", 120, null, "Sint-Niklaas", 9100, "BE", "0000/000.000", "jer.bie@gmail.com", "daknam123"));
            cs.Create(new Customer("Name", "FirstName", "Street", 1, null, "City", 9999, "Country", "0000/000.000", "e@mail.com", "password"));
            ds.Create(new Dish("Beef Burger", "Finest Irish Angus Beef Burger", 7.5, 87));
            ds.Create(new Dish("Chicken Burger", "The Greatest Poultry From All The Lands On A Delicious Bun", 5.5, 69));
            ds.Create(new Dish("Margherita Pizza", "The Base For All Pizza's", 12, 64));

            ////create order
            //List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
            //dishesWithAmount.Add(new Dictionary<Dish, int> { { ds.Read(1), 1 } });//beef burger
            //dishesWithAmount.Add(new Dictionary<Dish, int> { { ds.Read(2), 2 } });//chicken burger
            //Order o2 = new Order(DateTime.Now, null, cs.Read(2), dishesWithAmount);
            //os.Create(o2);

            //List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
            //dishesWithAmount.Add(new Dictionary<Dish, int> { { ds.Read(3), 6 } });//6 pizza
            //Order o = new Order(DateTime.Now, null, cs.Read(1), dishesWithAmount);
            //os.Create(o);
            //update order
            //List < Dictionary<Dish, int> > dishesWithAmount = new List<Dictionary<Dish, int>>();
            //dishesWithAmount.Add(new Dictionary<Dish, int> { { ds.Read(3), 4 } });//4 pizza
            //Order o = new Order(2, os.Read(2).CreateDate, DateTime.Now, cs.Read(1), dishesWithAmount);
            //os.Update(o);
            //delete order
            //List < Dictionary<Dish, int> > dishesWithAmount = new List<Dictionary<Dish, int>>();
            //dishesWithAmount.Add(new Dictionary<Dish, int> { { ds.Read(3), 4 } });//4 pizza
            //Order o = new Order(2, os.Read(2).CreateDate, DateTime.Now, cs.Read(1), dishesWithAmount);
            //os.Delete(o);


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
                foreach (Dictionary<Dish, int> d in item.DishesWithAmount)
                {
                    foreach (var a in d)
                    {
                        Console.Write($"{a.Key.Name}: {a.Value}x ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("====================================================");
        }
    }
}