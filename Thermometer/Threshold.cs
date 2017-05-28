namespace ThermometerNS
{
    public class Threshold
    {
        public string ThresholdName;
        public double ThresholdValueCelsius;
        public bool SensitiveToRisingEdge;
        public bool SensitiveToFallingEdge;
        public bool IsReached = false;
        public TemperatureTolerance TempTolerance;

        public Threshold(string thresholdName, double thresholdValue, double temperatureTolerance, bool sensitiveToRisingEdge, bool sensitiveToFallingEdge)
        {
            ThresholdName = thresholdName;
            ThresholdValueCelsius = thresholdValue;
            SensitiveToRisingEdge = sensitiveToRisingEdge;
            SensitiveToFallingEdge = sensitiveToFallingEdge;
            TempTolerance = new TemperatureTolerance(temperatureTolerance, thresholdValue);
        }
    }
}