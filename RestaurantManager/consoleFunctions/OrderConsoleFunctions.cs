using ConsoleTables;
using RestaurantManager.entity;
using RestaurantManager.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.consoleFunctions
{
    class OrderConsoleFunctions : MiscConsoleFunctions
    {
        readonly OrderService orderService = new OrderService();


        public void ShowAllOrders()
        {
            List<OrderItem> orderList = orderService.GetAll();
            var table = new ConsoleTable("Id", "Date and time", "Menu items");
            foreach (OrderItem order in orderList)
            {
                table.AddRow(order.Id, order.DateTime, order.MenuItems);
            }
            Console.WriteLine(table);
        }
        public void AddOrder()
        {
            OrderItem order = new OrderItem();
            Console.WriteLine("Enter menu item Id's. To separate Id's with spaces.");
            string line = Console.ReadLine();
            while (!CheckIfiDsValid(line))
            {
                Console.WriteLine("Invalid ID's. Try again.");
                line = Console.ReadLine();
            }
            order.MenuItems = line;
            order.DateTime = DateTime.Now;
            Console.WriteLine(orderService.Create(order));
            EndFunction();
        }
        public void RemoveOrder()
        {
            ShowAllOrders();
            Console.WriteLine("Enter ID of order to remove.");
            string line = Console.ReadLine();
            long id;
            if (long.TryParse(line, out id))
            {
                id = Convert.ToInt64(line);
            }
            else
            {
                while (!long.TryParse(line, out id))
                {
                    Console.WriteLine("Invalid number. Try again.");
                    line = Console.ReadLine();
                }
                id = Convert.ToInt64(line);
            }
            if (orderService.Remove(id))
            {
                Console.WriteLine("Order removed sucessfully.");
                EndFunction();
            }
            else
            {
                Console.WriteLine("Could not remove order...");
                EndFunction();
            }
        }
    }
}
