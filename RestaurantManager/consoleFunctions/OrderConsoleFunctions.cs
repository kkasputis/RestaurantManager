using ConsoleTables;
using RestaurantManager.entity;
using RestaurantManager.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.consoleFunctions
{
    class OrderConsoleFunctions : MiscConsolFunctins
    {
        OrderService orderService = new OrderService();


        public void showAllOrders()
        {
            List<OrderItem> orderList = orderService.getAll();
            var table = new ConsoleTable("Id", "Date and time", "Menu items");
            foreach (OrderItem order in orderList)
            {
                table.AddRow(order.Id, order.dateTime, order.menuItems);
            }
            Console.WriteLine(table);
        }
        public void addOrder()
        {
            OrderItem order = new OrderItem();
            Console.WriteLine("Enter menu item Id's. To separate Id's with spaces.");
            string line = Console.ReadLine();
            while (!checkIfiDsValid(line))
            {
                Console.WriteLine("Invalid ID's. Try again.");
                line = Console.ReadLine();
            }
            order.menuItems = line;
            order.dateTime = DateTime.Now;
            Console.WriteLine(orderService.create(order));
            endFunction();
        }
        public void removeOrder()
        {
            showAllOrders();
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
            if (orderService.remove(id))
            {
                Console.WriteLine("Order removed sucessfully.");
                endFunction();
            }
            else
            {
                Console.WriteLine("Could not remove order...");
                endFunction();
            }
        }
    }
}
