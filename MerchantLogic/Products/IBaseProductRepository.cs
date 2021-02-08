using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelEngineApp
{
    public interface IBaseProductRepository
    {
        public IEnumerable<Product> Products { get; }

        public Task Sync();

        public Task Set(string key, string property, int value);
    }
}
