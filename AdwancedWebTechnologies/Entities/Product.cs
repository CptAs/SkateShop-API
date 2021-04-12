using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedWebTechnologies.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public int Discount { get; set; } = 0;
        public Category Category { get; private set; }
        public Producer Producer { get; private set; }

        public Product() { }
        public Product(string name, decimal price, string description, int discount, Category category, Producer producer)
        {
            Name = name;
            Price = price;
            Description = description;
            Discount = discount;
            Category = category;
            Producer = producer;
        }
    }
}
