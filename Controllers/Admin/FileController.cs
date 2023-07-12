using ApiWebFood.Data;
using ApiWebFood.Entities;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly ApiDotNetContext _contextim;
        private Dictionary<int?, ImgProduct> Productsimg = new Dictionary<int?, ImgProduct>();
        public FileController(ApiDotNetContext context)
        {
            _contextim = context;
        }
        [Route("ImgProduct")]
        [HttpPost]
        public async Task<IActionResult> UpLoadFileImage([FromForm] PostBlogmodel model)
        {

            if (model.LstFile == null || model.LstFile.Count == 0)
            {
                return BadRequest("vui lòng gửi file đính kèm");
            }
            foreach (var image in model.LstFile)
            {
                var path = "D:\\angular\\ProjectApi\\src\\assets\\image";
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(image.FileName);
                var upload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
                ImgProduct Image = new()
                {
                    ProductId = model.ProductId,
                    ImgLink = "/assets/image/" + fileName,
                    Product = null,
                };
                if (Image == null) return BadRequest("them anh that bai");
                await _contextim.ImgProducts.AddAsync(Image);
                image.CopyTo(new FileStream(upload, FileMode.Create));
            }
            await _contextim.SaveChangesAsync();
            return Ok("them thanh cong");
        }
        [Route("getimage")]
        [HttpGet]
        public async Task<IActionResult> GetImageWithID()
        {
            Productsimg.Clear();
            var images = _contextim.ImgProducts.ToList();
            foreach (var item in images)
            {
                Productsimg.TryAdd(item.ProductId,item);
            }

            return new JsonResult(Productsimg.Values);
        }
    }
}
