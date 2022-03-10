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
```
D:\Dev\CliFlowerShop>dotnet test
  Determining projects to restore...
  All projects are up-to-date for restore.
  CliFlowerShop -> D:\Dev\CliFlowerShop\CliFlowerShop\bin\Debug\net5.0\CliFlowerShop.dll
  CliFlowerShop.Test -> D:\Dev\CliFlowerShop\CliFlowerShop.Test\bin\Debug\net5.0\CliFlowerShop.Test.dll
Test run for D:\Dev\CliFlowerShop\CliFlowerShop.Test\bin\Debug\net5.0\CliFlowerShop.Test.dll (.NETCoreApp,Version=v5.0)
Microsoft (R) Test Execution Command Line Tool Version 16.11.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:    17, Skipped:     0, Total:    17, Duration: 25 ms - CliFlowerShop.Test.dll (net5.0)
```
## Running the solution
1. Run via dotnet command directly
    * From root directory, navigate to the `CliFlowerShop` directory
    * run: `dotnet run`
2. Publish and run via executable
    * run: `dotnet publish -c Release -o dist`
    * navigate to: `dist`
    * execute CliFlowerShop.exe
3. Build and run from your IDE of choice (Microsoft Visual Studio/JetBrains Rider)

### Program output example
```
Welcome to CLI Flower Shop!
The following flowers are available:
        R12: Roses
        L09: Lilies
        T58: Tulips
You can add order in the following format:
        [COUNT] [FLOWER CODE]
For Example:
        "10 R12"
Leave your input empty to submit your order!
Place your order below!:

foobar foobar foobar
Order format is Invalid! Valid format is:
        [POSITIVE_NUMBER] [FLOWER CODE]
For Example:
        10 R12

10 X12
Flower code is Invalid! Valid flower codes are:
        R12: Roses
        L09: Lilies
        T58: Tulips

0 R12
Order count must be greater than zero!

10 R12
15 L09
13 T58

Your Receipt:
10 R12 $12.99
        1 x 10 $12.99
15 L09 $41.90
        1 x 9 $24.95
        1 x 6 $16.95
13 T58 $25.85
        2 x 5 $9.95
        1 x 3 $5.95
```