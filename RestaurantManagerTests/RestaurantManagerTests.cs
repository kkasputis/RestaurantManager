using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.entity;
using RestaurantManager.service;
using System;
using System.Collections.Generic;
using System.IO;

namespace RestaurantManagerTests
{
    [TestClass]
    public class RestaurantManagerTests
    {
        readonly MockItems mockItems = new MockItems();
        readonly StockService stockService = new StockService();
        readonly OrderService orderService = new OrderService();
        readonly MenuService menuService = new MenuService();
        [TestMethod]
        public void TestStock()
        {
            File.Delete(@"stock.csv");
            Stock stock = mockItems.StockItem1();
            string result = stockService.Create(stock);
            Assert.AreEqual("New stock item created successfully.", result);
            stock.PortionCount = 5;
            Assert.AreEqual(1, stock.Id);
            bool boolResult = stockService.Edit(stock);
            Assert.AreEqual(true, boolResult);
            List<Stock> stockList = stockService.GetAll();
            Assert.AreEqual(1, stockList.Count);
            Assert.AreEqual(5, stockList[0].PortionCount);
            stockService.Remove(stockList[0].Id);
            stockList = stockService.GetAll();
            Assert.AreEqual(0, stockList.Count);
         
            

        }
        [TestMethod]
        public void TestMenuItem()
        {
            File.Delete(@"menu.csv");
            MenuItem menu = mockItems.MenuItem();
            string result = menuService.Create(menu);
            Assert.AreEqual("Menu item created successfully.", result);
            menu.Products = "1 2";
            Assert.AreEqual(1, menu.Id);
            bool boolResult = menuService.Edit(menu);
            Assert.AreEqual(true, boolResult);
            List<MenuItem> menuList = menuService.GetAll();
            Assert.AreEqual(1, menuList.Count);
            Assert.AreEqual("1 2", menuList[0].Products);
            menuService.Remove(menuList[0].Id);
            menuList = menuService.GetAll();
            Assert.AreEqual(0, menuList.Count);
       

        }
        [TestMethod]
        public void TestOrder()
        {
            File.Delete(@"order.csv");
            File.Delete(@"menu.csv");
            File.Delete(@"stock.csv");
            Stock stock = mockItems.StockItem1();
            stockService.Create(stock);
            stock = mockItems.StockItem2();
            stockService.Create(stock);
            MenuItem menu = mockItems.MenuItem();
            menuService.Create(menu);
            menu.Products = "1 2";
            OrderItem order = mockItems.OrderItem();
            string result = orderService.Create(order);
            Assert.AreEqual("New order created successfully.", result);
            order.MenuItems = "1 2";
            result = orderService.Create(order);
            Assert.AreEqual("Could not find menu item with id 2", result);
            menu.Products = "1 2";
            menuService.Create(menu);
            result = orderService.Create(order);
            Assert.AreEqual("Not not enought of Potato for Apple juice in this order", result);
            List<OrderItem> orderList = orderService.GetAll();
            Assert.AreEqual(1, orderList.Count);
            bool boolResult = orderService.Remove(orderList[0].Id);
            Assert.AreEqual(true, boolResult);
            orderList = orderService.GetAll();
            Assert.AreEqual(0, orderList.Count);


        }
    }
    class MockItems {
        public Stock StockItem1()
        {
            Stock stock = new Stock();
            stock.Name = "Apple";
            stock.Unit = "kg";
            stock.PortionCount = 10;
            stock.PortionSize = 1;
            return stock;
        }
        public Stock StockItem2()
        {
            Stock stock = new Stock();
            stock.Name = "Potato";
            stock.Unit = "kg";
            stock.PortionCount = 1;
            stock.PortionSize = 2;
            return stock;
        }
        public MenuItem MenuItem()
        {
            MenuItem menu = new MenuItem();
            menu.Name = "Apple juice";
            menu.Products = "1";
            return menu;
        }

        public OrderItem OrderItem()
        {
            OrderItem order = new OrderItem();
            order.DateTime = DateTime.Now;
            order.MenuItems = "1";
            return order;
        }
        }
}
