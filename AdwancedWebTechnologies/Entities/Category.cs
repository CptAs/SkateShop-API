
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        public Category ParrentCategory { get; set; }
        [ForeignKey("Category")]
        public int? ParrentCategoryId { get; set; }
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public Category() { }
        public Category(int id, string name, Category cat)
        {
            CategoryId = id;
            Name = name;
            ParrentCategory = cat;
            ParrentCategoryId = cat.CategoryId;
        }
        public Category(int id, string name)
        {
            CategoryId = id;
            Name = name;
            ParrentCategory = null;
            ParrentCategoryId = null;
        }
    }
}