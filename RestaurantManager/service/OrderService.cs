using RestaurantManager.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.service
{
    class OrderService : ICrud<OrderItem>

    {
        readonly CsvService csvService = new CsvService();
        public List<OrderItem> GetAll()
        {
            return csvService.ReadOrderFile();
        }

        public string Create(OrderItem order)
        {
            string stockResponse = CheckStock(order.MenuItems);
            if (stockResponse.Equals("true"))
            {
                List<OrderItem> orderItems;
                orderItems = csvService.ReadOrderFile();
                int id = orderItems.Count + 1;

                while (orderItems.Any(x => x.Id == id))
                {
                    id++;
                }
                order.Id = id;
                if (csvService.WriteNewOrder(order))
                {
                    return "New order created successfully.";
                }
                else { return "Failed to write new order..."; }
            }
            else
            {
                return stockResponse;
            }
        }

        public bool Remove(long id)
        {
            return csvService.RemoveOrder(id);
        }
        public bool Edit(OrderItem item)
        {
            throw new NotImplementedException();
        }
        public string CheckStock(String ingredients)
        {
            string[] menuIds = ingredients.Split(" ");
            List<Stock> stockList = csvService.ReadStockFile();
            List<MenuItem> menuList = csvService.ReadMenuFile();
            foreach (string menuId in menuIds)
            {
                MenuItem menu = menuList.FirstOrDefault(x => x.Id == Convert.ToInt64(menuId));
                if (menu == null)
                {
                    return "Could not find menu item with id " + menuId;
                }
                string[] stockIds = menu.Products.Split(" ");

                foreach (string id in stockIds)
                {
                    Stock stock = stockList.FirstOrDefault(x => x.Id == Convert.ToInt64(id));


                    if (stock == null)
                    {
                        return "Could not find stock item with id " + id;
                    }
                    int index = stockList.IndexOf(stock);
                    stock.PortionCount = stock.PortionCount - stock.PortionSize;
                    if (stock.PortionCount < 0)
                    {

                        return "Not not enought of " + stock.Name + " for " + menu.Name + " in this order";

                    }
                    else
                    {
                        stockList[index] = stock;
                    }
                }

            }
            csvService.EditStock(stockList);
            return "true";
        }

    }
}

