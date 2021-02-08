using System.Collections.Generic;

namespace ChannelEngineApp
{
    public class Order
    {
        public int Id { get; set; }
 
        public string ChannelOrderNo { get; set; }

        public decimal TotalInclVat { get; set; }

        public List<OrderLine> Lines { get; set; }
    }
}
