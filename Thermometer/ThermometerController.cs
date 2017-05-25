using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerNS
{
    public class Thermometer
    {
        public ThermometerProperties thermometerProperties = new ThermometerProperties();
        private double previousTemperature;
        public double currentTemperature;
        public bool hasBoiledFlag = false;
        public bool hasfrozenFlag = false;
        public List<double> HistoricalTemperatures;



        // Set the thermometer's optional thresholds property
        public void CreateThermometerThreshold(string thresholdName, double thresholdValue, double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge)
        {
            var threshold = new Threshold(thresholdName, thresholdValue, temperatureTolerance, sensativeToRisingEdge, sensativeToFallingEdge);
        }


        // Capture the current temperature reading for future referral.
        public void StoreCurrentTemperature()
        {
            previousTemperature = currentTemperature;
        }

        // Take a temperature reading and conduct any necessary unit conversions.
        public void RegisterTemperatureChange(double temperatureReading, string measurementUnits, string displayUnits)
        {
            StoreCurrentTemperature();
            HistoricalTemperatures.Add(ConvertUnits(temperatureReading, measurementUnits, displayUnits));
        }

        private double ConvertUnits(double temperatureReading, string measurementUnits, string displayUnits)
        {
            // TODO: change to establish measured units from dropdown on UI. Also establish display units from seperate dropdown
            if (measurementUnits == "Celsius" && displayUnits == "Fahrenheit")
            {
                // Convert Celsius to Fahrenheight
                currentTemperature = Math.Round((temperatureReading * 1.8) + 32, 2);
                return currentTemperature;
            }
            else if (measurementUnits == "Fahrenheit" && displayUnits == "Celsius")
            {
                // Convert Fahrenheight to Celsius
                currentTemperature = Math.Round((temperatureReading - 32) * 0.5556, 2);
                return currentTemperature;
            }
            else if ((measurementUnits == "Celsius" && displayUnits == "Celsius") || (measurementUnits == "Fahrenheit" && displayUnits == "Fahrenheit"))
            {
                currentTemperature = Math.Round(temperatureReading, 2);
                return currentTemperature;
            }
            // There's been an issue with establishing the measured units & display units and converting the read temperature. Return Nan.
            throw new System.InvalidCastException();
        }
        
        // Establish whether or not the temperature is dropping. Note: should be called before checking if any new thresholds have been reached or released.
        public bool IsTemperatureFalling()
        {
            return ((currentTemperature - previousTemperature) < 0);
        }

        public bool HasThresholdBeenReached()
        {
            foreach (var threshold in thermometerProperties.Thresholds)
            {
                if (threshold.IsReached == true)
                {
                    return true;
                }

                // Check for rising edge thresholds
                if (currentTemperature >= threshold.ThresholdValue && previousTemperature < threshold.ThresholdValue && threshold.SensativeToRisingEdge)
                {
                    threshold.IsReached = true;
                    return true;
                }

                // Check for falling edge thresholds
                else if (currentTemperature <= threshold.ThresholdValue && previousTemperature > threshold.ThresholdValue && threshold.SensativeToFallingEdge)
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
            if (threshold.SensativeToRisingEdge && currentTemperature < threshold.TempTolerance.LowerBand)
            {
                threshold.IsReached = false;
                return false;
            }
            if (threshold.SensativeToFallingEdge && currentTemperature > threshold.TempTolerance.UpperBand)
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
