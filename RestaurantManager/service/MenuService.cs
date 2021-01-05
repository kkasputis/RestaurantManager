using RestaurantManager.entity;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManager.service
{
    class MenuService : ICrud<MenuItem>

    {
        CsvService csvService = new CsvService();
        public string create(MenuItem item)
        {
            List<MenuItem> menuItems;
            string response;
            menuItems = csvService.readMenuFile();
            int id = menuItems.Count + 1;
            while (menuItems.Any(x => x.Id == id))
            {
                id++;
            }
            item.Id = id;
            if (csvService.writeNewMenu(item)) { response = "Menu item created successfully."; }
            else { response = "Faild to create new item."; }
            return response;
        }

        public bool edit(MenuItem item)
        {
            return csvService.editMenuItem(item);
        }

        public List<MenuItem> getAll()
        {
            return csvService.readMenuFile();
        }

        public bool remove(long id)
        {
            return csvService.removeMenuItem(id);
        }
    }
}
