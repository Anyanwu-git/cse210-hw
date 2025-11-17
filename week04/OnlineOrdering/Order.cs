using System;
using System.Collections.Generic;

namespace Foundation2
{
    public class Order
    {
        private List<Product> _products = new List<Product>();
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        private decimal GetSubtotal()
        {
            decimal subtotal = 0;

            foreach (Product product in _products)
            {
                subtotal += product.GetTotalPrice();
            }

            return subtotal;
        }

        private decimal GetShippingCost()
        {
            // $5 if in the USA, otherwise $35
            if (_customer.IsInUSA())
            {
                return 5m;
            }
            else
            {
                return 35m;
            }
        }

        public decimal GetTotalCost()
        {
            return GetSubtotal() + GetShippingCost();
        }

        public string GetPackingLabel()
        {
            // List name and product id of each product
            // One per line
            string label = "";

            foreach (Product product in _products)
            {
                label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
            }

            return label;
        }

        public string GetShippingLabel()
        {
            return _customer.GetShippingLabel();
        }
    }
}
