using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelEngineApp
{
    public interface IBaseProductRepository
    {
        /// <summary>
        /// Products retrieved from the dataset
        /// </summary>
        public IEnumerable<Product> Products { get; }

        /// <summary>
        /// Retrieves the latest version of the products dataset and stores it in the Products property
        /// </summary>
        /// <returns></returns>
        public Task Sync();

        /// <summary>
        /// Changes a single value within the merchant data set
        /// </summary>
        /// <param name="key">The merchant product number</param>
        /// <param name="property">The property name to change. (Currently only 'Stock' is supported)</param>
        /// <param name="value">The value to set</param>
        /// <returns></returns>
        public Task Set(string key, string property, int value);
    }
}
