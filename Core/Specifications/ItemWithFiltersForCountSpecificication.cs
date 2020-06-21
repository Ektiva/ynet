using Core.Entities;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ItemWithFiltersForCountSpecificication : BaseSpecification<Item>
    {
        public ItemWithFiltersForCountSpecificication(ItemSpecParams itemParams)
            : base(x =>
                (string.IsNullOrEmpty(itemParams.Search) || x.Name.ToLower().Contains(itemParams.Search)) &&
                (itemParams.BrandId.Count < 1 || itemParams.BrandId.Contains(x.ProductBrandId)) &&              
                ((itemParams.SubCategory.Count < 1) || itemParams.SubCategory.Contains(x.CategoryId))
                /*(!itemParams.BrandId.HasValue || x.ProductBrandId == itemParams.BrandId) &&
                (!itemParams.CategoryId.HasValue || x.CategoryId == itemParams.CategoryId) &&*//*||
                (!itemParams.SubCategoryId0.HasValue || x.CategoryId == itemParams.SubCategoryId0) ||
                (!itemParams.SubCategoryId1.HasValue || x.CategoryId == itemParams.SubCategoryId1) ||
                (!itemParams.SubCategoryId2.HasValue || x.CategoryId == itemParams.SubCategoryId2) ||
                (!itemParams.SubCategoryId3.HasValue || x.CategoryId == itemParams.SubCategoryId3) ||
                (!itemParams.SubCategoryId4.HasValue || x.CategoryId == itemParams.SubCategoryId4) ||
                (!itemParams.SubCategoryId5.HasValue || x.CategoryId == itemParams.SubCategoryId5) ||
                (!itemParams.SubCategoryId6.HasValue || x.CategoryId == itemParams.SubCategoryId6) ||
                (!itemParams.SubCategoryId7.HasValue || x.CategoryId == itemParams.SubCategoryId7) ||
                (!itemParams.SubCategoryId8.HasValue || x.CategoryId == itemParams.SubCategoryId8) ||
                (!itemParams.SubCategoryId9.HasValue || x.CategoryId == itemParams.SubCategoryId9) ||
                (!itemParams.SubCategoryId10.HasValue || x.CategoryId == itemParams.SubCategoryId10) ||
                (!itemParams.SubCategoryId11.HasValue || x.CategoryId == itemParams.SubCategoryId11) ||
                (!itemParams.SubCategoryId12.HasValue || x.CategoryId == itemParams.SubCategoryId12) ||
                (!itemParams.SubCategoryId13.HasValue || x.CategoryId == itemParams.SubCategoryId13) ||
                (!itemParams.SubCategoryId14.HasValue || x.CategoryId == itemParams.SubCategoryId14) ||
                (!itemParams.SubCategoryId15.HasValue || x.CategoryId == itemParams.SubCategoryId15) ||
                (!itemParams.SubCategoryId16.HasValue || x.CategoryId == itemParams.SubCategoryId16) ||
                (!itemParams.SubCategoryId17.HasValue || x.CategoryId == itemParams.SubCategoryId17) ||
                (!itemParams.SubCategoryId18.HasValue || x.CategoryId == itemParams.SubCategoryId18) ||
                (!itemParams.SubCategoryId19.HasValue || x.CategoryId == itemParams.SubCategoryId19) ||
                (!itemParams.SubCategoryId20.HasValue || x.CategoryId == itemParams.SubCategoryId20) ||
                (!itemParams.SubCategoryId21.HasValue || x.CategoryId == itemParams.SubCategoryId21) ||
                (!itemParams.SubCategoryId22.HasValue || x.CategoryId == itemParams.SubCategoryId22) ||
                (!itemParams.SubCategoryId23.HasValue || x.CategoryId == itemParams.SubCategoryId23) ||
                (!itemParams.SubCategoryId24.HasValue || x.CategoryId == itemParams.SubCategoryId24) ||
                (!itemParams.SubCategoryId25.HasValue || x.CategoryId == itemParams.SubCategoryId25) ||
                (!itemParams.SubCategoryId26.HasValue || x.CategoryId == itemParams.SubCategoryId26) ||
                (!itemParams.SubCategoryId27.HasValue || x.CategoryId == itemParams.SubCategoryId27) ||
                (!itemParams.SubCategoryId28.HasValue || x.CategoryId == itemParams.SubCategoryId28) ||
                (!itemParams.SubCategoryId29.HasValue || x.CategoryId == itemParams.SubCategoryId29)*/
            )
        {
        }

        public ItemWithFiltersForCountSpecificication(ItemSpecParams1 itemParams)
            : base(x =>
                (!itemParams.CategoryId.HasValue || x.CategoryId == itemParams.CategoryId)
            )
        {
        }
    }
}