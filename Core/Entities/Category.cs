using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Microsoft.SqlServer.Types;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        public string CategoryUp { get; set; }
        [Required]
        public int parentId { get; set; }
        public bool hasSubCategory { get; set; }
        [MaxLength(892)]
        public byte[] Node
        {
            get { return node; }
            set
            {
                node = value;
                nodeSql = node.ToSqlHierarchyId();
            }
        }

        [NotMapped]
        public string NodePath
        {
            //get => nodeSql.ToString();
            //set => Node = value.ToSqlHierarchyId().ToByteArray();
            get { return nodeSql.ToString(); }
            set { Node = value.ToSqlHierarchyId().ToByteArray(); }
        }

        [NotMapped]
        public SqlHierarchyId nodeSql;
        [NotMapped]
        public string Path { get; set; }

        //Privates attributes
        private byte[] node;
    }
}