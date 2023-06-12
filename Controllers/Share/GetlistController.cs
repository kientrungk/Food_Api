using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Share
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetlistController : ControllerBase
    {
        private readonly ApiDotNetContext _context;
        public GetlistController(ApiDotNetContext context)
        {
            _context = context;
        }
        //[Route("GetListProduct")]
        //[HttpGet]
        //public IActionResult GetlistProduct()
        //{
        //    var ListProduct = _context.Products.ToList();
        //    if (ListProduct == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}
    }
}
