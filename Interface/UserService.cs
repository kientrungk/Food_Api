using ApiWebFood.Entities;
using System.Security.Claims;

namespace ApiWebFood.Interface
{
    public class UserService : iUserService
    {

        private readonly HttpContextAccessor _context;

        public UserService(HttpContextAccessor context)
        {
            _context = context;
        }

        public string GetName()
        {
            var result = string.Empty;

            if(_context.HttpContext != null)
            {
                result = _context.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
