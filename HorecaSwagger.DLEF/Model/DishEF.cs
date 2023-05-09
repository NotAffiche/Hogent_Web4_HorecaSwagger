using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Model;

public class DishEF
{
    [Key]
    public int DishUUID { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public double PriceInEUR { get; set; }
    [Required]
    public int AmountAvailable { get; set; }
    [Required]
    [Column(TypeName = "bit")]
    public bool Deleted { get; set; }
    public ICollection<OrderEF> Orders { get; set; } = new List<OrderEF>();
}
