using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.entity
{
    interface ICrud<T>
    {
        public List<T> GetAll();
        public string Create(T item);
        public Boolean Remove(long id);
 
        public Boolean Edit(T item);

    }
}
