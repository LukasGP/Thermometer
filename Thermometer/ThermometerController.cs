using System;
using System.Collections.Generic;
using System.Linq;

namespace ThermometerNS
{
    public class Thermometer
    {
        private const string Celsius = "Celsius";
        private const string Fahrenheit = "Fahrenheit";
        private double PreviousTemperature;
        private double NormalizedTemperature;
        public double CurrentTemperature;
        public ThermometerProperties ThermometerProperties = new ThermometerProperties();
        public List<double> HistoricalTemperatures = new List<double>();
        public List<Threshold> NewlyReachedThresholds = new List<Threshold>();

        /// <summary>
        /// Set the thermometer's optional thresholds property
        /// </summary>
        /// <param name="thresholdName">The threshold's name</param>
        /// <param name="thresholdValue">The threshold's value</param>
        /// <param name="temperatureTolerance">The threshold's temperature tolerance in degrees</param>
        /// <param name="sensativeToRisingEdge">Whether or not the threshold should be able to be triggered by rising temperatures</param>
        /// <param name="sensativeToFallingEdge">Whether or not the threshold should be able to be triggered by falling temperatures</param>
        public void CreateThermometerThreshold(string thresholdName, double thresholdValue, double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge)
        {
            ThermometerProperties.Thresholds.Add(new Threshold(thresholdName, thresholdValue, temperatureTolerance, sensativeToRisingEdge, sensativeToFallingEdge));
        }

        /// <summary>
        /// Capture the current temperature reading for future historical reference. This value will always be normalized to Celsius.
        /// This is to ease the difficulty associated with thresholds. See comments in RegisterTemperatureChange().
        /// </summary>
        public void StorePreviousTemperature()
        {
            PreviousTemperature = NormalizedTemperature;
        }

        /// <summary>
        /// Take a temperature reading and conduct any necessary unit conversions. Also check if any new thresholds have been
        /// established as well as if any previously established thresholds are no longer considered reached, ex: the water was boiling
        /// but is not boiling any longer.
        /// </summary>
        /// <param name="temperatureReading">An external temperature reading in either Celsius or Fahrenheit</param>
        /// <param name="measurementUnits">Specifies whether or not the temperature reading is in Celsius or Fahrenheit</param>
        /// <param name="displayUnits">Specifies the desired output units (Celsius or Fahrenheit)</param>
        public void RegisterTemperatureChange(double temperatureReading, string measurementUnits, string displayUnits)
        {
            NewlyReachedThresholds.Clear();
            UpdateThermometerUnits(measurementUnits, displayUnits);
            StorePreviousTemperature();
            // Normalize the input temperature reading to celsius to simplify the handling of thresholds.
            // Note: if this is undesireable an alternative would be for thresholds to have a unit proprety.
            NormalizedTemperature = ConvertUnits(temperatureReading, measurementUnits, Celsius);
            NewlyReachedThresholds = GetNewlyReachedThresholds();
            CheckIfThresholdsAreStillReached();
            // Set the final output temperature that's ready to be displayed.
            CurrentTemperature = ConvertUnits(NormalizedTemperature, Celsius, displayUnits);
        }

        /// <summary>
        /// Update the thermometer's units. These specify what the thermometer should expect in terms of input
        /// units and output untis (Celsius or Fahrenheit).
        /// </summary>
        /// <param name="measurementUnits">Specifies the input units</param>
        /// <param name="displayUnits">Specifies the desired output units</param>
        public void UpdateThermometerUnits(string measurementUnits, string displayUnits)
        {
            ThermometerProperties.InputIsCelsius = measurementUnits.Equals(Celsius);
            ThermometerProperties.DisplayInCelsius = displayUnits.Equals(Celsius);
        }

        /// <summary>
        /// Establish a few typical default thresholds.
        /// </summary>
        public void SetupDefaultThresholds()
        {
            CreateThermometerThreshold("Boiling", 100, 0.1, true, false);
            CreateThermometerThreshold("Freezing", 0, 1, false, true);
            CreateThermometerThreshold("Hot Out", 45, 0.1, true, true);
        }

