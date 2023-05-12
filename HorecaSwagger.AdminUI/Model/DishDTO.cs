using System.ComponentModel.DataAnnotations;

namespace HorecaSwagger.AdminUI.Model;

public class DishDTO
{
    public int DishUUID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    [DataType(DataType.Currency)]
    public double PriceInEUR { get; set; }
    public int AmountAvailable { get; set; }
}
