using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrieveData
{
    class ResultModel
    {
        public List<MovieDetailsModel> results
        { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }

        public Dates dates { get; set; }
        public int total_pages { get; set; }
    }
}
