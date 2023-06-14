using ApiWebFood.Entities;

namespace ApiWebFood.Data
{
    [Serializable]
    public class ShppingCartModelcs
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
