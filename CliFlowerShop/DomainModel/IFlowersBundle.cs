using System.Collections.Generic;

namespace CliFlowerShop.DomainModel
{
    public interface IFlowersBundle
    {
        public string Code { get; }
        public IEnumerable<(int bundleCount, Bundle bundle)> OrderedBundles { get; }
    }
}
