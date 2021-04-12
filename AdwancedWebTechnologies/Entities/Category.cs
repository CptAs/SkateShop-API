﻿
using FiftyOne.Foundation.Mobile.Detection.Entities;
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
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        public Category Parrent { get; private set; }

        public Category() { }
        public Category(string name, Category cat)
        {
            Name = name;
            Parrent = cat;
        }
        public Category(string name)
        {
            Name = name;
            Parrent = null;
        }
    }
}