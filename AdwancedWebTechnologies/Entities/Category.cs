using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        public Category ParrentCategory { get; set; }
        public int? ParrentCategoryId { get; set; }

        public Category() { }
        public Category(string name, Category cat)
        {
            Name = name;
            ParrentCategory = cat;
            ParrentCategoryId = cat.CategoryId;
        }
        public Category(string name)
        {
            Name = name;
            ParrentCategory = null;
            ParrentCategoryId = null;
        }
    }
}
