using ConsoleTables;
using RestaurantManager.entity;
using RestaurantManager.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.consoleFunctions
{
    class StockConsoleFunctions : MiscConsolFunctins
    {
        StockService stockService = new StockService();
   
        public void showAllStock()
        {
            List<Stock> stockList = stockService.getAll();
            var table = new ConsoleTable("Id", "Name", "Portion Count", "Unit", "Portion Size");
            foreach (Stock stock in stockList)
            {
                table.AddRow(stock.Id, stock.Name, stock.PortionCount, stock.Unit, stock.PortionSize);
            }
            Console.WriteLine(table);
        }

        public void addStockItem()
        {
            Stock stock = new Stock();
            Console.WriteLine("Enter stock name");
            stock.Name = Console.ReadLine();
            Console.WriteLine("Enter stock units");
            stock.Unit = Console.ReadLine();
            Console.WriteLine("Enter portion count");
            string line = Console.ReadLine();
            decimal portionCount;
            if (decimal.TryParse(line, out portionCount))
            {
                stock.PortionCount = Convert.ToDecimal(line);
            }
            else
            {
                while (!decimal.TryParse(line, out portionCount))
                {
                    Console.WriteLine("Invalid number. Try again.");
                    line = Console.ReadLine();
                }
                stock.PortionCount = Convert.ToDecimal(line);
            }
            Console.WriteLine("Enter portion size");
            line = Console.ReadLine();
            decimal portionSize;
            if (decimal.TryParse(line, out portionSize))
            {
                stock.PortionSize = Convert.ToDecimal(line);
            }
            else
            {
                while (!decimal.TryParse(line, out portionSize))
                {
                    Console.WriteLine("Invalid number. Try again.");
                    line = Console.ReadLine();
                }
                stock.PortionSize = Convert.ToDecimal(line);
            }
            Console.WriteLine(stockService.create(stock));
            endFunction();
        }

        public void removeStockItem()
        {
            showAllStock();
            Console.WriteLine("Enter ID of stock item to remove.");
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
            if (stockService.remove(id))
            {
                Console.WriteLine("Stock removed sucessfully.");
                endFunction();
            }
            else
            {
                Console.WriteLine("Could not remove stock...");
                endFunction();
            }
        }

        public void editStockItem()
        {
            showAllStock();
            List<Stock> stockList = stockService.getAll();
            Console.WriteLine("Enter stock ID you want to edit");
            string line = Console.ReadLine();
            Stock stock = null;
            if (long.TryParse(line, out long result))
            {
                stock = stockList.FirstOrDefault(x => x.Id == Convert.ToInt64(line));
            }
            while (stock == null)
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
                    stock = stockList.FirstOrDefault(x => x.Id == Convert.ToInt64(line));
                }
            }
            Console.WriteLine("Enter new name for stock. If you do not want to edit name press Enter.");
            line = Console.ReadLine();
            if (!line.Equals(""))
            {
                stock.Name = line;
            }
            Console.WriteLine("Enter new units. If you do not want to edit name press Enter.");
            line = Console.ReadLine();
            if (!line.Equals(""))
            {
                stock.Unit = line;
            }
            Console.WriteLine("Enter new portion count. If you do not want to edit name press Enter.");
            line = Console.ReadLine();
            if (!line.Equals(""))
            {
                decimal portionCount;
                if (decimal.TryParse(line, out portionCount))
                {
                    stock.PortionCount = Convert.ToDecimal(line);
                }
                else
                {
                    while (!decimal.TryParse(line, out portionCount))
                    {
                        Console.WriteLine("Invalid number. Try again.");
                        line = Console.ReadLine();
                    }
                    stock.PortionCount = Convert.ToDecimal(line);
                }
            }
            Console.WriteLine("Enter new portion size. If you do not want to edit name press Enter.");
            line = Console.ReadLine();
            if (!line.Equals(""))
            {
                decimal portionSize;
                if (decimal.TryParse(line, out portionSize))
                {
                    stock.PortionSize = Convert.ToDecimal(line);
                }
                else
                {
                    while (!decimal.TryParse(line, out portionSize))
                    {
                        Console.WriteLine("Invalid number. Try again.");
                        line = Console.ReadLine();
                    }
                    stock.PortionSize = Convert.ToDecimal(line);
                }
            }
            if (stockService.edit(stock))
            {
                Console.WriteLine("Stock edited sucessfully.");
                endFunction();
            }
            else
            {
                {
                    Console.WriteLine("Failed to edit stock...");
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    appMenu();
                }
            }
        }
    }
}
