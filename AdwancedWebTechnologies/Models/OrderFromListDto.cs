using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Models
{
    public class OrderFromListDto
    {
        public List<List<int>> orderProducts { get; set; }
    }
}
