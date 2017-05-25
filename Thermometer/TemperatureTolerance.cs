using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerNS
{
    public class TemperatureTolerance
    {
        public double ToleranceValue;
        public double UpperBand;
        public double LowerBand;

        public TemperatureTolerance(double tempTolerance, double thresholdTemperature)
        {
            ToleranceValue = tempTolerance;
            UpperBand = thresholdTemperature + (thresholdTemperature * ToleranceValue);
            LowerBand = thresholdTemperature - (thresholdTemperature - ToleranceValue);
        }
    }
}
