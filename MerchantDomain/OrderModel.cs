namespace MerchantDomain
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
        public string MerchantProductNo { get; set; }
    }
}
