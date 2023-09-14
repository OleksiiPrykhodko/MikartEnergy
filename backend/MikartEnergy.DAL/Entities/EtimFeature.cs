﻿using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class EtimFeature
    {
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Deprecated { get; set; } = false;
        public string Description { get; set;} = string.Empty;
    }
}
