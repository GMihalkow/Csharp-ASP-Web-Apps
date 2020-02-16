using System;

namespace ShopApp.Dal
{
    public class CategoryTableViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorName { get; set; }
    }
}