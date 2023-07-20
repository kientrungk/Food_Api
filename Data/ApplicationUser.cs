using Microsoft.AspNetCore.Identity;

namespace ApiWebFood.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int? id{ get; set; }
        public string? Name { get; set; }
        public string? eamil { get; set; }
    }
}
