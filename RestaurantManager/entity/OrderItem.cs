using System;
using System.Text;

namespace RestaurantManager.entity
{
    class OrderItem
    {


        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public String MenuItems { get; set; }
    }
}
