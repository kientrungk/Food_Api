using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class Cart
{
    public int Id { get; set; }

    public int? Iduser { get; set; }

    public int? Productid { get; set; }

    public int? Quantity { get; set; }

    public virtual User? IduserNavigation { get; set; }

    public virtual Product? Product { get; set; }
}
