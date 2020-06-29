using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Types;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IGenericRepository<Item> _itemsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Color> _colorsRepo;
        private readonly IGenericRepository<Size> _sizesRepo;
        private readonly IGenericRepository<Image> _imagesRepo;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;
        private string ApiUrl = "https://localhost:44373/";
        public  ItemsController(IMapper mapper,
            IGenericRepository<Item> itemsRepo,
            IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<Category> categoryRepo,
            IGenericRepository<Color> colorsRepo,
            IGenericRepository<Size> sizesRepo,
            IGenericRepository<Image> imagesRepo,
            StoreContext context)
        {
            _mapper = mapper;
            _itemsRepo = itemsRepo;
            _productBrandRepo = productBrandRepo;
            _categoryRepo = categoryRepo;
            _colorsRepo = colorsRepo;
            _sizesRepo = sizesRepo;
            _imagesRepo = imagesRepo;
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ItemToReturnDto>>> GetItems(
             [FromQuery] ItemSpecParams itemParams)
        {
            
            //Get the category choose
            if (itemParams.CategoryId == null)
            {
                itemParams.CategoryId = 0/*Root*/;
            }
            if (itemParams.BrandId == null)
            {
                //Initialize the BrandId List
                itemParams.BrandId = new List<int?>();
            }
            itemParams.SubCategory = new List<int?>();
            itemParams.SubCategory.Add(itemParams.CategoryId);
            var cat = await _context.Categorys.Where(x => x.Id == itemParams.CategoryId).FirstOrDefaultAsync();
            SqlHierarchyId catNode = HierarchyExtensions.ToSqlHierarchyId(cat.Node);

            //Get all subCategories
            var allCategories = await _context.Categorys.ToListAsync();
            var subCategorys = new List<Category>();
            // Select all subcategories
            foreach (var cats in allCategories)
            {
                SqlHierarchyId node = HierarchyExtensions.ToSqlHierarchyId(cats.Node);
                if (node.IsDescendantOf(catNode) && node != catNode)
                {
                    subCategorys.Add(cats);
                    itemParams.SubCategory.Add(cats.Id);
                }
            }

            var spec = new ItemsWithTypesAndBrandsSpecification(itemParams);

            var countSpec = new ItemWithFiltersForCountSpecificication(itemParams);

            var totalItems = await _itemsRepo.CountAsync(countSpec);

            var items = await _itemsRepo.ListAsync(spec);

            IList<ItemToReturnDto> itemsToReturns = new List<ItemToReturnDto>();

            foreach (var elts in items)
            {
                int id = elts.Id;

                List<string> colorName = new List<string>();
                List<string> sizeName = new List<string>();
                List<ReviewDto> reviewList = new List<ReviewDto>();
                List<ImageToReturnDto> imageList = new List<ImageToReturnDto>();
                int ratingsCount = 0; int ratingsValue = 0;

                var colors = await _context.Colors.Where(x => x.ItemId == id).ToListAsync();
                if (colors.Count > 0)
                {
                    elts.Color = colors;
                    foreach (var elt in elts.Color)
                    {
                        colorName.Add(elt.Name);
                    }
                }
                var sizes = await _context.Sizes.Where(x => x.ItemId == id).ToListAsync();
                if (sizes.Count > 0)
                {
                    elts.Size = sizes;
                    foreach (var elt in elts.Size)
                    {
                        sizeName.Add(elt.Name);
                    }
                }
                var images = await _context.Images.Where(x => x.ItemId == id).ToListAsync();
                if (images.Count > 0)
                {
                    elts.Images = images;
                    foreach (var elt in elts.Images)
                    {
                        ImageToReturnDto img = new ImageToReturnDto();
                        img.small = (ApiUrl + elt.UrlSmall);
                        img.medium = (ApiUrl + elt.UrlMedium);
                        img.big = (ApiUrl + elt.UrlBig);
                        imageList.Add(img);
                    }
                }
                var reviews = await _context.Reviews.Where(x => x.ItemId == id).ToListAsync();
                if (reviews.Count > 0)
                {
                    elts.Review = reviews;
                    foreach (var elt in elts.Review)
                    {
                        ReviewDto rev = new ReviewDto();
                        rev.ReviewerName = elt.ReviewerName;
                        rev.ReviewerPhoto = (ApiUrl + elt.ReviewerPhoto);
                        rev.ReviewMessage = elt.ReviewMessage;
                        rev.rate = elt.rate;
                        if(elt.rate == 1)
                        {
                            rev.sentiment = "sentiment_very_dissatisfied";
                        }
                        else if (elt.rate == 2)
                        {
                            rev.sentiment = "sentiment_dissatisfied";
                        }
                        else if (elt.rate == 3 || elt.rate == 4)
                        {
                            rev.sentiment = "sentiment_satisfied";
                        }
                        else if (elt.rate == 5)
                        {
                            rev.sentiment = "sentiment_very_satisfied";
                        }
                        rev.ReviewDate = elt.ReviewDate;
                        reviewList.Add(rev);
                        ratingsCount++;
                        ratingsValue += elt.rate;
                    }
                }

                var itemsToReturn = new ItemToReturnDto
                {
                    Id = id,
                    Name = elts.Name,
                    Description = elts.Description,
                    OldPrice = elts.OldPrice,
                    NewPrice = elts.NewPrice,
                    Discount = elts.Discount,
                    RatingsCount = ratingsCount,
                    RatingsValue = ratingsValue,
                    availibilityCount = elts.availibilityCount,
                    cartCount = elts.cartCount,
                    TechnicalDescription = elts.TechnicalDescription,
                    AdditionalInformation = elts.AdditionalInformation,
                    Weight = elts.Weight,
                    Color = colorName,
                    Size = sizeName,
                    Images = imageList,
                    Reviews = reviewList,
                    CategoryId = elts.Category.Id,
                    BrandName = elts.ProductBrand.Name
                };
                itemsToReturns.Add(itemsToReturn);
            }


            //var data = _mapper
            //    .Map<IReadOnlyList<Item>, IReadOnlyList<ItemToReturnDto>>(items);

            return Ok(new Pagination1<ItemToReturnDto>(itemParams.PageIndex, itemParams.PageSize, totalItems, itemsToReturns)); ;
        }

       
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemToReturnDto>> GetItem(int id)
        {
            var spec = new ItemsWithTypesAndBrandsSpecification(id);

            // return await _productsRepo.GetByIdAsync(id);

            var item = await _itemsRepo.GetEntityWithSpec(spec);

            if (item == null) return NotFound(new ApiResponse(404));

            // return _mapper.Map<Item, ItemToReturnDto>(item);
           
            List<string> colorName = new List<string>();
            List<string> sizeName = new List<string>();
            List<ReviewDto> reviewList = new List<ReviewDto>();
            List<ImageToReturnDto> imageList = new List<ImageToReturnDto>();
            int ratingsCount = 0; int ratingsValue = 0;

            var colors = await _context.Colors.Where(x => x.ItemId == id).ToListAsync();
            if (colors.Count > 0)
            {
                item.Color = colors;
                foreach (var elt in item.Color)
                {
                    colorName.Add(elt.Name);
                }
            }
            var sizes = await _context.Sizes.Where(x => x.ItemId == id).ToListAsync();
            if (sizes.Count > 0)
            {
                item.Size = sizes;
                foreach (var elt in item.Size)
                {
                    sizeName.Add(elt.Name);
                }
            }
            var images = await _context.Images.Where(x => x.ItemId == id).ToListAsync();
            if (images.Count > 0)
            {
                item.Images = images; 
                foreach (var elt in item.Images)
                {
                    ImageToReturnDto img = new ImageToReturnDto();
                    img.small = (ApiUrl + elt.UrlSmall);
                    img.medium = (ApiUrl + elt.UrlMedium);
                    img.big = (ApiUrl + elt.UrlBig);
                    imageList.Add(img);
                }
            }
            var reviews = await _context.Reviews.Where(x => x.ItemId == id).ToListAsync();
            if (reviews.Count > 0)
            {
                item.Review = reviews;
                foreach (var elt in item.Review)
                {
                    ReviewDto rev = new ReviewDto();
                    rev.ReviewerName = elt.ReviewerName;
                    rev.ReviewerPhoto = (ApiUrl + elt.ReviewerPhoto);
                    rev.ReviewMessage = elt.ReviewMessage;
                    rev.rate = elt.rate;
                    if (elt.rate == 1)
                    {
                        rev.sentiment = "sentiment_very_dissatisfied";
                    }
                    else if (elt.rate == 2)
                    {
                        rev.sentiment = "sentiment_dissatisfied";
                    }
                    else if (elt.rate == 3 || elt.rate == 4)
                    {
                        rev.sentiment = "sentiment_satisfied";
                    }
                    else if (elt.rate == 5)
                    {
                        rev.sentiment = "sentiment_very_satisfied";
                    }
                    rev.ReviewDate = elt.ReviewDate;
                    reviewList.Add(rev);
                    ratingsCount++;
                    ratingsValue += elt.rate;
                }
            }

            return new ItemToReturnDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                OldPrice = item.OldPrice,
                NewPrice = item.NewPrice,
                Discount = item.Discount,
                RatingsCount = ratingsCount,
                RatingsValue = ratingsValue,
                availibilityCount = item.availibilityCount,
                cartCount = item.cartCount,
                TechnicalDescription = item.TechnicalDescription,
                AdditionalInformation = item.AdditionalInformation,
                Color = colorName,
                Size = sizeName,
                Images = imageList,
                Reviews = reviewList,
                CategoryId = item.Category.Id,
                BrandName = item.ProductBrand.Name
            };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(int id)
        {
            var brandToReturn = await _productBrandRepo.ListAllAsync();
            //foreach (var brand in brandToReturn)
            //{
            //    brand.ImageUrl = (ApiUrl + brand.ImageUrl);
            //}
            return Ok(brandToReturn);
        }

        [HttpGet("categorys")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategorys(int id)
        {
            return Ok(await _categoryRepo.ListAllAsync());
        }

        [HttpGet("colors")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetColors(int id)
        {

            var colors = await _colorsRepo.ListAllAsync();
            return Ok(colors);
        }

        [HttpGet("sizes")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetSizes(int id)
        {
            return Ok(await _sizesRepo.ListAllAsync());
        }

        [HttpGet("images")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetImages(int id)
        {
            return Ok(await _imagesRepo.ListAllAsync());
        }


    }
}