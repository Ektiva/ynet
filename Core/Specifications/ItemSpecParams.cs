using Microsoft.SqlServer.Types;
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

        /*
        region CategoryIds
        public int? SubCategoryId0 { get; set; }
        public int? SubCategoryId1 { get; set; }
        public int? SubCategoryId2 { get; set; }
        public int? SubCategoryId3 { get; set; }
        public int? SubCategoryId4 { get; set; }
        public int? SubCategoryId5 { get; set; }
        public int? SubCategoryId6 { get; set; }
        public int? SubCategoryId7 { get; set; }
        public int? SubCategoryId8 { get; set; }
        public int? SubCategoryId9 { get; set; }
        public int? SubCategoryId10 { get; set; }
        public int? SubCategoryId11 { get; set; }
        public int? SubCategoryId12 { get; set; }
        public int? SubCategoryId13 { get; set; }
        public int? SubCategoryId14 { get; set; }
        public int? SubCategoryId15 { get; set; }
        public int? SubCategoryId16 { get; set; }
        public int? SubCategoryId17 { get; set; }
        public int? SubCategoryId18 { get; set; }
        public int? SubCategoryId19 { get; set; }
        public int? SubCategoryId20 { get; set; }
        public int? SubCategoryId21 { get; set; }
        public int? SubCategoryId22 { get; set; }
        public int? SubCategoryId23 { get; set; }
        public int? SubCategoryId24 { get; set; }
        public int? SubCategoryId25 { get; set; }
        public int? SubCategoryId26 { get; set; }
        public int? SubCategoryId27 { get; set; }
        public int? SubCategoryId28 { get; set; }
        public int? SubCategoryId29 { get; set; }


        //public int?[] ColorId { get; set; }
        //public int?[] SizeId { get; set; }
        //public int?[] ImagesId { get; set; }
        #endregion
        */
        public string Sort { get; set; }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
