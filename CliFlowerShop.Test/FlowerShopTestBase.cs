using CliFlowerShop.Configuration;

namespace CliFlowerShop.Test
{
    public class FlowerShopTestBase
    {
        protected readonly StockConfiguration Stock;
        public FlowerShopTestBase()
        {
            Stock = ConfigurationLoader<StockConfiguration>.Load("stock.json");
        }
    }
}
