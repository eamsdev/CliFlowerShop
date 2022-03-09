using System.Collections.Generic;
using System.Linq;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainExceptions;
using CliFlowerShop.DomainModel;
using Xunit;

namespace CliFlowerShop.Test
{
    public class BundleCalculatorTests : FlowerShopTestBase
    {
        private readonly FlowerConfiguration _tulipsConfig;

        public BundleCalculatorTests()
        {
            _tulipsConfig = Stock.Flowers.Single(f => f.Name == "Tulips");
        }

        [Fact]
        public void RetrieveFlowerBundlesForZeroOrderedCount()
        {
            // Given: The Bundle Calculator
            var bundleCalculator = new BundlesCalculator(_tulipsConfig);

            // When: I request 0 flower
            // Then: An Exception is thrown
            Assert.Throws<InvalidFlowerCountException>(
                () => bundleCalculator.GetValidBundles(0));
        }

        [Fact]
        public void RetrieveFlowerBundlesForLessThanSmallestBundle()
        {
            // Given: The Bundle Calculator
            var bundleCalculator = new BundlesCalculator(_tulipsConfig);

            // When: I request 2 flower
            // Then: An Exception is thrown
            Assert.Throws<InvalidFlowerCountException>(
                () => bundleCalculator.GetValidBundles(2));
        }

        [Fact]
        public void RetrieveFlowerBundlesNotDivisible()
        {
            // Given: The Bundle Calculator
            var bundleCalculator = new BundlesCalculator(_tulipsConfig);

            // When: I request 4, 7, 11, 23 flowers
            // Then: An Exception is thrown for each call
            var numFlowers = new List<int> { 4, 7, 11, 23 };
            numFlowers.ForEach(_ => Assert.Throws<InvalidFlowerCountException>(
                () => bundleCalculator.GetValidBundles(4)));
        }

        [Fact]
        public void RetrieveFlowerBundlesForSmallestBundle()
        {
            // Given: The Bundle Calculator
            var bundleCalculator = new BundlesCalculator(_tulipsConfig);

            // When: I request 3 flowers
            var bundles = bundleCalculator.GetValidBundles(3);

            // Then: The Bundles have the expected content
            Assert.Equal("T58", bundles.Code);
            Assert.Single(bundles.OrderedBundles);
            Assert.Equal(1, bundles.OrderedBundles.First().bundleCount);
            Assert.Equal((decimal)5.95, bundles.OrderedBundles.First().bundle.Cost);
            Assert.Equal(3, bundles.OrderedBundles.First().bundle.Size);
        }

        [Fact]
        public void RetrieveFlowerBundlesForCombinedBundle()
        {
            // Given: The Bundle Calculator
            var bundleCalculator = new BundlesCalculator(_tulipsConfig);

            // When: I request 17 flowers
            var bundles = bundleCalculator.GetValidBundles(17);

            // Then: The Bundles have the expected content
            Assert.Equal("T58", bundles.Code);
            Assert.Equal(3, bundles.OrderedBundles.Count());
            Assert.Equal(9, bundles.OrderedBundles.ToList()[0].bundle.Size);
            
            Assert.All(bundles.OrderedBundles, bundle => Assert.True(bundle.bundleCount == 1));
            Assert.Equal((decimal)16.99, bundles.OrderedBundles.ToList()[0].bundle.Cost);
            
            Assert.Equal(5, bundles.OrderedBundles.ToList()[1].bundle.Size);
            Assert.Equal((decimal)9.95, bundles.OrderedBundles.ToList()[1].bundle.Cost);
            
            Assert.Equal(3, bundles.OrderedBundles.ToList()[2].bundle.Size);
            Assert.Equal((decimal)5.95, bundles.OrderedBundles.ToList()[2].bundle.Cost);
        }

        [Fact]
        public void RetrieveFlowerBundlesForMultipleNonLargestBundle()
        {
            // Given: The Bundle Calculator
            var bundleCalculator = new BundlesCalculator(Stock.Flowers.Last());

            // When: I request 13 flowers
            var bundles = bundleCalculator.GetValidBundles(13);

            // Then: The Bundles have the expected content
            Assert.Equal("T58", bundles.Code);
            Assert.Equal(2, bundles.OrderedBundles.Count());
            
            Assert.Equal(5, bundles.OrderedBundles.ToList()[0].bundle.Size);
            Assert.Equal(2, bundles.OrderedBundles.ToList()[0].bundleCount);
            Assert.Equal((decimal)9.95, bundles.OrderedBundles.ToList()[0].bundle.Cost);

            Assert.Equal(3, bundles.OrderedBundles.ToList()[1].bundle.Size);
            Assert.Equal(1, bundles.OrderedBundles.ToList()[1].bundleCount);
            Assert.Equal((decimal)5.95, bundles.OrderedBundles.ToList()[1].bundle.Cost);
        }
    }
}
