using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerNS;
using System.Linq;
using System.Collections.Generic;

namespace ThermometerTestNS
{
    [TestClass]
    public class TestThresholds
    {
        SetupResources TestResources = new SetupResources();

        [TestMethod]
        public void TestCheckIfBoilingNotBoiling()
        {
            //SETUP
            bool expectedIsBoiling = false;
            bool readIsBoiling = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 90, 99 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.NewlyReachedThresholds();
                
            }
            foreach (var threshold in thermometer._thermometerProperties.Thresholds)
            {
                readIsBoiling = threshold.IsReached.Equals(true) && threshold.ThresholdName.Equals("Boiling");
            }

            //ASSERT
            Assert.AreEqual(expectedIsBoiling, readIsBoiling);
        }

        [TestMethod]
        public void TestCheckIfBoilingAndBoiling()
        {
            //SETUP
            bool expectedIsBoiling = true;
            bool readIsBoiling = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 90, 100 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.NewlyReachedThresholds();
                foreach (var threshold in thermometer._thermometerProperties.Thresholds)
                {
                    if (threshold.IsReached == true)
                    {
                        readIsBoiling = threshold.ThresholdName.Equals("Boiling");
                    }
                }
            }

            //ASSERT
            Assert.AreEqual(expectedIsBoiling, readIsBoiling);
        }

        [TestMethod]
        public void TestCheckIfFreezingNotFreezing()
        {
            //SETUP
            bool expectedIsFreezing = false;
            bool readIsFreezing = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 2, 1 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.NewlyReachedThresholds();
            }
            foreach (var threshold in thermometer._thermometerProperties.Thresholds)
            {
                readIsFreezing = threshold.IsReached.Equals(true) && threshold.ThresholdName.Equals("Freezing");
            }

            //ASSERT
            Assert.AreEqual(expectedIsFreezing, readIsFreezing);
        }

        [TestMethod]
        public void TestCheckIfFreezingAndFreezing()
        {
            //SETUP
            bool expectedIsFreezing = true;
            bool readIsFreezing = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 0.1, -0.2 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                thermometer.NewlyReachedThresholds();
                foreach (var threshold in thermometer._thermometerProperties.Thresholds)
                {
                    if (threshold.IsReached == true)
                    {
                        readIsFreezing = threshold.ThresholdName.Equals("Freezing");
                    }
                }
            }

            //ASSERT
            Assert.AreEqual(expectedIsFreezing, readIsFreezing);
        }

        [TestMethod]
        public void TestCheckIfStillFreezingNotFreezing()
        {
            //SETUP
            bool expectedStillFreezing = false;
            bool testStillFreezing = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Freezing", 0, 0, false, true);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 0.1, -0.2, -0.1, 0, 0.11 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillFreezing = thermometer.IsThresholdStillReached(thermometer._thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillFreezing, testStillFreezing);
        }

        [TestMethod]
        public void TestFreezingHasntRaisedPastThresholdWithTolerance()
        {
            //SETUP
            bool expectedStillFreezing = true;
            bool testStillFreezing = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Freezing", 0, 0.1, false, true);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 0.1, -0.2, -0.1, 0, 0.09 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillFreezing = thermometer.IsThresholdStillReached(thermometer._thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillFreezing, testStillFreezing);
        }

        [TestMethod]
        public void TestBoilingHasntDroppedPastThresholdWithoutTolerance()
        {
            //SETUP
            bool expectedStillBoiling = false;
            bool testStillBoiling = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 98, 101, 98 };
            //TODO: remove string & put directly in method call
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StorePreviousTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillBoiling = thermometer.IsThresholdStillReached(thermometer._thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillBoiling, testStillBoiling);
        }

        [TestMethod]
        public void TestBoilingHasntDroppedPastThresholdWithTolerance()
        {
            //SETUP
            bool expectedStillBoiling = true;
            bool testStillBoiling = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            thermometer.CreateThermometerThreshold("Boiling", 100, 0.1, true, false);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 98, 101, 95 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillBoiling = thermometer.IsThresholdStillReached(thermometer._thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillBoiling, testStillBoiling);
        }
    }
}
