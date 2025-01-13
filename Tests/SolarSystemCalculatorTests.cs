using OurSolarSystemAPI.Utility;
using Xunit;
using System;
using System.Collections.Generic;

namespace OurSolarSystemAPI
{
    public class SolarSystemCalculatorTests
    {
        [Theory]
        [InlineData(new double[] { })]
        [InlineData(new double[] { 0.0,})]
        public void CalculateSum_ShouldReturnZeroForEmptyList(double[] planetAttributes)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(0, result);
        }
        [Theory]
        [InlineData(new double[] { 1.0, 2.0, 3.0 }, 6.0)]
        [InlineData(new double[] { 4.0, 5.0, 6.0 }, 15.0)]
        public void CalculateSum_ShouldReturnSumOfPositiveNumbers(double[] planetAttributes, double expectedSum)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(expectedSum, result);
        }

        [Theory]
        [InlineData(new double[] { -1.0, -2.0, -3.0 }, -6.0)]
        [InlineData(new double[] { -4.0, -5.0, -6.0 }, -15.0)]
        public void CalculateSum_ShouldReturnSumOfNegativeNumbers(double[] planetAttributes, double expectedSum)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(expectedSum, result);
        }

        [Theory]
        [InlineData(new double[] { -1.0, 2.0, -3.0, 4.0 }, 2.0)]
        [InlineData(new double[] { -4.0, 5.0, -6.0, 7.0 }, 2.0)]
        public void CalculateSum_ShouldReturnSumOfMixedNumbers(double[] planetAttributes, double expectedSum)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateSum(planetAttributes);

            // Assert
            Assert.Equal(expectedSum, result);
        }

        [Theory]
        [InlineData(10.0, 2.0, 5.0)]
        [InlineData(20.0, 4.0, 5.0)]
        public void CalculateRatio_ShouldReturnCorrectRatio(double attributeOne, double attributeTwo, double expectedRatio)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateRatio(attributeOne, attributeTwo);

            // Assert
            Assert.Equal(expectedRatio, result);
        }

        [Theory]
        [InlineData(10.0, 0.0)]
        [InlineData(20.0, 0.0)]
        public void CalculateRatio_ShouldHandleDivisionByZero(double attributeOne, double attributeTwo)
        {
            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => OurSolarSystemCalcutator.CalculateRatio(attributeOne, attributeTwo));
        }

        [Theory]
        [InlineData(10.0, 2.0, 20.0)]
        [InlineData(5.0, 3.0, 15.0)]
        public void ScaleAttribute_ShouldReturnScaledPosition(double position, double scalingFactor, double expectedScaledPosition)
        {
            // Act
            double result = OurSolarSystemCalcutator.ScaleAttribute(position, scalingFactor);

            // Assert
            Assert.Equal(expectedScaledPosition, result);
        }

        [Theory]
        [InlineData(0.0, 2.0, 0.0)]
        [InlineData(0.0, 3.0, 0.0)]
        public void ScaleAttribute_ShouldHandleZeroPosition(double position, double scalingFactor, double expectedScaledPosition)
        {
            // Act
            double result = OurSolarSystemCalcutator.ScaleAttribute(position, scalingFactor);

            // Assert
            Assert.Equal(expectedScaledPosition, result);
        }

        [Theory]
        [InlineData(10.0, 0.0, 0.0)]
        [InlineData(5.0, 0.0, 0.0)]
        public void ScaleAttribute_ShouldHandleZeroScalingFactor(double position, double scalingFactor, double expectedScaledPosition)
        {
            // Act
            double result = OurSolarSystemCalcutator.ScaleAttribute(position, scalingFactor);

            // Assert
            Assert.Equal(expectedScaledPosition, result);
        }

        [Theory]
        [InlineData(new double[] { 1.0, 2.0, 3.0 }, new double[] { 1.0, 2.0, 3.0 }, 0.0)]
        [InlineData(new double[] { 4.0, 5.0, 6.0 }, new double[] { 4.0, 5.0, 6.0 }, 0.0)]
        public void CalculateEuclideanDistance3dVector_ShouldReturnZeroForIdenticalVectors(double[] vector1, double[] vector2, double expectedDistance)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateEuclideanDistance3dVector(vector1, vector2);

            // Assert
            Assert.Equal(expectedDistance, result);
        }

        [Theory]
        [InlineData(new double[] { 1.0, 2.0, 3.0 }, new double[] { 4.0, 5.0, 6.0 }, 5.196152422706632)]
        [InlineData(new double[] { 0.0, 0.0, 0.0 }, new double[] { 1.0, 1.0, 1.0 }, 1.7320508075688772)] 
        public void CalculateEuclideanDistance3dVector_ShouldReturnCorrectDistance(double[] vector1, double[] vector2, double expectedDistance)
        {
            // Act
            double result = OurSolarSystemCalcutator.CalculateEuclideanDistance3dVector(vector1, vector2);

            // Assert
            Assert.Equal(expectedDistance, result);
        }
    }
}
