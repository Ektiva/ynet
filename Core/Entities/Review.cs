using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Review : BaseEntity
    {
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string ReviewerPhoto { get; set; }
        public string ReviewMessage { get; set; }
        public DateTimeOffset ReviewDate { get; set; } = DateTimeOffset.Now;
        public int rate { get; set; }
        public int ItemId { get; set; }

    }
}
