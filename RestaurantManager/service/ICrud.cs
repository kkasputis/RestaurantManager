using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.entity
{
    interface ICrud<T>
    {
        public List<T> getAll();
        public string create(T item);
        public Boolean remove(long id);
 
        public Boolean edit(T item);

    }
}
