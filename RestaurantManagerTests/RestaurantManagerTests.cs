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
        MockItems mockItems = new MockItems();
        StockService stockService = new StockService();
        OrderService orderService = new OrderService();
        MenuService menuService = new MenuService();
        [TestMethod]
        public void TestStock()
        {
            File.Delete(@"stock.csv");
            Stock stock = mockItems.stockItem1();
            string result = stockService.create(stock);
            Assert.AreEqual("New stock item created successfully.", result);
            stock.PortionCount = 5;
            Assert.AreEqual(1, stock.Id);
            bool boolResult = stockService.edit(stock);
            Assert.AreEqual(true, boolResult);
            List<Stock> stockList = stockService.getAll();
            Assert.AreEqual(1, stockList.Count);
            Assert.AreEqual(5, stockList[0].PortionCount);
            stockService.remove(stockList[0].Id);
            stockList = stockService.getAll();
            Assert.AreEqual(0, stockList.Count);
         
            

        }
        [TestMethod]
        public void TestMenuItem()
        {
            File.Delete(@"menu.csv");
            MenuItem menu = mockItems.menuItem();
            string result = menuService.create(menu);
            Assert.AreEqual("Menu item created successfully.", result);
            menu.Products = "1 2";
            Assert.AreEqual(1, menu.Id);
            bool boolResult = menuService.edit(menu);
            Assert.AreEqual(true, boolResult);
            List<MenuItem> menuList = menuService.getAll();
            Assert.AreEqual(1, menuList.Count);
            Assert.AreEqual("1 2", menuList[0].Products);
            menuService.remove(menuList[0].Id);
            menuList = menuService.getAll();
            Assert.AreEqual(0, menuList.Count);
       

        }
        [TestMethod]
        public void TestOrder()
        {
            File.Delete(@"order.csv");
            File.Delete(@"menu.csv");
            File.Delete(@"stock.csv");
            Stock stock = mockItems.stockItem1();
            stockService.create(stock);
            stock = mockItems.stockItem2();
            stockService.create(stock);
            MenuItem menu = mockItems.menuItem();
            menuService.create(menu);
            menu.Products = "1 2";
            OrderItem order = mockItems.orderItem();
            string result = orderService.create(order);
            Assert.AreEqual("New order created successfully.", result);
            order.menuItems = "1 2";
            result = orderService.create(order);
            Assert.AreEqual("Could not find menu item with id 2", result);
            menu.Products = "1 2";
            menuService.create(menu);
            result = orderService.create(order);
            Assert.AreEqual("Not not enought of Potato for Apple juice in this order", result);
            List<OrderItem> orderList = orderService.getAll();
            Assert.AreEqual(1, orderList.Count);
            bool boolResult = orderService.remove(orderList[0].Id);
            Assert.AreEqual(true, boolResult);
            orderList = orderService.getAll();
            Assert.AreEqual(0, orderList.Count);


        }
    }
    class MockItems {
        public Stock stockItem1()
        {
            Stock stock = new Stock();
            stock.Name = "Apple";
            stock.Unit = "kg";
            stock.PortionCount = 10;
            stock.PortionSize = 1;
            return stock;
        }
        public Stock stockItem2()
        {
            Stock stock = new Stock();
            stock.Name = "Potato";
            stock.Unit = "kg";
            stock.PortionCount = 1;
            stock.PortionSize = 2;
            return stock;
        }
        public MenuItem menuItem()
        {
            MenuItem menu = new MenuItem();
            menu.Name = "Apple juice";
            menu.Products = "1";
            return menu;
        }

        public OrderItem orderItem()
        {
            OrderItem order = new OrderItem();
            order.dateTime = DateTime.Now;
            order.menuItems = "1";
            return order;
        }
        }
}
