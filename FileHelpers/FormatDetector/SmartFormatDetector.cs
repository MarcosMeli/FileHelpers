using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers.RunTime;

namespace FileHelpers.Detection
{
    /// <summary>Utility class used to auto detect the record format, the number of fields, the type, etc.</summary>
    public sealed class SmartFormatDetector
    {
        #region "  Constants  "

        private const int MIN_SAMPLE_DATA = 15;
        private const double MIN_DELIMITED_DEVIATION = 0.2;

        #endregion

        #region "  Properties  "

        private FormatHint mFormatHint;

        /// <summary>
        /// Provides a suggestion to the <see cref="SmartFormatDetector"/> about the records in the file
        /// </summary>
        public FormatHint FormatHint
        {
            get { return mFormatHint; }
            set { mFormatHint = value; }
        }

        private int mSampleLines = 50;

        public int SampleLines
        {
            get { return mSampleLines; }
            set { mSampleLines = value; }
        }

        private Encoding mEncoding = Encoding.Default;

        /// <summary>The encoding to Read and Write the streams.</summary>
        /// <remarks>Default is the system's current ANSI code page.</remarks>
        public Encoding Encoding
        {
            get { return mEncoding; }
            set { mEncoding = value; }
        }

        private double mFixedLengthDeviationTolerance = 0.01;

        /// <summary>Used to calculate when a file has fixed length records. Between 0.0 - 1.0 (Default 0.01)</summary>
        public double FixedLengthDeviationTolerance
        {
            get { return mFixedLengthDeviationTolerance; }
            set { mFixedLengthDeviationTolerance = value; }
        }

        #endregion

        #region "  Public Methods  "
        
        public RecordFormatInfo[] DetectFileFormat(string file)
        {
            return DetectFileFormat(new string[] { file });
        }

        public RecordFormatInfo[] DetectFileFormat(IEnumerable<string> files)
        {
            List<RecordFormatInfo> res = new List<RecordFormatInfo>();
            string[][] sampleData = GetSampleLines(files, SampleLines);

            switch (mFormatHint)
            {
                case FormatHint.Unknown:
                    CreateMixedOptions(sampleData, res);
                    break;
                case FormatHint.FixedLength:
                    CreateFixedLengthOptions(sampleData, res);
                    break;
                case FormatHint.Delimited:
                    CreateDelimiterOptions(sampleData, res);
                    break;
                case FormatHint.DelimitedByTab:
                    CreateDelimiterOptions(sampleData, res, '\t');
                    break;
                case FormatHint.DelimitedByComma:
                    CreateDelimiterOptions(sampleData, res, ',');
                    break;
                case FormatHint.DelimitedBySemicolon:
                    CreateDelimiterOptions(sampleData, res, ';');
                    break;
                default:
                    throw new InvalidOperationException("Unsuported FormatHint value.");
            }

            foreach (RecordFormatInfo option in res)
            {
                DetectOptionals(option);
                DetectTypes(option);
                DetectQuoted(option);
            }

            // Sort by confidence
            res.Sort(delegate(RecordFormatInfo x, RecordFormatInfo y)
                    { return -1 * x.Confidence.CompareTo(y.Confidence); });

            return res.ToArray();
        }

        private void DetectOptionals(RecordFormatInfo option)
        {
            
        }

        #endregion

        #region "  Fields Properties Methods  "


        private void DetectQuoted(RecordFormatInfo format)
        {
        }

        private void DetectTypes(RecordFormatInfo format)
        {
        }

        #endregion

        #region "  Create Options Methods  "


        // UNKNOWN
        private void CreateMixedOptions(string[][] data, List<RecordFormatInfo> res)
        {
            double average = CalculateAverageLineWidth(data);
            double deviation = CalculateDeviationLineWidth(data, average);
        
            if (deviation / average <= FixedLengthDeviationTolerance * Math.Min(1, NumberOfLines(data) / MIN_SAMPLE_DATA))
                CreateFixedLengthOptions(data, res);

            CreateDelimiterOptions(data, res);

            //if (deviation > average * 0.01 &&
            //    deviation < average * 0.05)
            //    CreateFixedLengthOptions(data, res);

        }


        // FIXED LENGTH
        private void CreateFixedLengthOptions(string[][] data, List<RecordFormatInfo> res)
        {
            RecordFormatInfo format = new RecordFormatInfo();
            double average = CalculateAverageLineWidth(data);
            double deviation = CalculateDeviationLineWidth(data, average);

            format.mConfidence = (int)(Math.Max(0, 1 - deviation / average) * 100);

            FixedLengthClassBuilder builder = new FixedLengthClassBuilder("AutoDetectedClass");
            CreateFixedLengthFields(data, builder);

            format.mClassBuilder = builder;

            res.Add(format);
        }

        private void CreateFixedLengthFields(string[][] data, FixedLengthClassBuilder builder)
        {

        }

        // DELIMITED
        private void CreateDelimiterOptions(string[][] sampleData, List<RecordFormatInfo> res)
        {
            CreateDelimiterOptions(sampleData, res, '\0');
        }

