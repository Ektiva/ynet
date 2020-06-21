using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
