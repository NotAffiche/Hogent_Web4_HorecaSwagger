using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Model;

public class Dish
{
    public Dish(string name, string description, double priceInEUR, int amountAvailable)
    {
        Name = name;
        Description = description;
        PriceInEUR = priceInEUR;
        AmountAvailable = amountAvailable;
    }
    public Dish(int dishUUID, string name, string description, double priceInEUR, int amountAvailable) : this(name, description, priceInEUR, amountAvailable)
    {
        DishUUID = dishUUID;
    }

    public int DishUUID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double PriceInEUR { get; set; }
    public int AmountAvailable { get; set; }
}
