using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Model;

public class OrderDetailsEF
{
    [Key]
    public int UUID { get; set; }
}
