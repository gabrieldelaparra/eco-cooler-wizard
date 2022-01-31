using eco_cooler_wizard.Core;

using FluentAssertions;

using NUnit.Framework;

namespace eco_cooler_wizard.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGenerateCoolerInchesPerSample()
        {
            var generator = new CoolerGeneratorInches();
            var actual = generator.GenerateCooler(31, 36);

            var expected = new Cooler()
            {
                Width = 31,
                Height = 36,
                Rows = 7,
                Columns = 6,
                MarginBottom = 3,
                MarginLeft = 3,
                MarginRight = 3,
                MarginTop = 3,
            };

            Assert.IsNotNull(actual);

            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void TestGenerateCoolerInchesPerSamplePlusHalfWidth()
        {
            var generator = new CoolerGeneratorInches();
            var actual = generator.GenerateCooler(31.6, 36);

            var expected = new Cooler()
            {
                Width = 31.6,
                Height = 36,
                Rows = 7,
                Columns = 6,
                MarginBottom = 3,
                MarginLeft = 3.3,
                MarginRight = 3.3,
                MarginTop = 3,
            };

            Assert.IsNotNull(actual);

            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void TestGenerateCoolerInchesPerSamplePlusHalfHeight()
        {
            var generator = new CoolerGeneratorInches();
            var actual = generator.GenerateCooler(31, 36.8);

            var expected = new Cooler()
            {
                Width = 31,
                Height = 36.8,
                Rows = 7,
                Columns = 6,
                MarginBottom = 3.4,
                MarginLeft = 3,
                MarginRight = 3,
                MarginTop = 3.4,
            };

            Assert.IsNotNull(actual);

            expected.Should().BeEquivalentTo(actual);
        }
    }
}