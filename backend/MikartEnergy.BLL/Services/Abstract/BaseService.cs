using MikartEnergy.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly MikartContext _context;

        public BaseService(MikartContext context)
        {
            _context = context;
        }
    }
}
