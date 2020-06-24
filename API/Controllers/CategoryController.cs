using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Types;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public CategoryController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetCategorys()
        {
            var categorys = await _context.Categorys.Where(x => x.Name != "Root").ToListAsync();
            var toReturn = new List<CategoryToReturnDto>();
            foreach(var item in categorys)
            {
                var alt = new CategoryToReturnDto();
                alt.Id = item.Id;
                alt.Name = item.Name;
                alt.hasSubCategory = item.hasSubCategory;
                alt.parentId = item.parentId;
                toReturn.Add(alt);
            }
            return toReturn;
        }

        // GET: api/Category/GetCategory/level
        [HttpGet("{GetCategory}/{level}")]
        public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetCategory(int id, int parentId)
        {
            var categorys = await _context.Categorys.Where(x => (x.Name != "Root" && x.Name != "All Categories" && x.Name != "Motors")).ToListAsync();
            var toReturn = new List<CategoryToReturnDto>();

            if (parentId != 0)
            {
                var parent = await _context.Categorys.FindAsync(parentId);
                var parentNode = HierarchyExtensions.ToSqlHierarchyId(parent.Node);

                foreach (var item in categorys)
                {
                    var node = HierarchyExtensions.ToSqlHierarchyId(item.Node);
                    if (node.GetLevel().ToSqlInt32() == id && node.GetAncestor(1) == parentNode)
                    {
                        var alt = new CategoryToReturnDto();
                        alt.Id = item.Id;
                        alt.Name = item.Name;
                        alt.hasSubCategory = item.hasSubCategory;
                        alt.parentId = item.parentId;
                        toReturn.Add(alt);
                    }
                }
            }else
            {
                foreach (var item in categorys)
                {
                    var node = HierarchyExtensions.ToSqlHierarchyId(item.Node);
                    if (node.GetLevel().ToSqlInt32() == id)
                    {
                        var alt = new CategoryToReturnDto();
                        alt.Id = item.Id;
                        alt.Name = item.Name;
                        alt.hasSubCategory = item.hasSubCategory;
                        alt.parentId = item.parentId;
                        toReturn.Add(alt);
                    }
                }
            }
            
            return toReturn;
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categorys.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Path = Encoding.UTF8.GetString(category.Node, 0, category.Node.Length);

            return category;
        }

        //// GET: api/Category/GetLastChild/Root
        //[HttpGet("{GetLastChild}/{name}")]
        //public async Task<ActionResult<Category>> GetLastChild(string name)
        //{
        //    var listNode = _context.Categorys.Where(x => x.CategoryUp == name)
        //        .OrderByDescending(x => x.Node)
        //        .FirstOrDefault();

        //    return listNode;
        //}

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (category.CategoryUp == "Root")
            {
                category.NodePath = "/1/";
                category.parentId = 0;
            }

            if (category.CategoryUp == "N/A")
            {
                category.NodePath = "/1/";
                category.parentId = -1;
            }
            else
            {
                category.parentId = _context.Categorys.FirstOrDefault(x => x.Name == category.CategoryUp).Id;

                var parentNode = new byte[2147483591];
                var lastChild = new Category();
                try
                {
                    parentNode = _context.Categorys.FirstOrDefault(x => x.Name == category.CategoryUp).Node;
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = "Parent does not exist. Please verified the spelling or Add this as a parent first. => Error message: " + ex.Message });
                }

                try
                {
                    lastChild = _context.Categorys.Where(x => x.CategoryUp == category.CategoryUp)
                        .OrderByDescending(x => x.Node)
                        .FirstOrDefault();

                    SqlHierarchyId lastSqlNode = HierarchyExtensions.ToSqlHierarchyId(lastChild.Node);

                    category.Node = HierarchyExtensions.ToByteArray(HierarchyExtensions.ToSqlHierarchyId(parentNode).GetDescendant(lastSqlNode, new SqlHierarchyId()));
                    if (category.CategoryUp == "N/A")
            {
                category.NodePath = "/1/";
                category.parentId = -1;
            }
                }
                catch (Exception ex)
                {
                    category.Node = HierarchyExtensions.ToByteArray(HierarchyExtensions.ToSqlHierarchyId(parentNode).GetDescendant(new SqlHierarchyId(), new SqlHierarchyId()));
                }
            }
            _context.Categorys.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }
    }
}