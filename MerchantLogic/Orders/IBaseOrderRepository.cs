using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelEngineApp
{
    public interface IBaseOrderRepository
    {
        public IEnumerable<Order> Orders { get; }

        public Task Sync();
    }
}
