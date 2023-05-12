namespace HorecaSwagger.API.DTO;

public class DishDTO
{
    public DishDTO()
    {

    }

    public DishDTO(string name, string? description, double priceInEur, int amountAvailable)
    {
        Name = name;
        Description = description;
        PriceInEUR = priceInEur;
        AmountAvailable = amountAvailable;
    }

    public DishDTO(int dishUUID, string name, string? description, double priceInEur, int amountAvailable) : this(name, description, priceInEur, amountAvailable)
    {
        DishUUID = dishUUID;
    }

    public int DishUUID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double PriceInEUR { get; set; }
    public int AmountAvailable { get; set; }
}
