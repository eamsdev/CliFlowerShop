using CliFlowerShop.DomainExceptions;
using CliFlowerShop.DomainModel;
using Xunit;

namespace CliFlowerShop.Test
{
    public class FlowerOrdersTests : FlowerShopTestBase
    {
        [Fact]
        public void EmptyOrderTest()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit an empty order
            // Then: Exception is raised
            Assert.Throws<InvalidOrderFormatException>(
                () => order.AddOrder(""));
        }

        [Fact]
        public void WhiteSpaceOrderTest()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);
            
            // When: I submit an order with white space
            // Then: Exception is raised
            Assert.Throws<InvalidOrderFormatException>(
                () => order.AddOrder(" "));
        }

        [Fact]
        public void InvalidOrderFormatTest()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit an order with wrong format
            // Then: Exception is raised
            Assert.Throws<InvalidOrderFormatException>(
                () => order.AddOrder("Hamilton is a good musical"));
        }

        [Fact]
        public void InvalidFlowerTests()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit an order with wrong format
            // Then: Exception is raised
            Assert.Throws<InvalidFlowerCodeException>(
                () => order.AddOrder("10 F00"));
        }

        [Fact]
        public void ZeroFlowerTests()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit an order with 0 Lilies
            // Then: Exception is raised
            Assert.Throws<InvalidFlowerCountException>(
                () => order.AddOrder("0 L09"));
        }


        [Fact]
        public void RepeatedOrdersTest()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit repeated orders
            // Then: Exception is raised
            order.AddOrder("10 L09");
            Assert.Throws<OrderAlreadyExistsException>(
                () => order.AddOrder("10 L09"));
        }

        [Fact]
        public void ValidOrderTests()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit an order with 10 Lilies
            order.AddOrder("10 L09");
            
            // Then: Expected items is found
            Assert.Contains(
                order.Orders, 
                o => o.flowerCode == "L09" 
                     && o.flowerCount == 10);
        }

        [Fact]
        public void ValidMultipleOrderTests()
        {
            // Given: a flower order
            var order = new FlowerOrders(Stock);

            // When: I submit an order with 9 Lilies
            // And: I submit an order with 12 Roses
            order.AddOrder("9 L09");
            order.AddOrder("12 R12");

            // Then: Expected items is found
            Assert.Contains(
                order.Orders,
                o => o.flowerCode == "L09" 
                     && o.flowerCount == 9);
            Assert.Contains(
                order.Orders,
                o => o.flowerCode == "R12"
                     && o.flowerCount == 12);
        }
    }
}
