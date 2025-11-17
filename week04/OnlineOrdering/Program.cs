using System;
using System.Collections.Generic;

namespace Foundation2
{
    class Program
    {
        static void Main(string[] args)
        {
            // ----- First customer (USA) -----
            Address address1 = new Address(
                "123 Main Street",
                "Dallas",
                "TX",
                "USA");

            Customer customer1 = new Customer("Sarah Johnson", address1);

            Order order1 = new Order(customer1);

            Product prod1 = new Product("Wireless Mouse", "WM-100", 15.99m, 2);
            Product prod2 = new Product("Mechanical Keyboard", "KB-200", 49.50m, 1);
            Product prod3 = new Product("USB-C Cable", "CB-300", 7.25m, 3);

            order1.AddProduct(prod1);
            order1.AddProduct(prod2);
            order1.AddProduct(prod3);

            // ----- Second customer (International) -----
            Address address2 = new Address(
                "45 Queen's Road",
                "London",
                "Greater London",
                "United Kingdom");

            Customer customer2 = new Customer("Lilian Anyanwu", address2);

            Order order2 = new Order(customer2);

            Product prod4 = new Product("27\" Monitor", "MN-400", 189.99m, 1);
            Product prod5 = new Product("Laptop Stand", "LS-500", 32.00m, 1);

            order2.AddProduct(prod4);
            order2.AddProduct(prod5);

            // ----- Display order details -----
            List<Order> orders = new List<Order> { order1, order2 };
            int orderNumber = 1;

            foreach (Order order in orders)
            {
                Console.WriteLine($"=======================");
                Console.WriteLine($"Order #{orderNumber}");
                Console.WriteLine("=======================\n");

                Console.WriteLine("Packing Label:");
                Console.WriteLine(order.GetPackingLabel());

                Console.WriteLine("Shipping Label:");
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine();

                Console.WriteLine($"Total Cost: ${order.GetTotalCost():0.00}");
                Console.WriteLine();

                orderNumber++;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
