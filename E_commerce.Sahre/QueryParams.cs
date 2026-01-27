using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahred
{
    public class QueryParams
    {

        public int? BrandId { get; set; }
        public int ? TypeId { get; set; }


        public string? Search { get; set; }
        public  SortedParams sorted { get; set; }
 
    }
}
