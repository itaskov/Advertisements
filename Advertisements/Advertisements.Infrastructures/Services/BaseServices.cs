using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advertisements.Data;

namespace Advertisements.Infrastructures.Services
{
    public abstract class BaseServices
    {
        protected IAdsData Data;

        protected BaseServices(IAdsData adsData)
        {
            this.Data = adsData;
        }
    }
}
