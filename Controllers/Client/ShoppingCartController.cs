using ApiWebFood.Data;
using ApiWebFood.Entities;
using AutoMapper;
using AutoMapper.Internal;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace ApiWebFood.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApiDotNetContext _ShoppingContext;
        //private readonly ILogger<ShoppingCartController> _logger;
        public ShoppingCartController(ApiDotNetContext shoppingContext)
        {
            _ShoppingContext = shoppingContext;
            //_logger = logger;
        }
        [Route("AddPr")]
        [HttpPost]
        public async Task<IActionResult> Addpr(int id)
        {
            if (ProductModel.ListCartUser != null)
            {
                var pr = ProductModel.ListCartUser.Keys.FirstOrDefault(value => value.ProductId == id);
                if (pr != null)
                {
                    foreach (KeyValuePair<ShppingCartModelcs, int> item in ProductModel.ListCartUser)
                    {
                        if (item.Key.ProductId == id)
                        {
                            ProductModel.ListCartUser[item.Key] += 1;
                        }
                    }
                }
                else
                {
                    ShppingCartModelcs model = new ShppingCartModelcs();
                    model.ProductId = id;
                    Product prsnew = _ShoppingContext.Products.Find(id);
                    model.Product = prsnew;
                    ProductModel.ListCartUser.Add(model, 1);
                }
            }
            else
            {
                ProductModel.ListCartUser = new Dictionary<ShppingCartModelcs, int>();
                ShppingCartModelcs model = new ShppingCartModelcs();
                model.ProductId = id;
                Product prsnew = _ShoppingContext.Products.Find(id);
                model.Product = prsnew;
                ProductModel.ListCartUser.Add(model,1);
            }

            List<CartResModel> listcart = new List<CartResModel>();

            foreach (KeyValuePair<ShppingCartModelcs, int> item in ProductModel.ListCartUser)
            {
                CartResModel cart = new CartResModel();
                cart.quantity = item.Value;
                cart.data = item.Key;
                listcart.TryAdd(cart);
            }

            return Ok(listcart);
        }
        [Route("GetListCart")]
        [HttpGet]
        public async Task<IActionResult> GetListCart()
        {
            if (ProductModel.ListCartUser == null)
            {
                ProductModel.ListCartUser = new Dictionary<ShppingCartModelcs, int>();
            }

            List<CartResModel> listcart = new List<CartResModel>();

            foreach (KeyValuePair<ShppingCartModelcs, int> item in ProductModel.ListCartUser)
            {
                CartResModel cart = new CartResModel();
                cart.quantity = item.Value;
                cart.data = item.Key;
                listcart.TryAdd(cart);
            }
            return Ok(new
            {
                data = listcart,
                quanlity = listcart.Count
            }); ;
        }

        [Route("DecreaseIncard")]
        [HttpPost]
        public async Task<IActionResult> DeleteWithid(int id)
        {
            foreach (KeyValuePair<ShppingCartModelcs, int> item in ProductModel.ListCartUser)
            {
                if(item.Key.Product.Id == id)
                {
                    if (item.Value > 1)
                    {
                        ProductModel.ListCartUser[item.Key] -=1;
                    }
                    else
                    {
                        ProductModel.ListCartUser.Remove(item.Key);
                    }
                }
            }
            List<CartResModel> listcart = new List<CartResModel>();
            foreach (KeyValuePair<ShppingCartModelcs, int> item in ProductModel.ListCartUser)
            {
                CartResModel cart = new CartResModel();
                cart.quantity = item.Value;
                cart.data = item.Key;
                listcart.TryAdd(cart);
            }
            return Ok(listcart);
        }
        [Route("AddNewOrder")]
        [HttpPost]
        public async Task<IActionResult> SavePr(int userid)
        {
            float totalPrice = 0f;
            List<CartResModel> listcart = new List<CartResModel>();
            foreach (KeyValuePair<ShppingCartModelcs, int> item in ProductModel.ListCartUser)
            {
                CartResModel carts = new CartResModel();
                carts.quantity = item.Value;
                carts.data = item.Key;
                listcart.TryAdd(carts);
                totalPrice += item.Value*(int)item.Key.Product.Price;
                Cart cart = new Cart();
                cart.Productid = item.Key.Product.Id;
                cart.Iduser = userid;
                cart.Quantity = item.Value;
                await _ShoppingContext.Carts.AddAsync(cart);
                 _ShoppingContext.SaveChanges();
            }
            ProductModel.ListCartUser.Clear();
            Order newoder = new Order();
            newoder.Orderdate = DateTime.Now;
            newoder.Userid = userid;
            newoder.TotalPrice = totalPrice;
            newoder.Status = 1; // mắc đinh là khi mua xong ở trong danh sách đang chờ
            await _ShoppingContext.AddAsync(newoder);
            _ShoppingContext.SaveChanges();
              
            return Ok(new
            {
                data = $"mua sản phẩm thành công: totol {totalPrice}"
            });
        }

        [Route("SendEmail")]
        [HttpPost]
        public async Task<IActionResult> SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ben.grimes50@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("ben.grimes50@Sethereal.email"));
            email.Subject = "test send eamil";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("ben.grimes50@ethereal.email", "PWCuN7pY4AdsZ1bGbg");
                smtp.Send(email);
                smtp.Disconnect(true);
                var message = new Mess()
                {
                    messease = "Xác nhận mua hàng",
                    Statuscode = true
                };
                return Ok(message);
            }
        }
    }
}
