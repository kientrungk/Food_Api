using ApiWebFood.Data;
using ApiWebFood.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApiDotNetContext _ContextOrder;
        public OrderController(ApiDotNetContext ContextCommenr)
        {
            _ContextOrder = ContextCommenr;
        }

        [HttpGet]
        public async Task<IActionResult> GetListOrder()
        {
            var listorder = _ContextOrder.Users.Include(item => item.Orders).ToList();
            return Ok(listorder);
        }

        [Route("TotalOder")]
        [HttpGet]
        public async Task<IActionResult> TotalOrder()
        {
            var totaloder = _ContextOrder.Orders.ToList().Count;
            return Ok(totaloder);
        }
        [Route("TotalPrice")]
        [HttpGet]
        public async Task<IActionResult> TotalPrice()
        {
            var totaloder = _ContextOrder.Orders.ToList();
            double totalprice = 0;
            foreach (var item in totaloder)
            {
                totalprice += (double)item.TotalPrice;
            }
            return Ok(totalprice);
        }

        [Route("UpdateStatusOrder")]
        [HttpPut]
        public async Task<IActionResult> UpdateStatus(int Status, int Oderid)
        {
            if (await _ContextOrder.Orders.FirstOrDefaultAsync(i => i.Id == Oderid) is Order found)
            {
                found.Status = Status;
                _ContextOrder.Update(found);
                await _ContextOrder.SaveChangesAsync();
                var messseage = new Mess()
                {
                    messease = "Cập nhập thành công",
                    Statuscode = true
                };
                return Ok(messseage);
            }
            Mess messseage = new Mess()
            {
                messease = "Cập nhập trạng thái không thành công hãy kiểm tra lại thông tin",
                Statuscode = false
            };
            return BadRequest(messseage);
        }
    }
}
