namespace Ecommerce.Controllers
{
    internal class Cart
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? UserID { get; set; }
    }
}