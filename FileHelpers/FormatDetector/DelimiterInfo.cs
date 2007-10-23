namespace FileHelpers.Detection
{
    internal sealed class DelimiterInfo
    {
        public char Delimiter;
        public int AvergeByLine;
        public double Deviation;

        public DelimiterInfo(char delimiter, int average, double deviation)
        {
            Delimiter = delimiter;
            AvergeByLine = average;
            Deviation = deviation;
        }
    }
}