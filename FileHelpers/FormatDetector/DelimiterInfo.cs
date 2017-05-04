using System;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Collect statistic for the different delimitters
    /// </summary>
    internal sealed class DelimiterInfo
    {
        /// <summary>
        /// Max fields
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Miminum fields found
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Average number of fields found per record
        /// </summary>
        public int Average { get; set; }

        /// <summary>
        /// delimiter we are testing data with
        /// </summary>
        public char Delimiter { get; set; }

        /// <summary>
        /// Standard deviation with the data
        /// </summary>
        public double Deviation { get; set; }

        /// <summary>
        /// Create delimitter statistics
        /// </summary>
        /// <param name="delimiter">delimitter we are testing with</param>
        /// <param name="average">average number of fields</param>
        /// <param name="max">maximum fields found</param>
        /// <param name="min">minimum fields found</param>
        /// <param name="deviation">standard deviation</param>
        public DelimiterInfo(char delimiter, double average, int max, int min, double deviation)
        {
            Max = max;
            Min = min;
            Delimiter = delimiter;
            Average = (int) Math.Round(average);
            Deviation = deviation;
        }
    }
}