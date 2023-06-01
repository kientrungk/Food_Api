using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class ImgProduct
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? ImgLink { get; set; }

    public virtual Product? Product { get; set; }
}
