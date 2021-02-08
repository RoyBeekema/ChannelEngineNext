using System;
using System.Collections.Generic;

namespace MerchantData
{
    public class ProductStock
    {
        public int Value { get; set; }

        public string Path { get { return "Stock"; } }

        public string Op { get { return "replace"; } }
    }
}
