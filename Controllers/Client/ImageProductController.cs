using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProductController : ControllerBase
    {
        private readonly ApiDotNetContext _context;
        public ImageProductController(ApiDotNetContext context)
        {
            _context = context;
        }
        // add new image product
        //[Route("imagepr")]
        //[HttpPost]

    }
}
