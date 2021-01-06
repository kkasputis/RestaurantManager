using ConsoleTables;
using RestaurantManager.entity;
using RestaurantManager.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.consoleFunctions
{
    class MenuConsoleFunctions : MiscConsoleFunctions
    {
        readonly MenuService menuService = new MenuService();
        public void ShowAllMenu()
        {
            List<MenuItem> menuList = menuService.GetAll();
            var table = new ConsoleTable("Id", "Name", "Products");
            foreach (MenuItem menu in menuList)
            {
                table.AddRow(menu.Id, menu.Name, menu.Products);
            }
            Console.WriteLine(table);
        }

        public void AddMenuItem()
        {
            MenuItem menu = new MenuItem();
            Console.WriteLine("Enter menu item name");
            menu.Name = Console.ReadLine();
            Console.WriteLine("Enter menu item products Id's. Seperate Id's by spaces.");
            string line = Console.ReadLine();
            while (!CheckIfiDsValid(line))
            {
                Console.WriteLine("Invalid ID's. Try again.");
                line = Console.ReadLine();
            }
            menu.Products = line;
            Console.WriteLine(menuService.Create(menu));
            EndFunction();
        }

        public void EditMenuItem()
        {
            ShowAllMenu();
            List<MenuItem> menuList = menuService.GetAll();
            Console.WriteLine("Enter menu ID you want to edit");
            string line = Console.ReadLine();
            MenuItem menu = null;
            if (long.TryParse(line, out long result))
            {
                menu = menuList.FirstOrDefault(x => x.Id == Convert.ToInt64(line));
            }
            while (menu == null)
            {
                Console.WriteLine("Invalid ID. Try again. Or type \"menu\" to go back to menu.");
                line = Console.ReadLine();
                if (line.Equals("menu"))
                {
                    appMenu();
                    return;
                }
                if (long.TryParse(line, out result))
                {
                    menu = menuList.FirstOrDefault(x => x.Id == Convert.ToInt64(line));
                }
            }
            Console.WriteLine("Enter new name for menu item. If you do not want to edit name press Enter.");
            line = Console.ReadLine();
            if (!line.Equals(""))
            {
                menu.Name = line;
            }
            Console.WriteLine("Enter new products for menu item. If you do not want to edit products press Enter.");
            line = Console.ReadLine();
            if (!line.Equals(""))
            {
                while (!CheckIfiDsValid(line))
                {
                    Console.WriteLine("Invalid ID's. Try again.");
                    line = Console.ReadLine();
                }
                menu.Products = line;
            }
            if (menuService.Edit(menu))
            {
                Console.WriteLine("Menu item edited successfully.");
                EndFunction();
            }
            else
            {
                Console.WriteLine("Failed to edit menu item..");
                EndFunction();
            }
        }
        public void RemoveMenuItem()
        {
            ShowAllMenu();
            Console.WriteLine("Enter ID of menu item to remove.");
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
            if (menuService.Remove(id))
            {
                Console.WriteLine("Menu item removed sucessfully.");
                EndFunction();
            }
            else
            {
                Console.WriteLine("Could not remove menu item...");
                EndFunction();
            }
        }
    }
}
