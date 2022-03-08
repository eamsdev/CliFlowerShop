using System;
using CliFlowerShop.Configuration;
using CliFlowerShop.DomainExceptions;

namespace CliFlowerShop.DomainModel
{
    public class FlowerShopAgent
    {
        private readonly FlowerShop _shop;

        public FlowerShopAgent()
        {
            var stock = ConfigurationLoader<StockConfiguration>.Load("Configuration/stock.json");
            _shop = new FlowerShop(stock);
        }

        public void Run()
        {
            var newOrders = _shop.NewOrders();

            Console.WriteLine("Welcome to CLI Flower Shop!\n" +
                              "The following flowers are available:\n" +
                              "\t1. R12: Roses\n" +
                              "\t2. L09: Lilies\n" +
                              "\t3. T58: Tulips\n" +
                              "You can add order in the following format:\n" +
                              "\t[COUNT] [FLOWER CODE]\n" +
                              "For Example:\n" +
                              "\t\"10 R12\"\n" +
                              "\twill order 10 Roses!\n" +
                              "Leave your input empty to submit your order!\n" +
                              "Place your order below!:\n");

            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    break;

                try
                {
                    newOrders.AddOrder(input);
                }
                catch (InvalidFlowerCodeException)
                {
                    Console.WriteLine("Flower code is Invalid! Valid flower codes are:\n" +
                                      "\t1. R12: Roses\n" +
                                      "\t2. L09: Lilies\n" +
                                      "\t3. T58: Tulips");
                }
                catch (InvalidOrderFormatException)
                {
                    Console.WriteLine("Order format is Invalid! Valid format is:\n" +
                                      "\t[POSITIVE_NUMBER] [FLOWER CODE]\n" +
                                      "For Example:\n" +
                                      "\t\"10 R12\"");
                }
                catch (InvalidFlowerCountException)
                {
                    Console.WriteLine("Order count must be greater than zero!");
                }
                catch (OrderAlreadyExistsException)
                {
                    Console.WriteLine("Order already exists!");
                }

            }

            Console.WriteLine("Your Receipt:");
            Console.WriteLine(_shop.SubmitOrder(newOrders).ToString());
            Console.ReadLine();
        }
    }
}