        /// <summary>
        /// Change the units of a temperature reading based on the input units and desired output units.
        /// ie: Celsius -> Fahrenheit or Fahrenheit -> Celsius.
        /// </summary>
        /// <param name="temperatureReading">External temperature reading</param>
        /// <param name="measurementUnits">The units of the external temperature reading</param>
        /// <param name="displayUnits">The desired units of the output temperature reading</param>
        /// <returns>The converted temperature value in the desired units</returns>
        public double ConvertUnits(double temperatureReading, string measurementUnits, string displayUnits)
        {
            // Check for null unit selection.
            if (measurementUnits == "" || displayUnits == "")
            {
                return temperatureReading;
            }
            // Convert Celsius to Fahrenheit.
            if (measurementUnits == Celsius && displayUnits == Fahrenheit)
            {
                // Convert Celsius to Fahrenheight.
                return Math.Round((temperatureReading * 1.8) + 32, 2);
                
            }
            // Convert Fahrenheight to Celsius.
            if (measurementUnits == Fahrenheit && displayUnits == Celsius)
            {
                return Math.Round((temperatureReading - 32) * 0.5556, 2);
            }
            // Don't perform a conversion if Celsius -> Celsius or Fahrenheit -> Fahrenheit
            if ((measurementUnits == Celsius && displayUnits == Celsius) || (measurementUnits == Fahrenheit && displayUnits == Fahrenheit))
            {
                return Math.Round(temperatureReading, 2);
            }
            // There's been an issue with establishing the measured units & display units and converting the read temperature. Return Nan.
            throw new InvalidCastException();
        }

        /// <summary>
        /// Establish a list of thresholds that have been reached over the last temperature change. Comparing the 
        /// latest temperature reading with the previous temperature reading and well as all threshold values, 
        /// and sensitivity directions.
        /// </summary>
        /// <returns>A list of newly reached thresholds</returns>
        public List<Threshold> GetNewlyReachedThresholds()
        {
            var newlyReachedThresholds = new List<Threshold>();
            foreach (var threshold in ThermometerProperties.Thresholds)
            {
                if (threshold.IsReached) continue;
                // Check for rising edge thresholds
                if (NormalizedTemperature >= threshold.ThresholdValueCelsius && PreviousTemperature < threshold.ThresholdValueCelsius && threshold.SensitiveToRisingEdge)
                {
                    threshold.IsReached = true;
                    newlyReachedThresholds.Add(threshold);
                }
                // Check for falling edge thresholds
                else if (NormalizedTemperature <= threshold.ThresholdValueCelsius && PreviousTemperature > threshold.ThresholdValueCelsius && threshold.SensitiveToFallingEdge)
                {
                    threshold.IsReached = true;
                    newlyReachedThresholds.Add(threshold);
                }
            }
            return newlyReachedThresholds;
        }

        /// <summary>
        /// Loops through all previously reached thresholds determining if they are still reached.
        /// </summary>
        public void CheckIfThresholdsAreStillReached()
        {
            foreach (var threshold in ThermometerProperties.Thresholds)
            {
                if (threshold.IsReached)
                {
                    IsThresholdStillReached(threshold);
                }
            }
        }

        /// <summary>
        /// Determines if previously reached thresholds are still considered reached. ie: The water was boiling, is it
        /// still boiling? If the threshold is no longer reached it lowers its IsReached flag.
        /// </summary>
        /// <param name="threshold">The threshold data structure that is being checked</param>
        /// <returns>Whether or not the threshold is still reached (bool). Currently solely used by unit tests</returns>
        public bool IsThresholdStillReached(Threshold threshold)
        {
            // Case 1: rising edge threshold still above threshold
            // Case 2: falling edge thresh
            if (threshold.SensitiveToRisingEdge && NormalizedTemperature < threshold.TempTolerance.LowerBand)
            {
                threshold.IsReached = false;
                return false;
            }
            if (threshold.SensitiveToFallingEdge && NormalizedTemperature > threshold.TempTolerance.UpperBand)
            {
                threshold.IsReached = false;
                return false;
            }
            threshold.IsReached = true;
            return true;
        }
    }
}
