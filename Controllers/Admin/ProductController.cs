using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // add mới 1 sản phẩm vào trong
        private readonly ApiDotNetContext _contextpr;

        public ProductController(ApiDotNetContext context)
        {
            _contextpr = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPr()
        {
            var pr = _contextpr.Products.ToList();
            return Ok(pr);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewPr(Product pr)
        {
            try
            {
                Product prs = new()
                {
                    Name = pr.Name,
                    Description = pr.Description,
                    Price = pr.Price,
                    Idcg = pr.Idcg,
                    Carts = null,
                    ImgProducts = null,
                    ProductDiscounts = null,
                    Reviews = null,
                };
                if(pr.Carts.Count > 0 && pr.Carts != null)
                {
                    prs.Carts = pr.Carts;
                }
                if(pr.ImgProducts != null && pr.ImgProducts.Count > 0)
                {
                    prs.ImgProducts = pr.ImgProducts;
                }
                if (pr.ProductDiscounts != null && pr.ProductDiscounts.Count > 0)
                {
                    prs.ProductDiscounts = pr.ProductDiscounts;
                }
                if (pr.Reviews != null && pr.Reviews.Count > 0)
                {
                    prs.Reviews = pr.Reviews;
                }
                await _contextpr.Products.AddAsync(prs);
                _contextpr.SaveChanges();
                return Ok(prs);
                return Ok(pr);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePr(Product pr)
        {
            if (pr != null)
            {
                _contextpr.Products.Update(pr);
            }
            _contextpr.SaveChanges();
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> UpdatePr(int id) 
        {
            var product = _contextpr.Products.Find(id);
            if (product != null)
            {
                _contextpr.Products.Remove(product);
                return NoContent();
            }
            return BadRequest();
        }
        [Route("Getdetail")]
        [HttpGet]
        public async Task<JsonResult> GetDetailProduct(int id)
        {
            var product = _contextpr.Products.FindAsync(id);
            if (product == null)
            {
                return new JsonResult("không tìm thấy sản phẩm này");
            }
            return new JsonResult(product);
        }

    }
}
