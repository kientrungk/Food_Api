using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class ProductDiscount
{
    public int Id { get; set; }

    public int? Productid { get; set; }

    public int? CateId { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual Product? Product { get; set; }
}
