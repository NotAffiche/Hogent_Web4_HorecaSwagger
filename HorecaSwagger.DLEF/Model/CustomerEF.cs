using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Model;

public class CustomerEF
{
    [Key]
    public int CustomerUUID { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string Street { get; set; }
    public int Nr { get; set; }
    public string? NrAddition { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Required]
    [Column(TypeName = "bit")]
    public bool Deleted { get; set; }
    //
    public ICollection<OrderEF> Orders { get; set; } = new List<OrderEF>();
}
