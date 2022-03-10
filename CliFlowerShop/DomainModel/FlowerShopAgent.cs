using System;
using System.Linq;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainExceptions;

namespace CliFlowerShop.DomainModel
{
    public class FlowerShopAgent
    {
        private readonly FlowerShop _shop;
        private readonly StockConfiguration _stockConfiguration;

        public FlowerShopAgent()
        {
            _stockConfiguration = ConfigurationLoader<StockConfiguration>.Load("stock.json");
            _shop = new FlowerShop(_stockConfiguration);
        }

        public void BuyFlowers()
        {
            PrintWelcomeBanner();
            
            var newOrders = _shop.NewOrders();
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    break;
                
                AddOrderInternal(newOrders, input);
            }

            SubmitOrderAndPrintReceipt(newOrders);
        }

        private void AddOrderInternal(FlowerOrders orders, string newOrder)
        {
            try
            {
                orders.AddOrder(newOrder);
            }
            catch (InvalidFlowerCodeException)
            {
                PrintFlowerCodeError();
            }
            catch (InvalidOrderFormatException)
            {
                PrintInvalidFormatError();
            }
            catch (InvalidFlowerCountException)
            {
                PrintFlowerCountError();
            }
            catch (OrderAlreadyExistsException)
            {
                PrintOrderAlreadyExistsError();
            }
        }
        
        private void SubmitOrderAndPrintReceipt(IOrders orders)
        {
            Console.WriteLine("Your Receipt:");
            Console.WriteLine(_shop.SubmitOrder(orders).ToString());
        }

        private void PrintWelcomeBanner()
            => Console.WriteLine("Welcome to CLI Flower Shop!\n" +
                              "The following flowers are available:\n" +
                              $"{GetFormattedValidFlowers()}\n" +
                              "You can add order in the following format:\n" +
                              "\t[COUNT] [FLOWER CODE]\n" +
                              "For Example:\n" +
                              "\t\"10 R12\"\n" +
                              "Leave your input empty to submit your order!\n" +
                              "Place your order below!:\n");
        

        private void PrintFlowerCodeError()
            => Console.WriteLine("Flower code is Invalid! Valid flower codes are:\n" +
                                 $"{GetFormattedValidFlowers()}\n");

        private void PrintInvalidFormatError()
            => Console.WriteLine("Order format is Invalid! Valid format is:\n" +
                                 "\t[POSITIVE_NUMBER] [FLOWER CODE]\n" +
                                 "For Example:\n" +
                                 $"\t{GetFormattedOrderExample()}\n");
        
        private static void PrintFlowerCountError()
            => Console.WriteLine("Order count must be greater than zero!\n");

        private static void PrintOrderAlreadyExistsError()
            => Console.WriteLine("Order already exists!\n");

        private string GetFormattedValidFlowers()
            => string.Join("\n", _stockConfiguration.Flowers.Select(f => $"\t{f.Code}: {f.Name}"));
        
        private string GetFormattedOrderExample()
            => _stockConfiguration.Flowers.Select(f => $"10 {f.Code}").First();
    }
}
