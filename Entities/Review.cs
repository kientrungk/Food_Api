using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class Review
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Productid { get; set; }

    public int? Rating { get; set; }

    public string Comment { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
