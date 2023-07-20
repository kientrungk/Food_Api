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
            var pr = _contextpr.Products.Include(item=> item.ImgProducts).ToList();
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
                if (pr.Carts.Count > 0 && pr.Carts != null)
                {
                    prs.Carts = pr.Carts;
                }
                if (pr.ImgProducts != null && pr.ImgProducts.Count > 0)
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
            var imagepr = _contextpr.ImgProducts.Where(obj => obj.ProductId == id);
            foreach (var item in imagepr)
            {
                _contextpr.ImgProducts.RemoveRange(item);
            }
            string messeage = "";
            if (product == null)
            {
                messeage = "Không có Tìm thấy sản phẩm này";
                return new JsonResult(messeage);
            }
            _contextpr.Products.RemoveRange(product);
            _contextpr.SaveChanges();
            messeage = "xóa sản phẩm thành công";
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
            var product = _contextpr.Products.
                Include(item => item.ImgProducts).
                Include(item=> item.Reviews).
                FirstOrDefault(productid=> productid.Id == id);
            if (product == null)
            {
                return new JsonResult("không tìm thấy sản phẩm này");
            }
            return new JsonResult(product);
        }

        [Route("GetNameCategory")]
        [HttpGet]
        public async Task<IActionResult> GetNameCate(int id)
        {
            var CateName = _contextpr.Categories.FirstOrDefault(name => name.Id == id)?.Name;
            if (CateName == null)
            {
                return new JsonResult("|-|");
            }
            return new JsonResult(CateName);
        }

        [Route("ListImage")]
        [HttpGet]
        public async Task<IActionResult> GetListImage()
        {
            var listimage = _contextpr.ImgProducts.ToList();
            return new JsonResult(listimage);
        }

        [Route("ImageWithid")]
        [HttpGet]
        public async Task<IActionResult> GetImageid(int id)
        {
            var image = _contextpr.ImgProducts.FirstOrDefault(obj => obj.ProductId == id);
            if (image == null) return BadRequest("khong tim thay san pham nay");
            return new JsonResult(image);
        }

        // list image with id
        [Route("listImageWithid")]
        [HttpGet]
        public async Task<IActionResult> GetListImageid(int id)
        {
            var image = _contextpr.ImgProducts.Where(obj => obj.ProductId == id);
            if (image == null) return BadRequest("khong tim thay san pham nay");
            return new JsonResult(image);
        }
    }
}
