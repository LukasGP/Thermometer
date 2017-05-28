using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermometerNS;

namespace ThermometerTestNS
{
    internal class SetupResources
    {
        /// <summary>
        /// Establish a generic test thermometer with two thresholds: 'Boiling' and 'Freezing'.
        /// </summary>
        /// <param name="testThermometer">The current instance of the thermometer requiring setup.</param>
        public void SetupGenericTestThermometer(Thermometer testThermometer)
        {
            // Create two thresholds, 'Boiling' and 'Freezing'.
            testThermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);
            testThermometer.CreateThermometerThreshold("Freezing", 0, 0, false, true);
        }
    }
}
