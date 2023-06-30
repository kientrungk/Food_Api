using ApiWebFood.Data;
using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
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
                if (pr.Price <= 0)
                {
                    return new JsonResult("KIểm tra lại thông số");
                }
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
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
        [Route("GetListCart")]
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            if (ProductModel.ListCartUser != null)
            {
                return new JsonResult(ProductModel.ListCartUser);
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Deleteincart(int id)
        {
            var product = _contextpr.Products.Find(id);
            string messeage = "";
            if (product == null)
            {
                messeage = "Không có Tìm thấy sản phẩm này";
                return new JsonResult(messeage);
            }
            _contextpr.Products.RemoveRange(product);
            _contextpr.SaveChanges();
            messeage="xóa sản phẩm thành công";
            return new JsonResult(messeage);
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdatePr(int id) 
        {
            var product = _contextpr.Products.Find(id);
            if (product != null)
            {
                _contextpr.Products.Update(product);
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
