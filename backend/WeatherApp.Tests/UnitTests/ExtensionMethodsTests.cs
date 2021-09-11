using NUnit.Framework;
using WeatherApp.Extensions;

namespace WeatherApp.Tests.UnitTests
{
    public class ExtensionMethodsTests
    {
        [TestCase("abc", "a")]
        [TestCase("abc", "b")]
        [TestCase("abc", "c")]
        [TestCase(" test ", "test")]
        [TestCase("test", " test ")]
        [TestCase(" test ", " test ")]
        [TestCase("TEST", "test")]
        [TestCase("test", "TEST")]
        [TestCase("TEST", "TEST")]
        public void Like_SearchIsContainedInText_ReturnsTrue(string text, string search)
        {
            Assert.IsTrue(text.Like(search));
        }

        [TestCase("abc", "")]
        [TestCase("test", "abc")]
        [TestCase("abc", "cba")]
        [TestCase(" ", "")]
        [TestCase("", " ")]
        public void Like_SearchIsNotContainedInText_ReturnsTrue(string text, string search)
        {
            Assert.IsFalse(text.Like(search));
        }

        [TestCase(0, -273.15)]
        [TestCase(273.15, 0)]
        [TestCase(373.15, 100)]
        public void KelvinToCelsius_KelvinValue_ReturnsInCelsius(decimal kelvin, decimal celsius)
        {
            Assert.AreEqual(celsius, kelvin.KelvinToCelsius());
        }

        [TestCase(360, "N")]
        [TestCase(0, "N")]
        [TestCase(22.5, "NNE")]
        [TestCase(45, "NE")]
        [TestCase(67.5, "ENE")]
        [TestCase(90, "E")]
        [TestCase(112.5, "ESE")]
        [TestCase(135, "SE")]
        [TestCase(157.5, "SSE")]
        [TestCase(180, "S")]
        [TestCase(202.5, "SSW")]
        [TestCase(225, "SW")]
        [TestCase(247.5, "WSW")]
        [TestCase(270, "W")]
        [TestCase(292.5, "WNW")]
        [TestCase(315, "NW")]
        [TestCase(337.5, "NNW")]
        public void DegreesToCardinal_DegreesDirection_ReturnsInCompassWording(decimal direction, string compass)
        {
            Assert.AreEqual(compass, direction.DegreesToCardinal());
        }

        [TestCase(0, 0)]
        [TestCase(1, 3.6)]
        [TestCase(7660, 27_576)] // orbital velocity of ISS
        public void MpsToKmph_MetersPerSecond_ReturnsInKilometerPerHour(decimal mps, decimal kmph)
        {
            Assert.AreEqual(kmph, mps.MpsToKmph());
        }
    }
}
