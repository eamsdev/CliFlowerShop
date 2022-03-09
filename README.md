[![Build Status](https://app.travis-ci.com/pete-eams/CliFlowerShop.svg?branch=master)](https://app.travis-ci.com/pete-eams/CliFlowerShop)

# Flower Shop Challenge

## Context

A flower shop used to base the price of their flowers on an item by item cost. So if a
customer ordered 10 roses then they would be charged 10x the cost of single rose. The
flower shop has decided to start selling their flowers in bundles and charging the customer
on a per bundle basis. So if the shop sold roses in bundles of 5 and 10 and a customer
ordered 15 they would get a bundle of 10 and a bundle of 5.
The flower shop currently sells the following products:

| Name   | Code | Bundle                                |
|--------|------|---------------------------------------|
| Roses  | R12  | 5 @ $6.99<br>10 @ $12.99              |
| Lilies | L09  | 3 @ $9.95<br>6 @ $16.95<br>9 @ $24.95 |
| Tulips | T58  | 3 @ $5.95<br>5 @ $9.95<br>9 @ $16.99  |

## Task
Given a customer order you are required to determine the cost and bundle breakdown for
each product. To save on shipping space each order should contain the minimal number
of bundles.

## Pre-requisites
1. .Net 5.0 SDK needs to be installed
    * Confirm that you have it installed by running: `dotnet --version`, you should see: `5.0.XXX`

## Running the tests
1. From root directory run `dotnet test`

## Running the solution
1. Run via dotnet command directly
    * From root directory, navigate to the `CliFlowerShop` directory
    * run: `dotnet run`
2. Publish and run via executable
    * run: `dotnet publish -c Release`
    * navigate to: `.\CliFlowerShop\bin\Release\net5.0`
    * execute CliFlowerShop.exe