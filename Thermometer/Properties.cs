﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerNS
{
    public class ThermometerProperties
    {
        public List<Threshold> Thresholds = new List<Threshold>();
        //TODO: Utilize these properties
        public bool DisplayInCelsius;
        public bool InputIsCelsius;
    }
}
