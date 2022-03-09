using System.Collections.Generic;
using System.Linq;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainExceptions;

namespace CliFlowerShop.DomainModel
{
    public class BundlesCalculator
    {
        private readonly FlowerConfiguration _flowerConfig;
        private readonly List<(int Size, decimal Cost)> _orderedCountAndCost;

        public BundlesCalculator(FlowerConfiguration flowerConfig)
        {
            _flowerConfig = flowerConfig;
            _orderedCountAndCost = flowerConfig.Bundles
                .OrderByDescending(b => b.Size)
                .Select(b => (Count: b.Size, b.Cost)).ToList();
        }

        public FlowerBundles GetValidBundles(int orderedCount)
        {
            var retryCount = 0;
            if (orderedCount == 0)
                throw new InvalidFlowerCountException();

            FlowerBundles flowerBundles = null;
            while (ValidBundleHasNotBeenFound(flowerBundles, retryCount))
                flowerBundles = TryGetValidBundleWithBundleOffset(orderedCount, retryCount++);

            if (flowerBundles == default(FlowerBundles))
                throw new InvalidFlowerCountException();

            return flowerBundles;
        }

        private FlowerBundles TryGetValidBundleWithBundleOffset(
            int orderedCount,
            int retryCount)
        {
            var bundleSizesToTry = _orderedCountAndCost
                .Select(item => item.Size)
                .Skip(retryCount);

            var validBundle = new FlowerBundles(_flowerConfig);
            foreach (var size in bundleSizesToTry)
                while (orderedCount >= size)
                {
                    validBundle.Add(size);
                    orderedCount -= size;
                }

            return orderedCount != 0 ? null : validBundle;
        }

        private bool ValidBundleHasNotBeenFound(FlowerBundles flowerBundles, int retryCount)
            => flowerBundles == null && retryCount < _orderedCountAndCost.Count;
    }
}