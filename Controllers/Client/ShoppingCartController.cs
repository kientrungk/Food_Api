using ApiWebFood.Data;
using ApiWebFood.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApiDotNetContext _ShoppingContext;
        private readonly ILogger<ShoppingCartController> _logger;
        public ShoppingCartController(ApiDotNetContext shoppingContext, ILogger<ShoppingCartController> logger)
        {
            _ShoppingContext = shoppingContext;
            _logger = logger;
        }
        [Route("AddPr")]
        [HttpPost]
        public async Task<IActionResult> Addpr(int id)
        {
            if (ProductModel.ListCartUser!=null)
            {
                var pr = ProductModel.ListCartUser.FirstOrDefault(value => value.ProductId == id);
                if (pr != null)
                {
                    foreach (var item in ProductModel.ListCartUser)
                    {
                        if (item.ProductId == id)
                        {
                            item.Quantity++;
                        }
                    }
                }
                else
                {
                    ShppingCartModelcs model = new ShppingCartModelcs();
                    model.ProductId = id;
                    Product prsnew = _ShoppingContext.Products.Find(id);
                    model.Product = prsnew;
                    ProductModel.ListCartUser.Add(model);
                }
            }
            else
            {
                ProductModel.ListCartUser = new List<ShppingCartModelcs>();
                ShppingCartModelcs model = new ShppingCartModelcs();
                model.ProductId = id;
                Product prsnew = _ShoppingContext.Products.Find(id);
                model.Product = prsnew;
                model.Quantity++;
                ProductModel.ListCartUser.Add(model);
            }
            return Ok(ProductModel.ListCartUser);
        }
        [Route("Buy")]
        [HttpPost]
        public async Task<IActionResult> BuyProduct()
        {

            return Ok();
        }
    }
}
