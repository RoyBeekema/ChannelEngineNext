using System;
using System.Collections.Generic;

namespace ChannelEngineApp
{
    public class ProductStock
    {
        public int Value { get; set; }

        public string Path { get { return "Stock"; } }

        public string Op { get { return "replace"; } }
    }
}
