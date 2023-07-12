using System;
using System.Collections.Generic;

namespace ApiWebFood.Entities;

public partial class Discount
{
    public int Id { get; set; }

    public int? Code { get; set; }

    public int? DiscountAmount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }
}