        private void CreateDelimiterOptions(string[][] sampleData, List<RecordFormatInfo> res, char delimiter)
        {
            List<DelimiterInfo> delimiters = new List<DelimiterInfo>();

            if (delimiter == '\0')
                delimiters = GetDelimiters(sampleData);
            else
                delimiters.Add(GetDelimiterInfo(sampleData, delimiter));

            foreach (DelimiterInfo info in delimiters)
            {
                RecordFormatInfo format = new RecordFormatInfo();
                format.mConfidence = (int)((1 - info.Deviation ) * 100);
                AdjustConfidence(format, info);

                DelimitedClassBuilder builder = new DelimitedClassBuilder("AutoDetectedClass", info.Delimiter.ToString());
                builder.AddFields(info.AvergeByLine + 1);

                format.mClassBuilder = builder;

                res.Add(format);
            }

        }

        private void AdjustConfidence(RecordFormatInfo format, DelimiterInfo info)
        {
            switch (info.Delimiter)
            {

                case '/': // Avoid the date delimiters and url to be selected
                case '.': // Avoid the decimal separator to be selected
                    format.mConfidence = (int)(format.Confidence * 0.4);
                    break;


                case '@': // Avoid the mails separator to be selected
                case '&': // Avoid this is near a letter and URLS
                case '=': // Avoid because URLS contains it
                    format.mConfidence = (int)(format.Confidence * 0.6);
                    break;

                case '-': // Avoid this other date separator
                    format.mConfidence = (int)(format.Confidence * 0.7);
                    break;

            }
        }

        #endregion

        #region "  Helper & Utility Methods  "
        
        private string[][] GetSampleLines(IEnumerable<string> files, int nroOfLines)
        {
            List<string[]> res = new List<string[]>();

            foreach (string file in files)
            {
                res.Add(CommonEngine.RawReadFirstLinesArray(file, nroOfLines, mEncoding));
            }

            return res.ToArray();
        }

        private int NumberOfLines(string[][] data)
        {
            int lines = 0;
            foreach (string[] fileData in data)
            {
                lines += fileData.Length;
            }
            return lines;
        }


        private DelimiterInfo GetDelimiterInfo(string[][] data, char delimiter)
        {

            double average = CalculateAverage(delimiter, data);
            double deviation = CalculateDeviation(delimiter, data, average);

            return new DelimiterInfo(delimiter, (int)Math.Round(average), deviation);

        }

        private List<DelimiterInfo> GetDelimiters(string[][] data)
        {
            Dictionary<char, int> frecuency = new Dictionary<char, int>();
            int lines = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (j == 0) continue; // Ignore Header Line (if any)

                    lines++;

                    string line = data[i][j];
                    for (int ci = 0; ci < line.Length; ci++)
                    {
                        char c = line[ci];

                        if (Char.IsLetterOrDigit(c)
                            || c == ' ')
                            continue;

                        int count;
                        if (frecuency.TryGetValue(c, out count))
                        {
                            count++;
                            frecuency[c] = count;
                        }
                        else
                            frecuency.Add(c, 1);
                    }
                }
            }


            List<DelimiterInfo> candidates = new List<DelimiterInfo>();
            foreach (KeyValuePair<char, int> pair in frecuency)
            {
                double average = CalculateAverage(pair.Key, data);
                double deviation = CalculateDeviation(pair.Key, data, average);

                // Adjust based on the number of lines
                deviation = deviation * Math.Min(1, ((double) lines)/MIN_SAMPLE_DATA);

                if (average > 1 && deviation < MIN_DELIMITED_DEVIATION)
                    candidates.Add(new DelimiterInfo(pair.Key, (int)Math.Round(average), deviation));
            }

            return candidates;
        }


        #endregion

        #region "  Statistics Functions  "

        private double CalculateDeviation(char c, string[][] data, double average)
        {
            double bigSum = 0.0;
            int lines = 0;
            foreach (string[] fileData in data)
            {
                foreach (string line in fileData)
                {
                    lines++;

                    int sum = 0;
                    foreach (char candidate in line)
                    {
                        if (candidate == c)
                            sum += 1;
                    }

                    bigSum = Math.Pow(sum - average, 2);
                }
            }

            bigSum = bigSum / lines;
            bigSum = Math.Sqrt(bigSum);

            return bigSum;

        }

        private double CalculateAverage(char c, string[][] data)
        {
            double sum = 0;
            int lines = 0;

            foreach (string[] fileData in data)
            {
                foreach (string line in fileData)
                {
                    lines++;

                    foreach (char candidate in line)
                    {
                        if (candidate == c)
                            sum += 1;
                    }

                }
            }

            return sum / lines;
        }

        private double CalculateAverageLineWidth(string[][] data)
        {
            double sum = 0;
            int lines = 0;

            foreach (string[] fileData in data)
            {
                foreach (string line in fileData)
                {
                    lines++;
                    sum += line.Length;
                }
            }

            return sum / lines;
        }

        private double CalculateDeviationLineWidth(string[][] data, double average)
        {
            double bigSum = 0.0;
            int lines = 0;

            foreach (string[] fileData in data)
            {
                foreach (string line in fileData)
                {
                    lines++;
                    bigSum = Math.Pow(line.Length - average, 2);
                }
            }

            bigSum = bigSum / lines;
            bigSum = Math.Sqrt(bigSum);

            return bigSum;

        }

        #endregion

    }
}
