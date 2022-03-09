using System.Linq;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainModel;
using Xunit;

namespace CliFlowerShop.Test
{
    public class FlowerBundlesTests : FlowerShopTestBase
    {
        private readonly FlowerConfiguration _liliesConfig;

        public FlowerBundlesTests()
        {
            _liliesConfig = Stock.Flowers.Single(f => f.Name == "Lilies");
        }

        [Fact]
        public void EmptyBundleContainsNoItems()
        {
            // Given: a flower bundle
            var bundle = new FlowerBundles(_liliesConfig);

            // Then: the bundle starts empty
            Assert.Empty(bundle.OrderedBundles);
        }

        [Fact]
        public void BundleOfFlowersOfSameSize()
        {
            // Given: a flower bundle
            var bundle = new FlowerBundles(_liliesConfig);

            // When: I add a bundle of size 3
            bundle.Add(3);
            bundle.Add(3);

            // Then: There are 2 bundles of size 3
            Assert.Single(bundle.OrderedBundles);
            
            Assert.Equal("L09", bundle.Code);
            Assert.Equal(2, bundle.OrderedBundles.Single().bundleCount);
            Assert.Equal(3, bundle.OrderedBundles.Single().bundle.Size);
            Assert.Equal((decimal)6.99, bundle.OrderedBundles.Single().bundle.Cost);
        }

        [Fact]
        public void BundleOfFlowersOfMixedSizes()
        {
            // Given: a flower bundle
            var bundle = new FlowerBundles(_liliesConfig);

            // When: I add a bundle of size 3
            bundle.Add(3);
            bundle.Add(6);

            // Then: There are 2 bundles of size 3 and 6
            Assert.Equal(2, bundle.OrderedBundles.Count());
            Assert.Equal("L09", bundle.Code);
            
            Assert.Equal(1, bundle.OrderedBundles.ToList()[0].bundleCount);
            Assert.Equal(3, bundle.OrderedBundles.ToList()[0].bundle.Size);
            Assert.Equal((decimal)6.99, bundle.OrderedBundles.ToList()[0].bundle.Cost);

            Assert.Equal(1, bundle.OrderedBundles.ToList()[1].bundleCount);
            Assert.Equal(6, bundle.OrderedBundles.ToList()[1].bundle.Size);
            Assert.Equal((decimal)16.95, bundle.OrderedBundles.ToList()[1].bundle.Cost);
        }
    }
}
