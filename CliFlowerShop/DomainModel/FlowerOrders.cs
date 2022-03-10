using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainExceptions;

namespace CliFlowerShop.DomainModel
{
    public class FlowerOrders : IOrders
    {
        private const string OrderCountGroup = "OrderCount";
        private const string FlowerCodeGroup = "FlowerCode";
        private const string OrderRegexPattern = @"^(?<OrderCount>\d+)\s+(?<FlowerCode>(\w|\d)+)$";

        private readonly Regex _orderFormat;
        private readonly StockConfiguration _stock;
        private readonly List<(int flowerCount, string flowerCode)> _orders;

        public FlowerOrders(StockConfiguration stock)
        {
            _stock = stock;
            _orderFormat = new Regex(OrderRegexPattern);
            _orders = new List<(int flowerCount, string flowerCode)>();
        }

        public IEnumerable<(int flowerCount, string flowerCode)> Orders 
            => _orders;

        public void AddOrder(string userInput)
        {
            userInput = userInput.Trim(' ');
            var match = _orderFormat.Match(userInput);
            
            AssertValidOrderFormat(match);

            var flowerCount = int.Parse(match.Groups[OrderCountGroup].Value);
            var flowerCode = match.Groups[FlowerCodeGroup].Value;

            AssertFlowerCodeAndFlowerCountIsValid(flowerCode, flowerCount);
            AddOrderInternal(flowerCount, flowerCode);
        }

        private static void AssertValidOrderFormat(Match match)
        {
            if (!match.Success)
                throw new InvalidOrderFormatException();
        }
        
        private void AssertFlowerCodeAndFlowerCountIsValid(
            string flowerCode, 
            int flowerCount)
        {
            if (!IsValidFlowerCode(flowerCode))
                throw new InvalidFlowerCodeException();

            if (flowerCount == 0)
                throw new InvalidFlowerCountException();

            if (FlowerCodeAlreadyExists(flowerCode))
                throw new OrderAlreadyExistsException();
        }
        
        private bool IsValidFlowerCode(string flowerCode)
            => _stock.Flowers.Exists(f 
                => string.Compare(
                    f.Code, 
                    flowerCode, 
                    StringComparison.Ordinal) == 0);

        private void AddOrderInternal(int count, string code) 
            => _orders.Add((count, code));

        private bool FlowerCodeAlreadyExists(string flowerCode)
            => _orders.Exists(o => o.flowerCode == flowerCode);
    }
}
