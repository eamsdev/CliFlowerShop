using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliFlowerShop.DomainModel
{
    public class Invoice
    {
        private readonly List<string> _invalidOrders;
        private readonly List<IFlowersBundle> _allBundles;

        public Invoice(
            IEnumerable<IFlowersBundle> allBundles, 
            IEnumerable<string> invalidOrders)
        {
            _allBundles = allBundles.ToList();
            _invalidOrders = invalidOrders.ToList();
        }

        public override string ToString()
            => string.Join("", _allBundles.Select(IndividualInvoice)) 
               + SpecialNote();
        
        private static string IndividualInvoice(IFlowersBundle bundles)
        {
            var totalFlowers = 0;
            var totalCost = (decimal) 0;
            var individualBundlePrintOut = new StringBuilder();
            
            foreach (var (bundleCount, bundle) in bundles.OrderedBundles)
            {
                totalFlowers += bundleCount * bundle.Size;
                totalCost += bundleCount * bundle.Cost;
                
                individualBundlePrintOut.AppendLine(
                    $"\t{bundleCount} x {bundle.Size} ${bundle.Cost}");
            }

            return $"{totalFlowers} {bundles.Code} ${totalCost}\n" +
                   $"{individualBundlePrintOut}";
        }
        
        private string SpecialNote()
        {
            if (!_invalidOrders.Any())
                return string.Empty;
            
            return "\nThe following orders contain invalid flower counts: " +
                   $"{string.Join(", ", _invalidOrders)}";
        }
    }
}