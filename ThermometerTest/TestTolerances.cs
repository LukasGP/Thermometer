using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerNS;
using System.Linq;

namespace ThermometerTestNS
{
    [TestClass]
    public class TestTolerances
    {
        [TestMethod]
        // Test that the boiling threshold has been properly set.
        public void TestEstablishingTempTolerance()
        {
            //SETUP
            const double expectedTolerance = 0;
            var thermometer = new Thermometer();

            //EXECUTE
            thermometer.CreateThermometerThreshold("NotBoiling", 99, 0.1, false, true);
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTolerance, true, false);
            var testTolerance = thermometer.ThermometerProperties.Thresholds.First(x => x.ThresholdName == "Boiling").TempTolerance.ToleranceValue;

            //ASSERT
            Assert.AreEqual(expectedTolerance, testTolerance);
        }

        [TestMethod]
        // Test that the upper band of the boiling threshold has been properly set.
        public void TestEstablishingToleranceUpperBand()
        {
            //SETUP
            const double expectedTolerance = 5;
            const double expectedToleranceUpperBand = 105;
            var thermometer = new Thermometer();

            //EXECUTE
            thermometer.CreateThermometerThreshold("NotBoiling", 99, 0, false, true);
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTolerance, true, false);
            var testToleranceUpperBand = thermometer.ThermometerProperties.Thresholds.First(x => x.ThresholdName == "Boiling").TempTolerance.UpperBand;

            //ASSERT
            Assert.AreEqual(expectedToleranceUpperBand, testToleranceUpperBand);
        }

        [TestMethod]
        // Test that the lower band of the boiling threshold has been properly set.
        public void TestEstablishingToleranceLowerBand()
        {
            //SETUP
            const double expectedTolerance = 5;
            const double expectedToleranceLowerBand = 95;
            var thermometer = new Thermometer();

            //EXECUTE
            thermometer.CreateThermometerThreshold("NotBoiling", 99, 0, false, true);
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTolerance, true, false);
            var testToleranceUpperBand = thermometer.ThermometerProperties.Thresholds.First(x => x.ThresholdName == "Boiling").TempTolerance.LowerBand;

            //ASSERT
            Assert.AreEqual(expectedToleranceLowerBand, testToleranceUpperBand);
        }
    }
}
