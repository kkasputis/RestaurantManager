using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.entity
{
    class OrderItem
    {


        public long Id { get; set; }
        public DateTime dateTime { get; set; }
        public String menuItems { get; set; }
    }
}
