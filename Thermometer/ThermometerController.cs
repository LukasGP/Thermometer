using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerNS
{
    public class Thermometer
    {
        private string _celsius = "Celsius";
        private string _fahrenheit = "Fahrenheit";
        private double _previousTemperature;
        private double _normalizedTemperature;
        public double _currentTemperature;
        public ThermometerProperties _thermometerProperties = new ThermometerProperties();
        public List<double> _historicalTemperatures = new List<double>();
        public List<Threshold> _newlyReachedThresholds = new List<Threshold>();

        // Set the thermometer's optional thresholds property
        public void CreateThermometerThreshold(string thresholdName, double thresholdValue, double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge)
        {
            double thresholdValueFahrenheit = ConvertUnits(thresholdValue, _celsius, _fahrenheit);
            _thermometerProperties.Thresholds.Add(new Threshold(thresholdName, thresholdValue, temperatureTolerance, sensativeToRisingEdge, sensativeToFallingEdge));
        }

        // Capture the current temperature reading for future referral.
        public void StorePreviousTemperature()
        {
            _previousTemperature = _normalizedTemperature;
        }

        // Take a temperature reading and conduct any necessary unit conversions.
        public void RegisterTemperatureChange(double temperatureReading, string measurementUnits, string displayUnits)
        {
            _newlyReachedThresholds.Clear();
            // If the input units have changed update the operating units of the thermometer.
            UpdateThermometerUnits(measurementUnits, displayUnits);
            StorePreviousTemperature();
            // Normalize the input temperature reading to celsius.
            _normalizedTemperature = ConvertUnits(temperatureReading, measurementUnits, _celsius);
            // Establish a list of all newly reached thresholds.
            _newlyReachedThresholds = NewlyReachedThresholds();
            // Check if previously reached thresholds are still reached.
            CheckIfThresholdsAreStillReached();
            // Set the final output temperature that's ready for display.
            _currentTemperature = ConvertUnits(_normalizedTemperature, _celsius, displayUnits);
        }

        public void UpdateThermometerUnits(string measurementUnits, string displayUnits)
        {
            _thermometerProperties.InputIsCelsius = measurementUnits.Equals(_celsius);
            _thermometerProperties.DisplayInCelsius = displayUnits.Equals(_celsius);
        }

        public void SetupDefaultThresholds()
        {
            CreateThermometerThreshold("Boiling", 100, 0.1, true, false);
            CreateThermometerThreshold("Freezing", 0, 1, false, true);
            CreateThermometerThreshold("Hot Out", 45, 0.1, true, true);
        }

        public double ConvertUnits(double temperatureReading, string measurementUnits, string displayUnits)
        {
            // If there has been a null input for measurement units or display units return the passed temperature.
            if (measurementUnits == "" || displayUnits == "")
            {
                return temperatureReading;
            }
            // Instead of passing around a string have the units as a property.
            if (measurementUnits == _celsius && displayUnits == _fahrenheit)
            {
                // Convert Celsius to Fahrenheight
                return Math.Round((temperatureReading * 1.8) + 32, 2);
                
            }
            else if (measurementUnits == _fahrenheit && displayUnits == _celsius)
            {
                // Convert Fahrenheight to Celsius
                return Math.Round((temperatureReading - 32) * 0.5556, 2);
            }
            else if ((measurementUnits == _celsius && displayUnits == _celsius) || (measurementUnits == _fahrenheit && displayUnits == _fahrenheit))
            {
                return Math.Round(temperatureReading, 2);
            }
            // There's been an issue with establishing the measured units & display units and converting the read temperature. Return Nan.
            throw new System.InvalidCastException();
        }

        public List<Threshold> NewlyReachedThresholds()
        {
            var newlyReachedThresholds = new List<Threshold>();
            foreach (var threshold in _thermometerProperties.Thresholds)
            {
                if (threshold.IsReached == false)
                {
                    // Check for rising edge thresholds
                    if (_normalizedTemperature >= threshold.ThresholdValueCelsius && _previousTemperature < threshold.ThresholdValueCelsius && threshold.SensitiveToRisingEdge)
                    {
                        threshold.IsReached = true;
                        newlyReachedThresholds.Add(threshold);
                    }

                    // Check for falling edge thresholds
                    else if (_normalizedTemperature <= threshold.ThresholdValueCelsius && _previousTemperature > threshold.ThresholdValueCelsius && threshold.SensitiveToFallingEdge)
                    {
                        threshold.IsReached = true;
                        newlyReachedThresholds.Add(threshold);
                    }
                }
            }
            return newlyReachedThresholds;
        }

        public void CheckIfThresholdsAreStillReached()
        {
            foreach (var threshold in _thermometerProperties.Thresholds)
            {
                if (threshold.IsReached == true)
                {
                    IsThresholdStillReached(threshold);
                }
            }
        }

        public bool IsThresholdStillReached(Threshold threshold)
        {
            // Case 1: rising edge threshold still above threshold
            // Case 2: falling edge thresh
            if (threshold.SensitiveToRisingEdge && _normalizedTemperature < threshold.TempTolerance.LowerBand)
            {
                threshold.IsReached = false;
                return false;
            }
            if (threshold.SensitiveToFallingEdge && _normalizedTemperature > threshold.TempTolerance.UpperBand)
            {
                threshold.IsReached = false;
                return false;
            }
            threshold.IsReached = true;
            return true;
        }
    }
}
