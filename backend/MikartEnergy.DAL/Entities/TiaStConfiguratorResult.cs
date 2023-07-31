﻿using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class TiaStConfiguratorResult : BaseEntity
    {
        IEnumerable<KeyValuePair<string, int>> ExistingProducts { get; set; }
        IEnumerable<KeyValuePair<string, int>> NotExistingProducts { get; set; }
    }
}
