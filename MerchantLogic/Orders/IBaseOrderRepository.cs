using MerchantData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerchantLogic
{
    public interface IBaseOrderRepository
    {
        /// <summary>
        /// Orders retrieved from the dataset
        /// </summar
        public IEnumerable<Order> Orders { get; }

        /// <summary>
        /// Retrieves the latest version of the orders dataset and stores it in the Orders property
        /// </summary>
        /// <returns></returns>
        public Task Sync();
    }
}
