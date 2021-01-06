using RestaurantManager.entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.service
{
    class StockService : ICrud<Stock>
    {
        readonly CsvService csvService = new CsvService();
        public List<Stock> GetAll()
        {
            return csvService.ReadStockFile();
        }

        public string Create(Stock stock)
        {
            List<Stock> stockItems;
            string response;
            stockItems = csvService.ReadStockFile();
            int id = stockItems.Count + 1;
            while (stockItems.Any(x => x.Id == id))
            {
                id++;
            }
            stock.Id = id;
            if (csvService.WriteNewStock(stock)) { response = "New stock item created successfully."; }
            else { response = "Failded to create new stock item."; }
            return response;
        }

        public bool Remove(long id)
        {
            return csvService.RemoveStock(id);
        }

        public bool Edit(Stock item)
        {
            return csvService.EditStock(item);
        }
    }
}
