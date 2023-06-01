using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
