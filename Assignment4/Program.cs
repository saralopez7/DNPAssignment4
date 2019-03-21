
using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace Assignment4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region A) Using Object Initializers to instantiate the 3 customers

            var milk = CreateProduct("Milk", new decimal(7.04));
            var butter = CreateProduct("Butter", new decimal(5.04));
            var bread = CreateProduct("Bread", new decimal(26.54));
            var cacao = CreateProduct("Cacao", new decimal(37));
            var juice = CreateProduct("Juice", new decimal(18));

            Order[] kimOrders = { CreateOrder(1, milk), CreateOrder(2, butter), CreateOrder(1, bread) };
            var kim = CreateCustomer("Kim Foged", "Beder", kimOrders);

            Order[] ibOrders = { CreateOrder(4, milk), CreateOrder(1, butter), CreateOrder(1, bread), CreateOrder(1, cacao) };
            var ib = CreateCustomer("Ib Havn", "Horsens", ibOrders);

            Order[] rasmusOrders = { CreateOrder(10, juice) };
            var rasmus = CreateCustomer("Rasmus Bjerner", "Horsens", rasmusOrders);

            #endregion

            var customers = new List<Customer> { kim, ib, rasmus };

            #region B) Select all customers. Print the Name and City of each customer
            Console.WriteLine("All registered customers:");

            customers.Select(
                customer => {
                    Console.WriteLine($"Name: {customer.Name}, City: {customer.City}");
                    return customer;
                }).ToList();

            #endregion

            #region C) Select all customers from Horsens. Print the Name of each customer

            Console.WriteLine("\nCustomers living in Horsens:");
            customers.Where(customer => customer.City == "Horsens").Select(
                customer => {
                    Console.WriteLine($"Customer {customer.Name} lives in Horsens");
                    return customer;
                }).ToList();

            #endregion

            #region D) Select the count of orders for the customer Ib Havn. Print the number

            var ibOrderCount = customers.Where(customer => customer.Name == "Ib Havn")
                .Select(customer => customer.Orders)
                .Count();

            Console.WriteLine($"\nNumber of orders Ib Havn made: {ibOrderCount}");

            #endregion

            #region E) Select all customers buying milk. Print the Name of each customer 

            Console.WriteLine("\nCustomers who bought milk:");
            customers.Where(customer => customer.Orders.Any(x => x.Product == milk))
                .Select(customer =>
                {
                    var milkQuantity = customer.Orders.Where(order => order.Product == milk)
                        .Sum(product => product.Quantity);

                    Console.WriteLine($"Customer {customer.Name} bought {milkQuantity}L of milk");
                    return customer;
                }).ToList();

            #endregion

            #region F) Select the total sum (prices of products in Orders) from each customer. Print the Name and Sum of each customer

            Console.WriteLine("\nTotal amount of money every customer spent in all his products:");

            customers.Select(c =>
            {
                var totalPrice = c.Orders.Select(o => o.Product.Price).Sum();
                Console.WriteLine($"Customer {c.Name} spent {totalPrice}kr.");
                return c;
            }).ToList();

            #endregion

            #region G) Select the total sum(All customers sum added).Print the result

            var totalAmount = customers.Select(c => c.Orders.Select(o => o.Product.Price).Sum()).Sum();
            Console.WriteLine($"\nTotal amount of money all the customers spent together: {totalAmount}kr.");

            #endregion

            Console.ReadLine();
        }

        public static Product CreateProduct(string name, decimal price)
        {
            return new Product
            {
                Name = name,
                Price = price
            };
        }

        public static Customer CreateCustomer(string name, string city, Order[] orders)
        {
            return new Customer
            {
                Name = name,
                City = city,
                Orders = orders
            };
        }

        public static Order CreateOrder(int quantity, Product product)
        {
            return new Order
            {
                Quantity = quantity,
                Product = product
            };
        }
    }
}
