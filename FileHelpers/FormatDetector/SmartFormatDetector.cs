using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FileHelpers.Dynamic;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Utility class used to auto detect the record format,
    /// the number of fields, the type, etc.
    /// </summary>
    public sealed class SmartFormatDetector
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFormatDetector"/> class.
        /// </summary>
        public SmartFormatDetector()
        {
            QuotedChar = '"';
        }

        #region "  Constants  "

        private const int MIN_SAMPLE_DATA = 15;
        private const double MIN_DELIMITED_DEVIATION = 0.2;

        #endregion

        #region "  Properties  "

        private FormatHint mFormatHint;

        /// <summary>
        /// Provides a suggestion to the <see cref="SmartFormatDetector"/> 
        /// about the records in the file
        /// </summary>
        public FormatHint FormatHint
        {
            get { return mFormatHint; }
            set { mFormatHint = value; }
        }

        private int mMaxSampleLines = 50;

        /// <summary>
        /// The number of lines of each file to be used as sample data.
        /// </summary>
        public int MaxSampleLines
        {
            get { return mMaxSampleLines; }
            set { mMaxSampleLines = value; }
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
        
        ///<summary>
        ///Indicates if the sample file has headers
        ///</summary>
        public bool FileHasHeaders { get; set; }

        /// <summary>
        /// Used to calculate when a file has fixed length records. 
        /// Between 0.0 - 1.0 (Default 0.01)
        /// </summary>
        public double FixedLengthDeviationTolerance
        {
            get { return mFixedLengthDeviationTolerance; }
            set { mFixedLengthDeviationTolerance = value; }
        }

        #endregion

        #region "  Public Methods  "
        
        /// <summary>
        /// Tries to detect the possible formats of the file using the <see cref="FormatHint"/>
        /// </summary>
        /// <param name="file">The file to be used as sample data</param>
        /// <returns>The possible <see cref="RecordFormatInfo"/> of the file.</returns>
        public RecordFormatInfo[] DetectFileFormat(string file)
        {
            return DetectFileFormat(new string[] { file });
        }

        /// <summary>
        /// Tries to detect the possible formats of the file using the <see cref="FormatHint"/>
        /// </summary>
        /// <param name="files">The files to be used as sample data</param>
        /// <returns>The possible <see cref="RecordFormatInfo"/> of the file.</returns>
        public RecordFormatInfo[] DetectFileFormat(IEnumerable<string> files)
        {
            List<RecordFormatInfo> res = new List<RecordFormatInfo>();
            string[][] sampleData = GetSampleLines(files, MaxSampleLines);

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
                DetectOptionals(option, sampleData);
                DetectTypes(option, sampleData);
                DetectQuoted(option, sampleData);
            }

            // Sort by confidence
            res.Sort(delegate(RecordFormatInfo x, RecordFormatInfo y)
                    { return -1 * x.Confidence.CompareTo(y.Confidence); });

            return res.ToArray();
        }


        #endregion

        #region "  Fields Properties Methods  "


        private void DetectQuoted(RecordFormatInfo format, string[][] data)
        {
            if (format.ClassBuilder is FixedLengthClassBuilder)
                return;





        }

        private void DetectTypes(RecordFormatInfo format, string[][] data)
        {
        }

        private void DetectOptionals(RecordFormatInfo option, string[][] data)
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

        private class FixedColumnInfo
        {
            public int Start;
            public int Length;
            public byte Confidence;
        }

        private void CreateFixedLengthFields(string[][] data, FixedLengthClassBuilder builder)
        {
            List<FixedColumnInfo> res = null;
            
            foreach (string[] dataFile in data)
            {
                List<FixedColumnInfo> candidates = CreateFixedLengthCandidates(dataFile);
                res = JoinFixedColCandidates(res, candidates);
            }

            for (int i = 0; i < res.Count; i++)
			{
                FixedColumnInfo col = res[i];
                builder.AddField("Field" + i.ToString().PadLeft(4, '0'), col.Length, typeof(string));
            }
        }

        private List<FixedColumnInfo> CreateFixedLengthCandidates(string[] lines)
        {
            List<FixedColumnInfo> res = null;

            foreach (string line in lines)
            {
                List<FixedColumnInfo> candidates = new List<FixedColumnInfo>();
                int filled = 0;
                int blanks = 0;

                FixedColumnInfo col = null;
                for (int i = 1; i < line.Length; i++)
                {
                    if (Char.IsWhiteSpace(line[i]))
                    {
                        blanks += 1;
                    }
                    else
                    {
                        if (blanks > 2)
                        {
                            if (col == null)
                            {
                                col = new FixedColumnInfo();
                                col.Start = 0;
                                col.Length = i;
                            }
                            else
                            {
                                FixedColumnInfo prevCol = col;
                                col = new FixedColumnInfo();
                                col.Start = prevCol.Start + prevCol.Length;
                                col.Length = i - col.Start;
                            }
                            candidates.Add(col);
                            // col anterior termina
                            filled = 1;
                            blanks = 0;
                        }
                    }
                        

                }

                if (col == null)
                {
                    col = new FixedColumnInfo();
                    col.Start = 0;
                    col.Length = line.Length;
                }
                else
                {
                    FixedColumnInfo prevCol = col;
                    col = new FixedColumnInfo();
                    col.Start = prevCol.Start + prevCol.Length;
                    col.Length = line.Length - col.Start;
                }

                candidates.Add(col);


                res = JoinFixedColCandidates(res, candidates);
            }

            return res;
        }

        private List<FixedColumnInfo> JoinFixedColCandidates(List<FixedColumnInfo> cand1, List<FixedColumnInfo> cand2)
        {
            if (cand1 == null) return cand2;
            if (cand2 == null) return cand1;

            // Merge the result based on confidence
            return cand1;


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
                builder.IgnoreFirstLines = FileHasHeaders ? 1 : 0;
                var firstLineSplitted = sampleData[0][0].Split(info.Delimiter);
                for (int i = 0; i < info.Max + 1; i++)
                {
                    string name = "Field " + (i + 1).ToString().PadLeft(3, '0');
                    if (FileHasHeaders && i < firstLineSplitted.Length)
                        name = firstLineSplitted[i];

                    var f = builder.AddField(StringHelper.ToValidIdentifier(name));
                    if (i > info.Min)
                        f.FieldOptional = true;
                }
                

                format.mClassBuilder = builder;

                res.Add(format);
            }

        }

        private void AdjustConfidence(RecordFormatInfo format, DelimiterInfo info)
        {
            switch (info.Delimiter)
            {

                case '"':  // Avoid the quote identifier
                case '\'': // Avoid the quote identifier
                    format.mConfidence = (int)(format.Confidence * 0.2);
                    break;

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

                case ',': // Help the , ; tab to be confident
                case ';': 
                case '\t': 
                    format.mConfidence = (int)Math.Min(100, format.Confidence * 1.15);
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

            var indicators = CalculateIndicators(delimiter, data);
            double deviation = CalculateDeviation(delimiter, data, indicators.Avg);

            return new DelimiterInfo(delimiter, indicators.Avg, indicators.Max, indicators.Min, deviation);

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
                var indicators = CalculateIndicators(pair.Key, data);
                double deviation = CalculateDeviation(pair.Key, data, indicators.Avg);

                // Adjust based on the number of lines
                deviation = deviation * Math.Min(1, ((double) lines)/MIN_SAMPLE_DATA);

                if (indicators.Avg > 1 && deviation < MIN_DELIMITED_DEVIATION)
                    candidates.Add(new DelimiterInfo(pair.Key, indicators.Avg, indicators.Max, indicators.Min, deviation));
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

        private class Indicators
        {
            public int Max = int.MinValue;
            public int Min = int.MaxValue;
            public double Avg = 0;
        }

        private Indicators CalculateIndicators(char c, string[][] data)
        {
            var res = new Indicators();
            int totalDelimiters = 0;
            int lines = 0;

            foreach (string[] fileData in data)
            {
                
                foreach (string line in fileData)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    
                    lines++;

                    var delimiterInLine = QuoteHelper.CountNumberOfDelimiters(line, c, QuotedChar);

                    if (delimiterInLine > res.Max)
                        res.Max = delimiterInLine;

                    if (delimiterInLine < res.Min)
                        res.Min = delimiterInLine;

                    totalDelimiters += delimiterInLine;
                }
                
            }

            res.Avg = totalDelimiters / (double) lines;

            return res;
        }

        /// <summary>
        /// Gets or sets the quoted char.
        /// </summary>
        /// <value>The quoted char.</value>
        protected char QuotedChar { get; set; }

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
    
    internal static class QuoteHelper
    {
        public static int CountNumberOfDelimiters(string line, char delimiter, char quotedChar)
        {
           // Debug.Assert(false, "TODO: This dont work yet");

            int delimitersInLine = 0;
            var restOfLine = line;
            while (!string.IsNullOrEmpty(restOfLine))
            {
                if (restOfLine.StartsWith(quotedChar.ToString()))
                {
                    restOfLine = DiscartUntilQuotedChar(restOfLine, quotedChar);
                }
                else
                {
                    var index = restOfLine.IndexOf(delimiter);
                    if (index < 0)
                        return delimitersInLine;
                    else
                    {
                        delimitersInLine++;
                        restOfLine = restOfLine.Substring(index + 1);
                    }
                }

            }

            return delimitersInLine;
        }

        private static string DiscartUntilQuotedChar(string line, char quoteChar)
        {
            if (line.StartsWith(quoteChar.ToString()))
                line = line.Substring(1);
            
            var index = line.IndexOf(quoteChar);
            if (index < 0)
                return string.Empty;
            else
                return line.Substring(index + 1);
        }
    }
}
