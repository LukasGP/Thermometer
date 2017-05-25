using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerNS;
using System.Collections.Generic;
using System.Linq;

namespace ThermometerTestNS
{
    [TestClass]
    public class ThermometerTest
    {

        public void SetupThermometer(Thermometer testThermometer)
        {
            // Specify the properties of the thermometer.
            testThermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);
            testThermometer.CreateThermometerThreshold("Freezing", 0, 0, false, true);
        }

        [TestMethod]
        /* Test that the boiling threshold property of the thermometer has been properly set.*/
        public void TestSettingThresholds()
        {
            //SETUP
            var thermometer = new Thermometer();
            var boilingThreshold = new Threshold("Boiling", 100, 0, true, false);
            var freezingThreshold = new Threshold("Freezing", 0, 0, false, true);
            var expectedThresholds = new List<Threshold>() { { boilingThreshold }, { freezingThreshold } };

            //EXECUTE
            thermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);
            thermometer.CreateThermometerThreshold("Freezing", 0, 0, false, true);

            //ASSERT
            CollectionAssert.AreEquivalent(expectedThresholds, thermometer.thermometerProperties.Thresholds);
        }

        [TestMethod]
        /* Test that the temperature tolerance property of the thermometer has been properly set.*/
        public void TestTemperatureTolerance()
        {
            //SETUP
            var thermometer = new Thermometer();
            var expectedTempTolerance = 0.1;
            double actualTempTolerance;

            //EXECUTE
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTempTolerance, true, false);

            actualTempTolerance = thermometer.thermometerProperties.Thresholds.First().TempTolerance.ToleranceValue;

            //ASSERT
            Assert.AreEqual(expectedTempTolerance, thermometer.thermometerProperties);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Celsius -> Celsius.*/
        public void TestRegisterTemperatureChangeCelsiusToCelsius()
        {
            //SETUP
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5};
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp,measuredUnits,displayUnits);
            }
            readTemps = thermometer.HistoricalTemperatures;

            //ASSERT
            CollectionAssert.AreEqual(testTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Fahrenheit -> Fahrenheit.*/
        public void TestRegisterTemperatureChangeFahrenheitToFahrenheit()
        {
            //SETUP
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testTemps = new List<double>() { 23, 24.8, 26.6, 28.4, 26.6, 24.8, 26.6, 28.4, 30.2, 32, 32, 32, 33.8, 35.6, 37.4, 39.2, 37.4, 35.6, 37.4, 39.2, 41 };

            var readTemps = new List<double>();
            string measuredUnits = "Fahrenheit";
            string displayUnits = "Fahrenheit";

            //EXECUTE
            foreach (var temp in testTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
            }
            readTemps = thermometer.HistoricalTemperatures;

            //ASSERT
            CollectionAssert.AreEqual(testTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Celsius -> Fahrenheit.*/
        public void TestRegisterTemperatureChangeCelsiusToFahrenheit()
        {
            //SETUP
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            var testFahrenheitTemps = new List<double>() { 23, 24.8, 26.6, 28.4, 26.6, 24.8, 26.6, 28.4, 30.2, 32, 32, 32, 33.8, 35.6, 37.4, 39.2, 37.4, 35.6, 37.4, 39.2, 41 };


            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Fahrenheit";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
            }
            readTemps = thermometer.HistoricalTemperatures;

            //ASSERT
            CollectionAssert.AreEqual(testFahrenheitTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Fahrenheit -> Celsius .*/
        public void TestRegisterTemperatureChangeFahrenheitToCelsius()
        {
            //SETUP
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            var testFahrenheitTemps = new List<double>() { 23, 24.8, 26.6, 28.4, 26.6, 24.8, 26.6, 28.4, 30.2, 32, 32, 32, 33.8, 35.6, 37.4, 39.2, 37.4, 35.6, 37.4, 39.2, 41 };


            var readTemps = new List<double>();
            string measuredUnits = "Fahrenheit";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
            }
            readTemps = thermometer.HistoricalTemperatures;

            //ASSERT
            CollectionAssert.AreEqual(testCelsiusTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with conversion: InvalidUnits -> Celsius .*/
        public void TestRegisterTemperatureChangeInvalidToCelsius()
        {
            //SETUP
            string expectedErrorMessage = "Specified cast is not valid.";
            string receivedErrorMessage = "";

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            string measuredUnits = "InvalidUnits";
            string displayUnits = "Celsius";
            
            //EXECUTE
                // Read the temperature from an external source and store it in a list of doubles.
                try
                {
                    // Try to convert the first value from our test data set to Celsius from InvalidUnits.
                    thermometer.RegisterTemperatureChange(testCelsiusTemps[0], measuredUnits, displayUnits);
                }
                catch(Exception ImproperUnits)
                {
                    // Expect the system to throw an error due to incorrect units.
                    receivedErrorMessage = ImproperUnits.Message;
                }

            //ASSERT
            Assert.AreEqual(expectedErrorMessage, receivedErrorMessage);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with conversion: Celsius -> InvalidUnits.*/
        public void TestRegisterTemperatureChangeCelsiusToInvalid()
        {
            //SETUP
            string expectedErrorMessage = "Specified cast is not valid.";
            string receivedErrorMessage = "";

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            string measuredUnits = "Celsius";
            string displayUnits = "InvalidUnits";

            //EXECUTE
            // Read the temperature from an external source and store it in a list of doubles.
            try
            {
                // Try to convert the first value from our test data set to Celsius from InvalidUnits.
                thermometer.RegisterTemperatureChange(testCelsiusTemps[0], measuredUnits, displayUnits);
            }
            catch (Exception ImproperUnits)
            {
                // Expect the system to throw an error due to incorrect units.
                receivedErrorMessage = ImproperUnits.Message;
            }

            //ASSERT
            Assert.AreEqual(expectedErrorMessage, receivedErrorMessage);
        }

        [TestMethod]
        // Test establishing the temperature delta direction for a negative rising value.
        public void TestEstablishDeltaDirectionRisingNegative()
        {
            //SETUP
            bool expectedDeltaDirection = false;
            bool readTempDeltaDirection = true;
            
            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4};
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTempDeltaDirection = thermometer.IsTemperatureFalling();
            }

            //ASSERT
            Assert.AreEqual(expectedDeltaDirection, readTempDeltaDirection);
        }

        [TestMethod]
        // Test establishing the temperature delta direction for a negative rising value.
        public void TestEstablishDeltaDirectionRisingPositive()
        {
            //SETUP
            bool expectedDeltaDirection = false;
            bool readTempDeltaDirection = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 4, 5 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTempDeltaDirection = thermometer.IsTemperatureFalling();
            }

            //ASSERT
            Assert.AreEqual(expectedDeltaDirection, readTempDeltaDirection);
        }

        [TestMethod]
        public void TestEstablishDeltaDirectionFallingNegative()
        {
            //SETUP
            bool expectedDeltaDirection = true;
            bool readTempDeltaDirection = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -4, -5 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTempDeltaDirection = thermometer.IsTemperatureFalling();
            }

            //ASSERT
            Assert.AreEqual(expectedDeltaDirection, readTempDeltaDirection);
        }

        [TestMethod]
        public void TestEstablishDeltaDirectionFallingPositive()
        {
            //SETUP
            bool expectedDeltaDirection = true;
            bool readTempDeltaDirection = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 5, 4 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTempDeltaDirection = thermometer.IsTemperatureFalling();
            }

            //ASSERT
            Assert.AreEqual(expectedDeltaDirection, readTempDeltaDirection);
        }

        [TestMethod]
        public void TestCheckIfBoilingNotBoiling()
        {
            //SETUP
            bool expectedIsBoiling = false;
            bool readIsBoiling = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 90, 99 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readIsBoiling = thermometer.HasThresholdBeenReached();
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
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 90, 100 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readIsBoiling = thermometer.HasThresholdBeenReached();
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
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 2, 1 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readIsFreezing = thermometer.HasThresholdBeenReached();
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
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 0.1, -0.2 };
            var readTemps = new List<double>();
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readIsFreezing = thermometer.HasThresholdBeenReached();
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
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 0.1, -0.2, -0.1, 0, 0.11};
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillFreezing = thermometer.IsThresholdStillReached(thermometer.thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillFreezing, testStillFreezing);
        }

        [TestMethod]
        public void TestCheckIfStillFreezingIsFreezing()
        {
            //SETUP
            bool expectedStillFreezing = true;
            bool testStillFreezing = false;
            
            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 0.1, -0.2, -0.1, 0, 0.09 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillFreezing = thermometer.IsThresholdStillReached(thermometer.thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillFreezing, testStillFreezing);
        }

        [TestMethod]
        public void TestCheckIfStillBoilingNotBoiling()
        {
            //SETUP
            bool expectedStillBoiling = false;
            bool testStillBoiling = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 98, 101, 98 };
            //TODO: remove string & put directly in method call
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillBoiling = thermometer.IsThresholdStillReached(thermometer.thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillBoiling, testStillBoiling);
        }

        [TestMethod]
        public void TestCheckIfStillBoilingIsBoiling()
        {
            //SETUP
            bool expectedStillBoiling = true;
            bool testStillBoiling = false;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            SetupThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { 98, 101, 98 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.StoreCurrentTemperature();
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);

                testStillBoiling = thermometer.IsThresholdStillReached(thermometer.thermometerProperties.Thresholds.First());
            }

            //ASSERT
            Assert.AreEqual(expectedStillBoiling, testStillBoiling);
        }
    }
}
