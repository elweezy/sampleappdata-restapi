using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAppData.DAL.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
