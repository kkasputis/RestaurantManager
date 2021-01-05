using RestaurantManager.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace RestaurantManager.service
{
    class CsvService
    {
        private readonly string orderDir = "orders.csv";
        private readonly string stockDir = "stock.csv";
        private readonly string menuDir = "menu.csv";


        public List<OrderItem> readOrderFile()
        {
            try
            {
                if (File.Exists(@orderDir))
                {
                    List<OrderItem> orders = File.ReadAllLines(@orderDir)
                                              .Select(v => OrderValues.parseToObject(v))
                                              .ToList();
                    return orders;
                }
                else
                {
                    emptyFile(@orderDir);
                    return new List<OrderItem>();
                }
            }
            catch
            {
                Console.WriteLine("Could not read Order file...");
                return new List<OrderItem>();
            }
        }

        public bool writeNewOrder(OrderItem order)
        {
            if (!File.Exists(@orderDir))
            {
                emptyFile(@orderDir);
            }
            try
            {

                File.AppendAllText(@orderDir, orderObjectToString(order) + Environment.NewLine);
                Console.WriteLine(@orderDir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool removeOrder(long id)
        {
            try
            {
                List<OrderItem> orderItems = readOrderFile().Where(x => x.Id != id).ToList();
                emptyFile(@orderDir);
                foreach (OrderItem orderItem in orderItems)
                {

                    File.AppendAllText(@orderDir, orderObjectToString(orderItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }



        public List<MenuItem> readMenuFile()
        {
            if (File.Exists(@menuDir))
            {
                List<MenuItem> menuItems = File.ReadAllLines(@menuDir)
                                          .Select(v => MenuValues.parseToObject(v))
                                          .ToList();
                return menuItems;
            }
            else
            {
                emptyFile(@menuDir);
                return new List<MenuItem>();
            }
        }

        public bool writeNewMenu(MenuItem menu)
        {
            if (!File.Exists(@menuDir))
            {
                emptyFile(@menuDir);
            }
            try
            {
                File.AppendAllText(@menuDir, menuObjectToString(menu) + Environment.NewLine);
                Console.WriteLine(@menuDir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool editMenuItem(MenuItem menu)

        {
            try
            {
                List<MenuItem> menuItems = readMenuFile();
                MenuItem originalMenu = menuItems.First(x => x.Id == menu.Id);
                int index = menuItems.IndexOf(originalMenu);
                Console.WriteLine(index);
                if (index < 0)
                {
                    Console.WriteLine("Could not find menu item with id " + menu.Id);
                    return false;
                }
                menuItems[index] = menu;

                emptyFile(@menuDir);
                foreach (MenuItem menuItem in menuItems)
                {
                    File.AppendAllText(@menuDir, menuObjectToString(menuItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }

        public bool removeMenuItem(long id)
        {
            try
            {
                List<MenuItem> menuItems = readMenuFile().Where(x => x.Id != id).ToList();
                emptyFile(@menuDir);
                foreach (MenuItem menuItem in menuItems)
                {
                    File.AppendAllText(@menuDir, menuObjectToString(menuItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }


        public List<Stock> readStockFile()
        {
            if (File.Exists(@stockDir))
            {
                List<Stock> stockItems = File.ReadAllLines(@stockDir)
                                          .Select(v => StockValues.parseToObject(v))
                                          .ToList();
                return stockItems;
            }

            else
            {
                emptyFile(@stockDir);
                return new List<Stock>();
            }
        }
        public bool writeNewStock(Stock stock)
        {
            if (!File.Exists(@stockDir))
            {
                emptyFile(@stockDir);
            }
            try
            {
                File.AppendAllText(@stockDir, stockObjectToString(stock) + Environment.NewLine);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool removeStock(long id)
        {
            try
            {
                List<Stock> stockItems = readStockFile().Where(x => x.Id != id).ToList();
                emptyFile(@stockDir);
                foreach (Stock stockItem in stockItems)
                {
                    File.AppendAllText(@stockDir, stockObjectToString(stockItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }
        public bool editStock(Stock stock)
        {
            try
            {
                List<Stock> stockItems = readStockFile();
                Stock originalStock = stockItems.First(x => x.Id == stock.Id);
                int index = stockItems.IndexOf(originalStock);
                if (index < 0)
                {
                    Console.WriteLine("Cound not find stock item with id " + stock.Id);
                    return false;

                }
                stockItems[index] = stock;

                emptyFile(@stockDir);
                foreach (Stock stockItem in stockItems)
                {
                    File.AppendAllText(@stockDir, stockObjectToString(stockItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }

        }

        public void editStock(List<Stock> stock)
        {
            emptyFile(@stockDir);
            foreach (Stock stockItem in stock)
            {
                File.AppendAllText(@stockDir, stockObjectToString(stockItem) + Environment.NewLine);
            }
        }


        public string stockObjectToString(Stock stock)
        {
            return stock.Id + "," + stock.Name + "," + stock.PortionCount + "," + stock.Unit + "," + stock.PortionSize;
        }
        public string orderObjectToString(OrderItem order)
        {
            return order.Id + "," + order.dateTime + "," + order.menuItems;
        }
        public string menuObjectToString(MenuItem menu)
        {
            return menu.Id + "," + menu.Name + "," + menu.Products;
        }

        public void emptyFile(String dir)
        {
            StreamWriter strm = File.CreateText(@dir);
            strm.Flush();
            strm.Close();
        }
        class OrderValues
        {
            public static OrderItem parseToObject(string csvLine)
            {
                string[] values = csvLine.Split(',');
                entity.OrderItem order = new entity.OrderItem();
                order.Id = Convert.ToInt64(values[0]);
                order.dateTime = Convert.ToDateTime(values[1]);
                order.menuItems = Convert.ToString(values[2]);

                return order;
            }
        }

        class StockValues
        {



            public static Stock parseToObject(string csvLine)
            {
                string[] values = csvLine.Split(',');
                Stock stock = new Stock();
                stock.Id = Convert.ToInt64(values[0]);
                stock.Name = Convert.ToString(values[1]);
                stock.PortionCount = Convert.ToDecimal(values[2]);
                stock.Unit = Convert.ToString(values[3]);
                stock.PortionSize = Convert.ToDecimal(values[4]);

                return stock;
            }
        }

        class MenuValues
        {


            public static MenuItem parseToObject(string csvLine)
            {
                string[] values = csvLine.Split(',');
                MenuItem menu = new MenuItem();
                menu.Id = Convert.ToInt64(values[0]);
                menu.Name = Convert.ToString(values[1]);
                menu.Products = Convert.ToString(values[2]);


                return menu;
            }
        }
    }

}

