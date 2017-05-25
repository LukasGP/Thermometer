using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermometerNS;

namespace ThermometerTestNS
{
    class SetupResources
    {
        public void SetupGenericTestThermometer(Thermometer testThermometer)
        {
            // Specify the properties of the thermometer.
            testThermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);
            testThermometer.CreateThermometerThreshold("Freezing", 0, 0, false, true);
        }
    }
}
