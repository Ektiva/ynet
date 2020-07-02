﻿using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ItemSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 100000;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public List<int?> BrandId { get; set; }
        public int? CategoryId { get; set; }
        //public SqlHierarchyId CategoryIdNode { get; set; }
        public List<int?> SubCategory { get; set; }

        public string Sort { get; set; }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}