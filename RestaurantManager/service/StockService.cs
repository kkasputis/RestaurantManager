using RestaurantManager.entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.service
{
    class StockService : ICrud<Stock>
    {
        CsvService csvService = new CsvService();
        public List<Stock> getAll()
        {
            return csvService.readStockFile();
        }

        public string create(Stock stock)
        {
            List<Stock> stockItems;
            string response;
            stockItems = csvService.readStockFile();
            int id = stockItems.Count + 1;
            while (stockItems.Any(x => x.Id == id))
            {
                id++;
            }
            stock.Id = id;
            if (csvService.writeNewStock(stock)) { response = "New stock item created successfully."; }
            else { response = "Failded to create new stock item."; }
            return response;
        }

        public bool remove(long id)
        {
            return csvService.removeStock(id);
        }

        public bool edit(Stock item)
        {
            return csvService.editStock(item);
        }
    }
}
