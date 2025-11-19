namespace SearchTicketApp.Helpers
{
    public static class DistanceCalculator
    {
        public const float EarthRadius = 6371;

        private static float ToRadians(float angle) => MathF.PI * angle / 180.0f;

        /// <summary>
        /// Method for calculating distance between coords on Earth. Haversine formula is used, see:
        /// https://www.geeksforgeeks.org/dsa/haversine-formula-to-find-distance-between-two-points-on-a-sphere/
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static float CalculateDistanceKmBetweenTwoCoords(float lat1, float lon1, float lat2, float lon2)
        {
            float dLat = lat2 - lat1;
            float dLon = lon2 - lon1;

            //expression under square root
            float a = MathF.Pow(MathF.Sin(ToRadians(dLat / 2)), 2) +
                      MathF.Pow(MathF.Sin(ToRadians(dLon / 2)), 2) *
                      MathF.Cos(ToRadians(lat1)) * MathF.Cos(ToRadians(lat2));

            return 2 * MathF.Asin(MathF.Sqrt(a)) * EarthRadius;
        }

    }
}
