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
        private readonly string OrderDir = "orders.csv";
        private readonly string StockDir = "stock.csv";
        private readonly string MenuDir = "menu.csv";


        public List<OrderItem> ReadOrderFile()
        {
            try
            {
                if (File.Exists(@OrderDir))
                {
                    List<OrderItem> orders = File.ReadAllLines(@OrderDir)
                                              .Select(v => OrderValues.ParseToObject(v))
                                              .ToList();
                    return orders;
                }
                else
                {
                    EmptyFile(@OrderDir);
                    return new List<OrderItem>();
                }
            }
            catch
            {
                Console.WriteLine("Could not read Order file...");
                return new List<OrderItem>();
            }
        }

        public bool WriteNewOrder(OrderItem order)
        {
            if (!File.Exists(@OrderDir))
            {
                EmptyFile(@OrderDir);
            }
            try
            {

                File.AppendAllText(@OrderDir, orderObjectToString(order) + Environment.NewLine);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveOrder(long id)
        {
            try
            {
                List<OrderItem> orderItems = ReadOrderFile().Where(x => x.Id != id).ToList();
                EmptyFile(@OrderDir);
                foreach (OrderItem orderItem in orderItems)
                {

                    File.AppendAllText(@OrderDir, orderObjectToString(orderItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }



        public List<MenuItem> ReadMenuFile()
        {
            if (File.Exists(@MenuDir))
            {
                List<MenuItem> menuItems = File.ReadAllLines(@MenuDir)
                                          .Select(v => MenuValues.ParseToObject(v))
                                          .ToList();
                return menuItems;
            }
            else
            {
                EmptyFile(@MenuDir);
                return new List<MenuItem>();
            }
        }

        public bool WriteNewMenu(MenuItem menu)
        {
            if (!File.Exists(@MenuDir))
            {
                EmptyFile(@MenuDir);
            }
            try
            {
                File.AppendAllText(@MenuDir, menuObjectToString(menu) + Environment.NewLine);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditMenuItem(MenuItem menu)

        {
            try
            {
                List<MenuItem> menuItems = ReadMenuFile();
                MenuItem originalMenu = menuItems.First(x => x.Id == menu.Id);
                int index = menuItems.IndexOf(originalMenu);
                if (index < 0)
                {
                    Console.WriteLine("Could not find menu item with id " + menu.Id);
                    return false;
                }
                menuItems[index] = menu;

                EmptyFile(@MenuDir);
                foreach (MenuItem menuItem in menuItems)
                {
                    File.AppendAllText(@MenuDir, menuObjectToString(menuItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }

        public bool RemoveMenuItem(long id)
        {
            try
            {
                List<MenuItem> menuItems = ReadMenuFile().Where(x => x.Id != id).ToList();
                EmptyFile(@MenuDir);
                foreach (MenuItem menuItem in menuItems)
                {
                    File.AppendAllText(@MenuDir, menuObjectToString(menuItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }


        public List<Stock> ReadStockFile()
        {
            if (File.Exists(@StockDir))
            {
                List<Stock> stockItems = File.ReadAllLines(@StockDir)
                                          .Select(v => StockValues.ParseToObject(v))
                                          .ToList();
                return stockItems;
            }

            else
            {
                EmptyFile(@StockDir);
                return new List<Stock>();
            }
        }
        public bool WriteNewStock(Stock stock)
        {
            if (!File.Exists(@StockDir))
            {
                EmptyFile(@StockDir);
            }
            try
            {
                File.AppendAllText(@StockDir, StockObjectToString(stock) + Environment.NewLine);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveStock(long id)
        {
            try
            {
                List<Stock> stockItems = ReadStockFile().Where(x => x.Id != id).ToList();
                EmptyFile(@StockDir);
                foreach (Stock stockItem in stockItems)
                {
                    File.AppendAllText(@StockDir, StockObjectToString(stockItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }
        }
        public bool EditStock(Stock stock)
        {
            try
            {
                List<Stock> stockItems = ReadStockFile();
                Stock originalStock = stockItems.First(x => x.Id == stock.Id);
                int index = stockItems.IndexOf(originalStock);
                if (index < 0)
                {
                    Console.WriteLine("Cound not find stock item with id " + stock.Id);
                    return false;

                }
                stockItems[index] = stock;

                EmptyFile(@StockDir);
                foreach (Stock stockItem in stockItems)
                {
                    File.AppendAllText(@StockDir, StockObjectToString(stockItem) + Environment.NewLine);
                }
                return true;
            }
            catch { return false; }

        }

        public void EditStock(List<Stock> stock)
        {
            EmptyFile(@StockDir);
            foreach (Stock stockItem in stock)
            {
                File.AppendAllText(@StockDir, StockObjectToString(stockItem) + Environment.NewLine);
            }
        }


        public string StockObjectToString(Stock stock)
        {
            return stock.Id + "," + stock.Name + "," + stock.PortionCount + "," + stock.Unit + "," + stock.PortionSize;
        }
        public string orderObjectToString(OrderItem order)
        {
            return order.Id + "," + order.DateTime + "," + order.MenuItems;
        }
        public string menuObjectToString(MenuItem menu)
        {
            return menu.Id + "," + menu.Name + "," + menu.Products;
        }

        public void EmptyFile(String dir)
        {
            StreamWriter strm = File.CreateText(@dir);
            strm.Flush();
            strm.Close();
        }
        class OrderValues
        {
            public static OrderItem ParseToObject(string csvLine)
            {
                string[] values = csvLine.Split(',');
                entity.OrderItem order = new entity.OrderItem();
                order.Id = Convert.ToInt64(values[0]);
                order.DateTime = Convert.ToDateTime(values[1]);
                order.MenuItems = Convert.ToString(values[2]);

                return order;
            }
        }

        class StockValues
        {



            public static Stock ParseToObject(string csvLine)
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


            public static MenuItem ParseToObject(string csvLine)
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

