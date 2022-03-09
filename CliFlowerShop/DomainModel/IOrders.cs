using System.Collections.Generic;

namespace CliFlowerShop.DomainModel
{
    public interface IOrders
    {
        public IEnumerable<(int flowerCount, string flowerCode)> Orders { get; }
    }
}