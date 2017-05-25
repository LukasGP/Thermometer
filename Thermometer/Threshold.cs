using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerNS
{
    public class Threshold
    {
        public string ThresholdName;
        public double ThresholdValue;
        public bool SensativeToRisingEdge;
        public bool SensativeToFallingEdge;
        public bool IsReached;
        public TemperatureTolerance TempTolerance;

        public Threshold(string thresholdName, double thresholdValue, double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge)
        {
            ThresholdName = thresholdName;
            ThresholdValue = thresholdValue;
            SensativeToRisingEdge = sensativeToRisingEdge;
            SensativeToFallingEdge = sensativeToFallingEdge;
            TempTolerance = new TemperatureTolerance(temperatureTolerance, thresholdValue);
        }
    }
}
