using OurSolarSystemAPI.Utility;
using Xunit;
using System;
using System.Collections.Generic;

namespace OurSolarSystemTest
{
    public class SolarSystemCalculatorTests
    {
        [Fact]
        public void CalculateSum_ShouldReturnZeroForEmptyList()
        {
            // Arrange
            var planetAttributes = new List<double>();

            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateSum_ShouldReturnSumOfPositiveNumbers()
        {
            // Arrange
            var planetAttributes = new List<double> { 1.0, 2.0, 3.0 };

            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(6.0, result);
        }

        [Fact]
        public void CalculateSum_ShouldReturnSumOfNegativeNumbers()
        {
            // Arrange
            var planetAttributes = new List<double> { -1.0, -2.0, -3.0 };

            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(-6.0, result);
        }

        [Fact]
        public void CalculateSum_ShouldReturnSumOfMixedNumbers()
        {
            // Arrange
            var planetAttributes = new List<double> { -1.0, 2.0, -3.0, 4.0 };

            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(2.0, result);
        }

        [Fact]
        public void CalculateRatio_ShouldReturnCorrectRatio()
        {
            // Arrange
            double attributeOne = 10.0;
            double attributeTwo = 2.0;

            // Act
            double result = OurSolarSystemCalcutator.CalculateRatio(attributeOne, attributeTwo);

            // Assert
            Assert.Equal(5.0, result);
        }

        [Fact]
        public void CalculateRatio_ShouldHandleDivisionByZero()
        {
            // Arrange
            double attributeOne = 10.0;
            double attributeTwo = 0.0;

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => OurSolarSystemCalcutator.CalculateRatio(attributeOne, attributeTwo));
        }

        [Fact]
        public void ScaleAttribute_ShouldReturnScaledPosition()
        {
            // Arrange
            double position = 10.0;
            double scalingFactor = 2.0;

            // Act
            double result = OurSolarSystemCalcutator.ScaleAttribute(position, scalingFactor);

            // Assert
            Assert.Equal(20.0, result);
        }

        [Fact]
        public void ScaleAttribute_ShouldHandleZeroPosition()
        {
            // Arrange
            double position = 0.0;
            double scalingFactor = 2.0;

            // Act
            double result = OurSolarSystemCalcutator.ScaleAttribute(position, scalingFactor);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void ScaleAttribute_ShouldHandleZeroScalingFactor()
        {
            // Arrange
            double position = 10.0;
            double scalingFactor = 0.0;

            // Act
            double result = OurSolarSystemCalcutator.ScaleAttribute(position, scalingFactor);

            // Assert
            Assert.Equal(0.0, result);
        }
        [Fact]
        public void CalculateEuclideanDistance3dVector_ShouldReturnZeroForIdenticalVectors()
        {
            // Arrange
            double[] vector1 = [1.0, 2.0, 3.0];
            double[] vector2 = [1.0, 2.0, 3.0];

            // Act
            double result = OurSolarSystemCalcutator.CalculateEuclideanDistance3dVector(vector1, vector2);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void CalculateEuclideanDistance3dVector_ShouldReturnCorrectDistance()
        {
            // Arrange
            double[] vector1 = [1.0, 2.0, 3.0];
            double[] vector2 = [4.0, 5.0, 6.0];

            // Act
            double result = OurSolarSystemCalcutator.CalculateEuclideanDistance3dVector(vector1, vector2);

            // Assert
            Assert.Equal(Math.Sqrt(27), result);
        }

    }
}
