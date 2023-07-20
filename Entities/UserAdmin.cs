using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class UserAdmin
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? DividePower { get; set; }
}
