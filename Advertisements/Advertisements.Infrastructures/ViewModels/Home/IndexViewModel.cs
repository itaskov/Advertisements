using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisements.Infrastructures.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<AdsIndexViewModel> AdsIndexViewModel { get; set; }

        public RightSideBarViewModel RightSideBarViewModel { get; set; }
    }
}
