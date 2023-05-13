using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorecaSwagger.BL.Exceptions;

namespace HorecaSwagger.BL.Model;

public class Dish
{
    private int _dishUUID;
    private string _name;
    private double _priceInEur;
    private int _amountAvailable;

    public Dish(string name, string? description, double priceInEUR, int amountAvailable)
    {
        Name = name;
        Description = description;
        PriceInEUR = priceInEUR;
        AmountAvailable = amountAvailable;
    }
    public Dish(int dishUUID, string name, string? description, double priceInEUR, int amountAvailable) : this(name, description, priceInEUR, amountAvailable)
    {
        DishUUID = dishUUID;
    }

    public int DishUUID
    {
        get { return _dishUUID; }
        set
        {
            if (value < 0) throw new DomainException("Invalid DishUUID", new ArgumentException());
            _dishUUID = value;
        }
    }
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Name", new ArgumentException());
            _name = value;
        }
    }
    public string? Description { get; set; }
    public double PriceInEUR
    {
        get { return _priceInEur; }
        set
        {
            if (value <= 0) throw new DomainException("Invalid PriceInEur", new ArgumentException());
            _priceInEur = value;
        }
    }
    public int AmountAvailable
    {
        get { return _amountAvailable; }
        set
        {
            if (value < 0) throw new DomainException("Invalid AmountAvailable", new ArgumentException());
            _amountAvailable = value;
        }
    }
}
