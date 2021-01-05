using RestaurantManager.consoleFunctions;
using System;


namespace RestaurantManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Restaurant Manger!");
            App app = new App();
            app.appMenu();

        }

    }
    class App
    {
        MenuConsoleFunctions menuConsoleFunctions = new MenuConsoleFunctions();
        StockConsoleFunctions stockFunctions = new StockConsoleFunctions();
        OrderConsoleFunctions orderFunctions = new OrderConsoleFunctions();
        public void appMenu()
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("Stock:");
            Console.WriteLine("\t1 - Show all stock items");
            Console.WriteLine("\t2 - Add new stock item");
            Console.WriteLine("\t3 - Edit stock item");
            Console.WriteLine("\t4 - Remove stock item");
            Console.WriteLine("Menu:");
            Console.WriteLine("\t5 - Show all menu items");
            Console.WriteLine("\t6 - Add new menu item");
            Console.WriteLine("\t7 - Edit menu item");
            Console.WriteLine("\t8 - Remove menu item");
            Console.WriteLine("Order:");
            Console.WriteLine("\t9 - Show all orders");
            Console.WriteLine("\t10 - Add new order");
            Console.WriteLine("\t11 - Remove order");
            Console.WriteLine("");
            Console.WriteLine("\texit - Close program");
            Console.Write("Your option? ");
            bool goodChoise = false;

            while (!goodChoise)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        goodChoise = true;
                        Console.WriteLine("All stock items:");
                        stockFunctions.showAllStock();
                        endFunction();
                        break;
                    case "2":
                        goodChoise = true;
                        Console.WriteLine("Add new stock item");
                        stockFunctions.addStockItem();
                        break;
                    case "3":
                        goodChoise = true;
                        Console.WriteLine("Edit stock item");
                        stockFunctions.editStockItem();
                        break;
                    case "4":
                        goodChoise = true;
                        Console.WriteLine("Remove stock item");
                        stockFunctions.removeStockItem();
                        break;
                    case "5":
                        goodChoise = true;
                        Console.WriteLine("All menu items");
                        menuConsoleFunctions.showAllMenu();
                        endFunction();
                        break;
                    case "6":
                        goodChoise = true;
                        Console.WriteLine("Add new menu item");
                        menuConsoleFunctions.addMenuItem();
                        break;
                    case "7":
                        goodChoise = true;
                        Console.WriteLine("Edit menu item");
                        menuConsoleFunctions.editMenuItem();
                        break;
                    case "8":
                        goodChoise = true;
                        Console.WriteLine("Remove menu item");
                        menuConsoleFunctions.removeMenuItem();
                        break;
                    case "9":
                        goodChoise = true;
                        Console.WriteLine("Show all orders");
                        orderFunctions.showAllOrders();
                        endFunction();
                        break;
                    case "10":
                        goodChoise = true;
                        Console.WriteLine("Add new order");
                        orderFunctions.addOrder();
                        break;
                    case "11":
                        goodChoise = true;
                        Console.WriteLine("Remove order");
                        orderFunctions.removeOrder();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Invalid option... Try again.");
                        break;
                }
            }
        }


        public void endFunction()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            appMenu();
        }
    }
    class MiscConsolFunctins
    {
        public bool checkIfiDsValid(string line)
        {
            bool response = true;
            long number;
            string[] ids = line.Split(" ");
            foreach (string id in ids)
            {
                if (!long.TryParse(id, out number)) { response = false; }
            }
            return response;
        }

        public void endFunction()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            App app = new App();
            app.appMenu();
        }
        public void appMenu()
        {
            App app = new App();
            app.appMenu();
        }
    }
}
