using ApiWebFood.Data;
using ApiWebFood.Entities;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Client
{
    [Route("api/[controller]")]
    [Authorize]
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
        [AllowAnonymous]
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
        [Route("Buy")]
        [HttpPost]
        public async Task<IActionResult> BuyProduct()
        {
            return Ok();
        }

        [Route("GetListCart")]
        [HttpGet]
        [AllowAnonymous]
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
            return Ok(listcart);
        }
    }
}
