using System;

namespace ShopApp.Dal
{
    public class ProductTableViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int StockCount { get; set; }

        public DateTime? AddedOn { get; set; }

        public string CategoryName { get; set; }
    }
}