using System.ComponentModel.DataAnnotations;

namespace ApiWebFood.Data
{
    public class UserRegister
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
