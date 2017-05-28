using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerNS;
using System.Collections.Generic;
using System.Linq;

namespace ThermometerTestNS
{
    [TestClass]
    public class TestTemperatureConversion
    {
        readonly SetupResources TestResources = new SetupResources();

        [TestMethod]
        // Test that the temperature is being read properly with no conversion: Celsius -> Celsius.
        public void TestRegisterTemperatureChangeCelsiusToCelsius()
        {
            //SETUP
            var thermometer = new Thermometer();
           TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5};
            var readTemps = new List<double>();
            const string measuredUnits = "Celsius";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTemps.Add(thermometer.CurrentTemperature);
            }

            //ASSERT
            CollectionAssert.AreEqual(testTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Fahrenheit -> Fahrenheit.*/
        public void TestRegisterTemperatureChangeFahrenheitToFahrenheit()
        {
            //SETUP
            var thermometer = new Thermometer();
           TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testTemps = new List<double>() { 23, 24.8, 26.6, 28.4, 26.6, 24.8, 26.6, 28.4, 30.2, 32, 32, 32, 33.8, 35.6, 37.4, 39.2, 37.4, 35.6, 37.4, 39.2, 41 };

            var readTemps = new List<double>();
            const string measuredUnits = "Fahrenheit";
            const string displayUnits = "Fahrenheit";

            //EXECUTE
            foreach (var temp in testTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTemps.Add(thermometer.CurrentTemperature);
            }

            //ASSERT
            CollectionAssert.AreEqual(testTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Celsius -> Fahrenheit.*/
        public void TestRegisterTemperatureChangeCelsiusToFahrenheit()
        {
            //SETUP
            var thermometer = new Thermometer();
           TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            var testFahrenheitTemps = new List<double>() { 23, 24.8, 26.6, 28.4, 26.6, 24.8, 26.6, 28.4, 30.2, 32, 32, 32, 33.8, 35.6, 37.4, 39.2, 37.4, 35.6, 37.4, 39.2, 41 };


            var readTemps = new List<double>();
            const string measuredUnits = "Celsius";
            const string displayUnits = "Fahrenheit";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTemps.Add(thermometer.CurrentTemperature);
            }

            //ASSERT
            CollectionAssert.AreEqual(testFahrenheitTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with no conversion: Fahrenheit -> Celsius .*/
        public void TestRegisterTemperatureChangeFahrenheitToCelsius()
        {
            //SETUP
            var thermometer = new Thermometer();
           TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testFahrenheitTemps = new List<double>() { 23, 24.8, 26.6, 28.4, 26.6, 24.8, 26.6, 28.4, 30.2, 32, 32, 32, 33.8, 35.6, 37.4, 39.2, 37.4, 35.6, 37.4, 39.2, 41 };
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };


            var readTemps = new List<double>();
            const string measuredUnits = "Fahrenheit";
            const string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testFahrenheitTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
                thermometer.RegisterTemperatureChange(temp, measuredUnits, displayUnits);
                readTemps.Add(thermometer.CurrentTemperature);
            }

            //ASSERT
            CollectionAssert.AreEqual(testCelsiusTemps, readTemps);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with conversion: InvalidUnits -> Celsius .*/
        public void TestRegisterTemperatureChangeInvalidToCelsius()
        {
            //SETUP
            const string expectedErrorMessage = "Specified cast is not valid.";
            var receivedErrorMessage = "";

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
           TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            const string measuredUnits = "InvalidUnits";
            const string displayUnits = "Celsius";
            
            //EXECUTE
                // Read the temperature from an external source and store it in a list of doubles.
                try
                {
                    // Try to convert the first value from our test data set to Celsius from InvalidUnits.
                    thermometer.RegisterTemperatureChange(testCelsiusTemps[0], measuredUnits, displayUnits);
                }
                catch(Exception improperUnits)
                {
                    // Expect the system to throw an error due to incorrect units.
                    receivedErrorMessage = improperUnits.Message;
                }

            //ASSERT
            Assert.AreEqual(expectedErrorMessage, receivedErrorMessage);
        }

        [TestMethod]
        /* Test that the temperature is being read properly with conversion: Celsius -> InvalidUnits.*/
        public void TestRegisterTemperatureChangeCelsiusToInvalid()
        {
            //SETUP
            const string expectedErrorMessage = "Specified cast is not valid.";
            var receivedErrorMessage = "";

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
           TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4, -3, -2, -3, -4, -3, -2, -1, 0, 0, 0, 1, 2, 3, 4, 3, 2, 3, 4, 5 };
            const string measuredUnits = "Celsius";
            const string displayUnits = "InvalidUnits";

            //EXECUTE
            // Read the temperature from an external source and store it in a list of doubles.
            try
            {
                // Try to convert the first value from our test data set to Celsius from InvalidUnits.
                thermometer.RegisterTemperatureChange(testCelsiusTemps[0], measuredUnits, displayUnits);
            }
            catch (Exception improperUnits)
            {
                // Expect the system to throw an error due to incorrect units.
                receivedErrorMessage = improperUnits.Message;
            }

            //ASSERT
            Assert.AreEqual(expectedErrorMessage, receivedErrorMessage);
        }
    }
}
