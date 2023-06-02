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
        private readonly ApiDotNetContext _context;

        public ProductController(ApiDotNetContext context)
        {
            _context = context;
        }
        // thêm danh sách category
        [Route("AddNewCategory")]
        [HttpPost]
        public IActionResult CreateCategory(Category cate)
        {
            try
            {
                var Category = _context.Categories.Add(cate);
                if (Category == null)
                {
                    return BadRequest();
                }
                return Ok(Category);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
        [Route("ListCategory")]
        [HttpGet]
        public IActionResult GetlistCate()
        {
            var Category = _context.Categories.ToList();
            return Ok(Category);
        }
        [Route("AddNewProduct")]
        [HttpPost]
        public IActionResult CreatePro(Product pro)
        {
            try
            {
                var Product = _context.Products.Add(pro);
                if (Product == null)
                {
                    return BadRequest();
                }
                _context.SaveChanges();
                return Ok(Product);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
        // lấy danh sách các sản phẩm theo category
        [Route("ListPrCate")]
        [HttpGet]
        public Category GetAllProductWithCategory(int id)
        {
           var catery= _context.Categories.Include(data => data.Products)
                .FirstOrDefault(cate => cate.Id == id);

            if(catery == null)
            {
                return null;
            }

            return catery;
        }
        // xoa 1 sản phẩm ra khỏi danh sách sản phẩm 
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
        // sua 1 san pham
        public IActionResult UpdateProduct(Product pr)
        {
           _context.Products.Update(pr);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
