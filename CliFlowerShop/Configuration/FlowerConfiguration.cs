using System.Collections.Generic;

namespace CliFlowerShop.Configuration
{
    public class FlowerConfiguration
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<BundleConfiguration> Bundles { get; set; }
    }
}
