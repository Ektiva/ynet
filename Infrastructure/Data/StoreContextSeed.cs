using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.ProductBrands.Any())
                {
                    //var brandsData =
                    //    File.ReadAllText(path + @"/Data/SeedData/brands.json");
                    var brandsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    //var typesData =
                    //    File.ReadAllText(path + @"/Data/SeedData/types.json");
                    var typesData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    //var productsData =
                    //    File.ReadAllText(path + @"/Data/SeedData/products.json");'
                    var productsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }



                //if (context.Categorys.Count() < 2)
                //{
                //    //var productsData =
                //    //    File.ReadAllText(path + @"/Data/SeedData/products.json");'
                //    var categorysData =
                //        File.ReadAllText("../Infrastructure/Data/SeedNewData/categories.json");

                //    var categorys = JsonSerializer.Deserialize<List<Category>>(categorysData);

                //    foreach (var item in categorys)
                //    {
                //        Category category = setCategory(context, item);

                //        context.Categorys.Add(category);
                //    }

                //    await context.SaveChangesAsync();
                //}

                if (context.Items.Count() < 31)
                {
                    //var productsData =
                    //    File.ReadAllText(path + @"/Data/SeedData/products.json");'
                    var itemsData =
                        File.ReadAllText("../Infrastructure/Data/SeedNewData/items.json");

                    var items = JsonSerializer.Deserialize<List<Item>>(itemsData);

                    foreach (var item in items)
                    {
                        context.Items.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Colors.Any())
                {
                    //var productsData =
                    //    File.ReadAllText(path + @"/Data/SeedData/products.json");'
                    var colorsData =
                        File.ReadAllText("../Infrastructure/Data/SeedNewData/colors.json");

                    var colors = JsonSerializer.Deserialize<List<Color>>(colorsData);

                    foreach (var item in colors)
                    {
                        context.Colors.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Sizes.Any())
                {
                    //var productsData =
                    //    File.ReadAllText(path + @"/Data/SeedData/products.json");'
                    var sizesData =
                        File.ReadAllText("../Infrastructure/Data/SeedNewData/sizes.json");

                    var sizes = JsonSerializer.Deserialize<List<Size>>(sizesData);

                    foreach (var item in sizes)
                    {
                        context.Sizes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (context.Images.Count() < 49)
                {
                    //var productsData =
                    //    File.ReadAllText(path + @"/Data/SeedData/products.json");'
                    var imagesData =
                        File.ReadAllText("../Infrastructure/Data/SeedNewData/images.json");

                    var images = JsonSerializer.Deserialize<List<Image>>(imagesData);

                    foreach (var item in images)
                    {
                        context.Images.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    //var dmData =
                    //    File.ReadAllText(path + @"/Data/SeedData/delivery.json");
                    var dmData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }

        private static Category setCategory(StoreContext _context,  Category category)
        {
            var parentNode = new byte[2147483591];
            var lastChild = new Category();

            parentNode = _context.Categorys.FirstOrDefault(x => x.Name == category.CategoryUp).Node;
            
            try
            {
                lastChild = _context.Categorys.Where(x => x.CategoryUp == category.CategoryUp)
                    .OrderByDescending(x => x.Node)
                    .FirstOrDefault();

                SqlHierarchyId lastSqlNode = HierarchyExtensions.ToSqlHierarchyId(lastChild.Node);

                category.Node = HierarchyExtensions.ToByteArray(HierarchyExtensions.ToSqlHierarchyId(parentNode).GetDescendant(lastSqlNode, new SqlHierarchyId()));
            }
            catch (Exception ex)
            {
                category.Node = HierarchyExtensions.ToByteArray(HierarchyExtensions.ToSqlHierarchyId(parentNode).GetDescendant(new SqlHierarchyId(), new SqlHierarchyId()));
            }

            return category;
        }
    }
}
