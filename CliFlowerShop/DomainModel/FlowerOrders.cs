using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainExceptions;
using Microsoft.VisualBasic;

namespace CliFlowerShop.DomainModel
{
    public class FlowerOrders : IOrders
    {
        private const string OrderCountGroup = "OrderCount";
        private const string FlowerCodeGroup = "FlowerCode";

        private readonly Regex _orderFormat;
        private readonly StockConfiguration _stock;

        public FlowerOrders(StockConfiguration stock)
        {
            _stock = stock;
            _orderFormat = new Regex(@"(?<OrderCount>\d+)\s+(?<FlowerCode>(\w|\d)+)$");
            Orders = new List<(int flowerCount, string flowerCode)>();
        }
        
        public List<(int flowerCount, string flowerCode)> Orders { get; }

        public void AddOrder(string userInput)
        {
            userInput = Strings.Trim(userInput);
            
            var match = _orderFormat.Match(userInput);
            if (!match.Success)
                throw new InvalidOrderFormatException();

            var flowerCount = int.Parse(match.Groups[OrderCountGroup].Value);
            var flowerCode = match.Groups[FlowerCodeGroup].Value;

            if (!IsValidFlowerCode(flowerCode))
                throw new InvalidFlowerCodeException();

            if (flowerCount == 0)
                throw new InvalidFlowerCountException();

            if (Orders.Exists(o => o.flowerCode == flowerCode))
                throw new OrderAlreadyExistsException();

            AddOrderInternal(flowerCount, flowerCode);
        }
        
        private bool IsValidFlowerCode(string flowerCode)
            => _stock.Flowers.Exists(f 
                => string.Compare(
                    f.Code, 
                    flowerCode, 
                    StringComparison.Ordinal) == 0);

        private void AddOrderInternal(int count, string code) 
            => Orders.Add((count, code));
    }
}
