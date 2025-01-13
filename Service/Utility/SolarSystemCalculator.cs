
namespace OurSolarSystemAPI.Utility
{
    public static class OurSolarSystemCalcutator
    {
        public static double CalculateSum(List<Double> planetAttributes)
        {
            double sum = 0;

            foreach (double attribute in planetAttributes)
            {
                sum += attribute;
            }
            return sum;
        }

        public static double CalculateRatio(double attributeOne, double attributeTwo)
        {
            if (attributeOne == 0 || attributeTwo == 0)
            {
                throw new DivideByZeroException();
            }
            return attributeOne / attributeTwo;
        }
        public static double ScaleAttribute(double position, double scalingFactor)
        {
            double scaledPosition = position * scalingFactor;

            return scaledPosition;
        }

        // calculates the distance between two points in 3d space: https://en.wikipedia.org/wiki/Euclidean_distance 
        public static double CalculateEuclideanDistance3dVector(double[] vector1, double[] vector2)
        {
            double sumOfSquaredDifferences =
                Math.Pow(vector1[0] - vector2[0], 2) +
                Math.Pow(vector1[1] - vector2[1], 2) +
                Math.Pow(vector1[2] - vector2[2], 2);

            return Math.Sqrt(sumOfSquaredDifferences);
        }

    }
}