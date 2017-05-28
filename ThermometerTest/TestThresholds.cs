using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerNS;
using System.Linq;
using System.Collections.Generic;

namespace ThermometerTestNS
{
    [TestClass]
    public class TestThresholds
    {
        readonly SetupResources TestResources = new SetupResources();

        [TestMethod]
        // Test if the 'Boiling' threshold has been reached for temperatures below its threshold.
        public void TestCheckIfBoilingNotBoilingCelsiusCelsius()
        {
            //SETUP
            const bool expectedIsBoiling = false;
            var readIsBoiling = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 90, 99 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.GetNewlyReachedThresholds();
                
            }
            foreach (var threshold in thermometer.ThermometerProperties.Thresholds)
            {
                readIsBoiling = threshold.IsReached.Equals(true) && threshold.ThresholdName.Equals("Boiling");
            }

            //ASSERT
            Assert.AreEqual(expectedIsBoiling, readIsBoiling);
        }

        [TestMethod]
        // Test if the 'Boiling' threshold has been reached for temperatures crossing its threshold.
        public void TestCheckIfBoilingAndBoilingCelsiusCelsius()
        {
            //SETUP
            const bool expectedIsBoiling = true;
            var readIsBoiling = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 90, 100 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.GetNewlyReachedThresholds();
                foreach (var threshold in thermometer.ThermometerProperties.Thresholds)
                {
                    if (threshold.IsReached)
                    {
                        readIsBoiling = threshold.ThresholdName.Equals("Boiling");
                    }
                }
            }

            //ASSERT
            Assert.AreEqual(expectedIsBoiling, readIsBoiling);
        }

        [TestMethod]
        // Test if the 'Freezing' threshold has been reached for temperatures above its threshold.
        public void TestCheckIfFreezingNotFreezing()
        {
            //SETUP
            const bool expectedIsFreezing = false;
            var readIsFreezing = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 2, 1 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.GetNewlyReachedThresholds();
            }
            foreach (var threshold in thermometer.ThermometerProperties.Thresholds)
            {
                readIsFreezing = threshold.IsReached.Equals(true) && threshold.ThresholdName.Equals("Freezing");
            }

            //ASSERT
            Assert.AreEqual(expectedIsFreezing, readIsFreezing);
        }

        [TestMethod]
        // Test if the 'Freezing' threshold has been reached for temperatures crossing its threshold.
        public void TestCheckIfFreezingAndFreezing()
        {
            //SETUP
            const bool expectedIsFreezing = true;
            var readIsFreezing = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 0.1, -0.2 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.GetNewlyReachedThresholds();
                foreach (var threshold in thermometer.ThermometerProperties.Thresholds)
                {
                    if (threshold.IsReached)
                    {
                        readIsFreezing = threshold.ThresholdName.Equals("Freezing");
                    }
                }
            }

            //ASSERT
            Assert.AreEqual(expectedIsFreezing, readIsFreezing);
        }

        [TestMethod]
        /* Test if the previously reached 'Freezing' threshold is still reached when its temperatures have passed above its
            upper tolerance band */
        public void TestCheckIfStillFreezingNotFreezing()
        {
            //SETUP
            const bool expectedStillFreezing = false;
            var testStillFreezing = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Freezing", 0, 0, false, true);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 0.1, -0.2, -0.1, 0, 0.11 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillFreezing = thermometer.IsThresholdStillReached(thermometer.ThermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillFreezing, testStillFreezing);
        }

        [TestMethod]
        /* Test if the previously reached 'Freezing' threshold is still reached when its temperatures haven't passed above its
            upper tolerance band */
        public void TestFreezingHasntRaisedPastThresholdWithTolerance()
        {
            //SETUP
            const bool expectedStillFreezing = true;
            var testStillFreezing = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Freezing", 0, 1, false, true);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 0.1, -0.2, -0.1, 0, 0.09 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";
            
            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillFreezing = thermometer.IsThresholdStillReached(thermometer.ThermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillFreezing, testStillFreezing);
        }

        [TestMethod]
        /* Test if the previously reached 'Boiling' threshold is still reached when its temperatures have passed below its
            lower tolerance band with a tolerance of zero */
        public void TestBoilingHasDroppedPastThresholdWithoutTolerance()
        {
            //SETUP
            const bool expectedStillBoiling = false;
            var testStillBoiling = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 98, 101, 98 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillBoiling = thermometer.IsThresholdStillReached(thermometer.ThermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillBoiling, testStillBoiling);
        }

        [TestMethod]
        /* Test if the previously reached 'Boiling' threshold is still reached when its temperatures have passed below its
            lower tolerance band with tolerance */
        public void TestBoilingHasntDroppedPastThresholdWithTolerance()
        {
            //SETUP
            const bool expectedStillBoiling = true;
            var testStillBoiling = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Boiling", 100, 5, true, false);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double> { 98, 101, 96 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillBoiling = thermometer.IsThresholdStillReached(thermometer.ThermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillBoiling, testStillBoiling);
        }
    }
}
