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
        /* Test that the boiling threshold property of the thermometer has been properly set.*/
        public void TestEstablishingTempTolerance()
        {
            //SETUP
            double expectedTolerance = 0;
            double testTolerance;
            var thermometer = new Thermometer();
            var boilingThreshold = new Threshold("Boiling", 100, expectedTolerance, true, false);

            //EXECUTE
            thermometer.CreateThermometerThreshold("NotBoiling", 99, 0.1, false, true);
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTolerance, true, false);
            testTolerance = thermometer._thermometerProperties.Thresholds.Where(x => x.ThresholdName == "Boiling").First().TempTolerance.ToleranceValue;

            //ASSERT
            Assert.AreEqual(expectedTolerance, testTolerance);
        }

        [TestMethod]
        /* Test that the boiling threshold property of the thermometer has been properly set.*/
        public void TestEstablishingToleranceUpperBand()
        {
            //SETUP
            double expectedTolerance = 0.1;
            double expectedToleranceUpperBand = 110;
            double testToleranceUpperBand;
            var thermometer = new Thermometer();
            var boilingThreshold = new Threshold("Boiling", 100, expectedTolerance, true, false);

            //EXECUTE
            thermometer.CreateThermometerThreshold("NotBoiling", 99, 0, false, true);
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTolerance, true, false);
            testToleranceUpperBand = thermometer._thermometerProperties.Thresholds.Where(x => x.ThresholdName == "Boiling").First().TempTolerance.UpperBand;

            //ASSERT
            Assert.AreEqual(expectedToleranceUpperBand, testToleranceUpperBand);
        }

        [TestMethod]
        /* Test that the boiling threshold property of the thermometer has been properly set.*/
        public void TestEstablishingToleranceLowerBand()
        {
            //SETUP
            double expectedTolerance = 0.1;
            double expectedToleranceUpperBand = 90;
            double testToleranceUpperBand;
            var thermometer = new Thermometer();
            var boilingThreshold = new Threshold("Boiling", 100, expectedTolerance, true, false);

            //EXECUTE
            thermometer.CreateThermometerThreshold("NotBoiling", 99, 0, false, true);
            thermometer.CreateThermometerThreshold("Boiling", 100, expectedTolerance, true, false);
            testToleranceUpperBand = thermometer._thermometerProperties.Thresholds.Where(x => x.ThresholdName == "Boiling").First().TempTolerance.LowerBand;

            //ASSERT
            Assert.AreEqual(expectedToleranceUpperBand, testToleranceUpperBand);
        }
    }
}
