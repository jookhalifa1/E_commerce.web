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

         private int _PageIndex = 1;

        public int  PageIndex
        {
            get
            {
        return _PageIndex;
            }
            set
            {
                if (value <= 0)
                {
                    _PageIndex = 1;
                }
                else
                {
                    _PageIndex = value;
                }
            }
        }


        private const int MinPageSize = 5;
        private const int MaxPageSize = 10;
        private int _PageSize = MinPageSize;

        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                if (value <= 0)
                {
                    _PageSize= MinPageSize;
                }
                else if (value > MaxPageSize)
                {
                    _PageSize= MaxPageSize;
                }
                else
                {
                    _PageSize = value;
                }


            }
        }


    }
}
