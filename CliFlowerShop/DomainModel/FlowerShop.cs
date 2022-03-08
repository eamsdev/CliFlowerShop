using CliFlowerShop.Configuration;
using System.Collections.Generic;
using System.Linq;
using CliFlowerShop.DomainExceptions;

namespace CliFlowerShop.DomainModel
{
    public class FlowerShop
    {
        private readonly StockConfiguration _stock;
        private readonly Dictionary<string, BundlesCalculator> _bundleCalculators;

        public FlowerShop(StockConfiguration stock)
        {
            _stock = stock;
            _bundleCalculators = stock.Flowers.ToDictionary(
                flowerConfig => flowerConfig.Code,
                flowerConfig => new BundlesCalculator(flowerConfig));
        }

        public Invoice SubmitOrder(IOrders orders)
        {
            var invalidOrders = new List<string>();
            var bundles = new List<FlowerBundles>();
            foreach (var (flowerCount, flowerCode) in orders.Orders)
            {
                try
                {
                    bundles.Add(_bundleCalculators[flowerCode]
                        .GetValidBundles(flowerCount));
                }
                catch (InvalidFlowerCountException)
                {
                    invalidOrders.Add(flowerCode);
                }
            }

            var invoice = new Invoice(bundles, invalidOrders);
            return invoice;
        }

        public FlowerOrders NewOrders() 
            => new(_stock);
    }
}
