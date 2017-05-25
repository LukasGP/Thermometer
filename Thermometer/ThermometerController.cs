using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerNS
{
    public class Thermometer
    {
        private double _previousTemperature;
        public double _currentTemperature;
        public ThermometerProperties _thermometerProperties = new ThermometerProperties();
        public List<double> _historicalTemperatures = new List<double>();


        // Set the thermometer's optional thresholds property
        public void CreateThermometerThreshold(string thresholdName, double thresholdValue, double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge)
        {
            _thermometerProperties.Thresholds.Add(new Threshold(thresholdName, thresholdValue, temperatureTolerance, sensativeToRisingEdge, sensativeToFallingEdge));
        }


        // Capture the current temperature reading for future referral.
        public void StoreCurrentTemperature()
        {
            _previousTemperature = _currentTemperature;
        }

        // Take a temperature reading and conduct any necessary unit conversions.
        public void RegisterTemperatureChange(double temperatureReading, string measurementUnits, string displayUnits)
        {
            StoreCurrentTemperature();
            _historicalTemperatures.Add(ConvertUnits(temperatureReading, measurementUnits, displayUnits));
        }

        public double GetLastTemperatureReading()
        {
            return _historicalTemperatures.Last();
        }

        private double ConvertUnits(double temperatureReading, string measurementUnits, string displayUnits)
        {
            // TODO: change to establish measured units from dropdown on UI. Also establish display units from seperate dropdown
            // Instead of passing around a string have the units as a property.
            if (measurementUnits == "Celsius" && displayUnits == "Fahrenheit")
            {
                // Convert Celsius to Fahrenheight
                _currentTemperature = Math.Round((temperatureReading * 1.8) + 32, 2);
                return _currentTemperature;
            }
            else if (measurementUnits == "Fahrenheit" && displayUnits == "Celsius")
            {
                // Convert Fahrenheight to Celsius
                _currentTemperature = Math.Round((temperatureReading - 32) * 0.5556, 2);
                return _currentTemperature;
            }
            else if ((measurementUnits == "Celsius" && displayUnits == "Celsius") || (measurementUnits == "Fahrenheit" && displayUnits == "Fahrenheit"))
            {
                _currentTemperature = Math.Round(temperatureReading, 2);
                return _currentTemperature;
            }
            // There's been an issue with establishing the measured units & display units and converting the read temperature. Return Nan.
            throw new System.InvalidCastException();
        }

        public bool HasThresholdBeenReached()
        {
            foreach (var threshold in _thermometerProperties.Thresholds)
            {
                if (threshold.IsReached == true)
                {
                    return true;
                }

                // Check for rising edge thresholds
                if (_currentTemperature >= threshold.ThresholdValue && _previousTemperature < threshold.ThresholdValue && threshold.SensativeToRisingEdge)
                {
                    threshold.IsReached = true;
                    return true;
                }

                // Check for falling edge thresholds
                else if (_currentTemperature <= threshold.ThresholdValue && _previousTemperature > threshold.ThresholdValue && threshold.SensativeToFallingEdge)
                {
                    threshold.IsReached = true;
                    return true;
                }
            }
            return false;
        }

        public bool IsThresholdStillReached(Threshold threshold)
        {
            // Case 1: rising edge threshold still above threshold
            // Case 2: falling edge thresh
            if (threshold.SensativeToRisingEdge && _currentTemperature < threshold.TempTolerance.LowerBand)
            {
                threshold.IsReached = false;
                return false;
            }
            if (threshold.SensativeToFallingEdge && _currentTemperature > threshold.TempTolerance.UpperBand)
            {
                threshold.IsReached = false;
                return false;
            }
            return true;
        }


        /*
        public void CheckIfThresholdReached()
        {
                var tempDeltaDirection = IsTemperatureFalling();
                // If the thermometer hasn't already boiled check if it's boiling.
                if (!hasBoiledFlag)
                {
                    if (IsBoiling())
                    {
                        //MessageBox.Show("Boiling!");
                    }
                }
                // If the thermometer has boiled check to see if it's fallen outside of the boiling threshold with tolerance.
                else
                {
                    IsStillBoiling();
                }
                if (!hasfrozenFlag)
                {
                    if (IsFreezing())
                    {
                        //MessageBox.Show("Frozen!");
                    }
                }
                else
                {
                    IsStillFreezing();
                }
            }





        // Check if boiling threshold has been reached.
        public bool IsBoiling()
        {
            if (currentTemperature >= thermometerProperties.BoilingThreshold)
            {
                // Flag that boiling threshold has been reached.
                hasBoiledFlag = true;
                return true;
            }
            return false;
        }

        public bool IsStillBoiling()
        {
            // TODO: may need additional checks for negative numbers?
            var tempTolerance = thermometerProperties.BoilingThreshold - (thermometerProperties.TemperatureTolerance * thermometerProperties.BoilingThreshold);
            // If the temperature has dropped outside of the boiling range set the has boiled flag to false and return false;
            if (currentTemperature < tempTolerance)
            {
                // Drop the has boiled flag.
                hasBoiledFlag = false;
                return false;
            }
            return true;
        }

        public bool IsFreezing()
        {
            // Flag that freezing threshold has been reached.
            if (currentTemperature <= thermometerProperties.FreezingThreshold)
            {
                hasfrozenFlag = true;
                return true;
            }
            return false;
        }

        public bool IsStillFreezing()
        {
            // TODO: may need additional checks for negative numbers?
            double tempTolerance;
            if (thermometerProperties.FreezingThreshold != 0)
            {
                tempTolerance = (thermometerProperties.TemperatureTolerance * thermometerProperties.FreezingThreshold) + thermometerProperties.FreezingThreshold;
            }
            else
            {
                tempTolerance = thermometerProperties.TemperatureTolerance;
            }
            // If the temperature has dropped outside of the boiling range set the has boiled flag to false and return false;
            if (currentTemperature > tempTolerance)
            {
                // Drop the has frozen flag.
                hasfrozenFlag = false;
                return false;
            }
            return true;
        }*/
    }
}
