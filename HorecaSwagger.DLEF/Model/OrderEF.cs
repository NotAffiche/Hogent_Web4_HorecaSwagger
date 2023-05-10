using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Model;

public class OrderEF
{
    [Key]
    public int OrderUUID { get; set; }
    [Required]
    public DateTime CreateDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    [Required]
    [Column(TypeName = "bit")]
    public bool Deleted { get; set; }
    //
    public int CustomerUUID { get; set; }
    public CustomerEF Customer { get; set; }
    public ICollection<DishEF> Dishes { get; set; } = new List<DishEF>();
}
