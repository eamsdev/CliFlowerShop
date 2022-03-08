using System.Collections.Generic;

namespace CliFlowerShop.DomainModel
{
    public interface IOrders
    {
        public List<(int flowerCount, string flowerCode)> Orders { get; }
    }
}