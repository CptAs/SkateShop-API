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
        [Required]
        public int Discount { get; set; } = 0;
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }

        public Product() { }
        public Product(string name, decimal price, string description, int discount, Category category, Producer producer)
        {
            Name = name;
            Price = price;
            Description = description;
            Discount = discount;
            Category = category;
            CategoryId = category.CategoryId;
            Producer = producer;
            ProducerId = producer.ProducerId;
        }
    }
}
