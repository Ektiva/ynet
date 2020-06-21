using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Size : BaseEntity
    {
        public string Name { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
