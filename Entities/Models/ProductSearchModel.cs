using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Models
{
    public class ProductSearchModel : BaseSearchModel
    {

        public ProductSearchModel()
        {

        }
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal rating { get; set; }
        public int stock { get; set; }
        public string? brand { get; set; }
        public string? category { get; set; }
        public string? thumbnail { get; set; }
        public List<string>? images { get; set; }

    }

}
