using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class discountController : ControllerBase
    {
        private readonly ApiDotNetContext _contextDis;
        public discountController(ApiDotNetContext context)
        {
            _contextDis = context;
        }
        [Route("GetAllDis")]
        [HttpGet]
        public async Task<IActionResult> GetDisAllDiscount()
        {
            var listdis = _contextDis.Discounts.ToList();
            return Ok(listdis);
        }
        [Route("AddNewDis")]
        [HttpPost]
        public async Task<IActionResult> AddNewDiscount(Discount dis)
        {
            if (dis == null)
            {
                return BadRequest("thêm thất bại hãy kiểm tra thông tin");
            }
            if(dis.StartDate > dis.EndDate)
            {
                return BadRequest("thêm thất bại hãy kiểm tra lại thôn tin");
            }
            await _contextDis.Discounts.AddAsync(dis);
            await _contextDis.SaveChangesAsync();
            return Ok("thêm mã giảm giá thành cồng");
        }

        [Route("DeleteDiscount")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = _contextDis.Discounts.Find(id);
            if (discount == null)
            {
                return BadRequest("kiểm tra lại thông tin");
            }
            _contextDis.Discounts.Remove(discount);
            await _contextDis.SaveChangesAsync();
            return Ok("xóa thành công");
        }
    }
}
