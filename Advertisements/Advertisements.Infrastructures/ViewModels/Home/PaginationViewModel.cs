using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisements.Infrastructures.ViewModels.Home
{
    public class PaginationViewModel
    {
        public int? SelectedPage { get; set; }
        
        public int NumberOfPages { get; set; }
        
        public int PageSize { get; set; }

        public int? CategoryId { get; set; }

        public int? TownId { get; set; }
    }
}
