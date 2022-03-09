using System.Collections.Generic;
using System.Linq;
using CliFlowerShop.Configuration;

namespace CliFlowerShop.DomainModel
{
    public class FlowerBundles : IFlowersBundle
    {
        private readonly List<int> _addedBundleSize;
        private readonly FlowerConfiguration _flowerConfig;

        public FlowerBundles(FlowerConfiguration flowerConfig)
        {
            Code = flowerConfig.Code;
            _flowerConfig = flowerConfig;
            _addedBundleSize = new List<int>();
        }
        
        public string Code { get; }

        public void Add(int bundleSize)
            => _addedBundleSize.Add(bundleSize);

        public IEnumerable<(int bundleCount, Bundle bundle)> OrderedBundles
            => _addedBundleSize
                .GroupBy(Size)
                .Select(CountAndBundleTuple);
        
        private decimal GetCostByBundleSize(int bundleSize)
            => _flowerConfig.Bundles.First(bundle => bundle.Size == bundleSize).Cost;

        // Syntactic Sugaring
        private static int Size(int size) => size;
        
        // Syntactic Sugaring
        private (int, Bundle) CountAndBundleTuple(IGrouping<int, int> grouping)
            => (grouping.Count(), new Bundle(grouping.Key, GetCostByBundleSize(grouping.Key)));
    }
}