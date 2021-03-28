using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Entities
{
    public class Producer
    {
        public int ProducerId { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        public Producer() { }
        public Producer(string name)
        {
            Name = name;
        }
    }
}
