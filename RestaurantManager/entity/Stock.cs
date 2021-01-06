using System.Text;

namespace RestaurantManager.entity
{
    class Stock
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal PortionCount { get; set; }
        public string Unit { get; set; }
        public decimal PortionSize { get; set; }
    }
}
