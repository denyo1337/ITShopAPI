using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class ProductsQuery
    {
        public string SearchPhrase { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
