using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Models
{
    public class ProductResponse
    {

        public List<ProductModel>? products { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }


    }

}
