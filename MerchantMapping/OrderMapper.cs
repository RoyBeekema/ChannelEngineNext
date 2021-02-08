using MerchantDomain;
using System.Collections.Generic;

namespace MerchantMapping
{
    // TODO[r.beekema] Use mapping for better design
    public static class OrderMapper
    {
        public static OrderModel ToOrderData(Dictionary<string,object> orderLine)
        {
            return new OrderModel
            {
                Description = (string)orderLine["Description"],
                Gtin = (string)orderLine["Gtin"],
                Quantity = (int)orderLine["Quantity"]
            };
        }
    }
}
