﻿using System;
using CliFlowerShop.DomainModel;

namespace CliFlowerShop
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            new FlowerShopAgent().BuyFlowers();
            Console.ReadLine();
        }
    }
}
