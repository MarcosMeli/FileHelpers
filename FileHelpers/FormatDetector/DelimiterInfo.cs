using System;

namespace FileHelpers.Detection
{
    internal sealed class DelimiterInfo
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Averge { get; set; }

        public char Delimiter { get; set; }
        public double Deviation { get; set; }

        public DelimiterInfo(char delimiter, double average, int max, int min, double deviation)
        {
            Max = max;
            Min = min;
            Delimiter = delimiter;
            Averge = (int)Math.Round(average);
            Deviation = deviation;
        }
    }
}