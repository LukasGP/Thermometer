using System.Collections.Generic;

namespace ThermometerNS
{
    public class ThermometerProperties
    {
        public List<Threshold> Thresholds = new List<Threshold>();
        public bool DisplayInCelsius;
        public bool InputIsCelsius;
    }
}
