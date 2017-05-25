using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerNS;
using System.Linq;
using System.Collections.Generic;

namespace ThermometerTestNS
{
    /*[TestClass]
    public class TestDeltaDirection
    {
        SetupResources TestResources = new TestSetupResources();

        [TestMethod]
        // Test establishing the temperature delta direction for a negative rising value.
        public void TestEstablishDeltaDirectionRisingNegative()
        {
            //SETUP
            bool expectedDeltaDirection = false;
            bool readTempDeltaDirection = true;

            // Specify the properties of the thermometer.
            var thermometer = new Thermometer();
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -5, -4 };
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
            TestResources.SetupGenericTestThermometer(thermometer);

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
            TestResources.SetupGenericTestThermometer(thermometer);

            // Establish test data to mimic external temperature readings.
            var testCelsiusTemps = new List<double>() { -4, -5 };
            string measuredUnits = "Celsius";
            string displayUnits = "Celsius";

            //EXECUTE
            foreach (var temp in testCelsiusTemps)
            {
                // Read the temperature from an external source and store it in a list of doubles.
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
            TestResources.SetupGenericTestThermometer(thermometer);

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
    }*/
}
