using ApiWebFood.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly ApiDotNetContext _context;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly ILogger<RoleController> _logger;
        public RoleController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> role, ApiDotNetContext context, ILogger<RoleController> logger)
        {
            _UserManager = usermanager;
            _RoleManager = role;
            _context = context;
            _logger = logger; 
        }

        [Route("gettAllRole")]
        [HttpGet]

        public async Task<IActionResult> GetRole()
        {
            var roles = _RoleManager.Roles.ToList();
            return Ok(roles);
        }

        [Route("addrole")]
        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            // check if role exit 
            var RoleExits = await _RoleManager.RoleExistsAsync(name);


            if (!RoleExits)
            {
                var RoleResult = await _RoleManager.CreateAsync(new IdentityRole(name));

                //we need to check if the role has been added successfully
                if (RoleResult.Succeeded)
                {
                    return Ok(new
                    {
                        result = $"role: {name} successfully"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        result = $"role: {name} not has added"
                    });
                }
            }

            return BadRequest(new { error = " Role already exit" });
        }

        
    }
}
