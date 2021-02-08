using System;
using System.Collections.Generic;

namespace MerchantData
{
    public class OrderLine
    {
        public string Description { get; set; }

        public string Gtin { get; set; }

        public int Quantity { get; set; }

        public string MerchantProductNo { get; set; }
    }
}
