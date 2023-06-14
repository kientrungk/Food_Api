using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;
[Serializable]
public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double? Price { get; set; }

    public int? Idcg { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? IdcgNavigation { get; set; }

    public virtual ICollection<ImgProduct> ImgProducts { get; set; } = new List<ImgProduct>();

    public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
