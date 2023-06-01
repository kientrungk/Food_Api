using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? Status { get; set; }

    public virtual User? User { get; set; }
}
