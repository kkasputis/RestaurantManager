using RestaurantManager.entity;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManager.service
{
    class MenuService : ICrud<MenuItem>

    {
        readonly CsvService csvService = new CsvService();
        public string Create(MenuItem item)
        {
            List<MenuItem> menuItems;
            string response;
            menuItems = csvService.ReadMenuFile();
            int id = menuItems.Count + 1;
            while (menuItems.Any(x => x.Id == id))
            {
                id++;
            }
            item.Id = id;
            if (csvService.WriteNewMenu(item)) { response = "Menu item created successfully."; }
            else { response = "Faild to create new item."; }
            return response;
        }

        public bool Edit(MenuItem item)
        {
            return csvService.EditMenuItem(item);
        }

        public List<MenuItem> GetAll()
        {
            return csvService.ReadMenuFile();
        }

        public bool Remove(long id)
        {
            return csvService.RemoveMenuItem(id);
        }
    }
}
