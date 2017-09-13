using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers.Dynamic;
using System.IO;
using FileHelpers.Helpers;

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

        private const int MinSampleData = 10;
        private const double MinDelimitedDeviation = 0.30001;

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

        private int mMaxSampleLines = 300;

        /// <summary>
        /// The number of lines of each file to be used as sample data.
        /// </summary>
        public int MaxSampleLines
        {
            get { return mMaxSampleLines; }
            set { mMaxSampleLines = value; }
        }

        private Encoding mEncoding = Encoding.GetEncoding(0);

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
        public bool? FileHasHeaders { get; set; }

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
            return DetectFileFormat(new string[] {file});
        }

        /// <summary>
        /// Tries to detect the possible formats of the file using the <see cref="FormatHint"/>
        /// </summary>
        /// <param name="files">The files to be used as sample data</param>
        /// <returns>The possible <see cref="RecordFormatInfo"/> of the file.</returns>
        public RecordFormatInfo[] DetectFileFormat(IEnumerable<string> files)
        {
            var readers = new List<TextReader>();
            foreach (var file in files)
            {
                readers.Add(new StreamReader(file, Encoding));
            }
            var res = DetectFileFormat(readers);

            foreach (var reader in readers)
            {
                reader.Close();
            }

            return res;
        }
    

    /// <summary>
        /// Tries to detect the possible formats of the file using the <see cref="FormatHint"/>
        /// </summary>
        /// <param name="files">The files to be used as sample data</param>
        /// <returns>The possible <see cref="RecordFormatInfo"/> of the file.</returns>
        public RecordFormatInfo[] DetectFileFormat(IEnumerable<TextReader> files)
        {
            var res = new List<RecordFormatInfo>();
            string[][] sampleData = GetSampleLines(files, MaxSampleLines);

            switch (mFormatHint) {
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

            foreach (var option in res) {
                DetectOptionals(option, sampleData);
                DetectTypes(option, sampleData);
                DetectQuoted(option, sampleData);
            }

            // Sort by confidence
            res.Sort(
                delegate(RecordFormatInfo x, RecordFormatInfo y) { return -1*x.Confidence.CompareTo(y.Confidence); });

            return res.ToArray();
        }

        #endregion

        #region "  Fields Properties Methods  "

        private void DetectQuoted(RecordFormatInfo format, string[][] data)
        {
            if (format.ClassBuilder is FixedLengthClassBuilder)
                return;

            // TODO: Add FieldQuoted
        }

        private void DetectTypes(RecordFormatInfo format, string[][] data)
        {

            // TODO: Try to detect posible formats (mostly numbers or dates)
        }

        private void DetectOptionals(RecordFormatInfo option, string[][] data)
        {
            // TODO: Try to detect optional fields

        }

        #endregion

        #region "  Create Options Methods  "

        // UNKNOWN
        private void CreateMixedOptions(string[][] data, List<RecordFormatInfo> res)
        {
            var stats = Indicators.CalculateAsFixedSize (data);

            if (stats.Deviation / stats.Avg <= FixedLengthDeviationTolerance * Math.Min (1, NumberOfLines (data) / MinSampleData))
                CreateFixedLengthOptions(data, res);

            CreateDelimiterOptions(data, res);

            //if (deviation > average * 0.01 &&
            //    deviation < average * 0.05)
            //    CreateFixedLengthOptions(data, res);
        }

        // FIXED LENGTH
        private void CreateFixedLengthOptions(string[][] data, List<RecordFormatInfo> res)
        {
            var format = new RecordFormatInfo();
            var stats = Indicators.CalculateAsFixedSize (data);

            format.mConfidence = (int)(Math.Max (0, 1 - stats.Deviation / stats.Avg) * 100);

            var builder = new FixedLengthClassBuilder("AutoDetectedClass");
            CreateFixedLengthFields(data, builder);

            format.mClassBuilder = builder;

            res.Add(format);
        }

        /// <summary>
        /// start and length of fixed length column
        /// </summary>
        private class FixedColumnInfo
        {
            /// <summary>
            /// start position of column
            /// </summary>
            public int Start;

            /// <summary>
            /// Length of column
            /// </summary>
            public int Length;
        }

        private void CreateFixedLengthFields(string[][] data, FixedLengthClassBuilder builder)
        {
            List<FixedColumnInfo> res = null;

            foreach (var dataFile in data) {
                List<FixedColumnInfo> candidates = CreateFixedLengthCandidates(dataFile);
                res = JoinFixedColCandidates(res, candidates);
            }

            for (int i = 0; i < res.Count; i++) {
                FixedColumnInfo col = res[i];
                builder.AddField("Field" + i.ToString().PadLeft(4, '0'), col.Length, typeof (string));
            }
        }

        private List<FixedColumnInfo> CreateFixedLengthCandidates(string[] lines)
        {
            List<FixedColumnInfo> res = null;

            foreach (var line in lines) {
                var candidates = new List<FixedColumnInfo>();
                int blanks = 0;

                FixedColumnInfo col = null;
                for (int i = 1; i < line.Length; i++) {
                    if (char.IsWhiteSpace(line[i]))
                        blanks += 1;
                    else {
                        if (blanks > 2) {
                            if (col == null) {
                                col = new FixedColumnInfo {
                                    Start = 0,
                                    Length = i
                                };
                            }
                            else {
                                FixedColumnInfo prevCol = col;
                                col = new FixedColumnInfo {
                                    Start = prevCol.Start + prevCol.Length
                                };
                                col.Length = i - col.Start;
                            }
                            candidates.Add(col);
                            blanks = 0;
                        }
                    }
                }

                if (col == null) {
                    col = new FixedColumnInfo {
                        Start = 0,
                        Length = line.Length
                    };
                }
                else {
                    FixedColumnInfo prevCol = col;
                    col = new FixedColumnInfo {
                        Start = prevCol.Start + prevCol.Length
                    };
                    col.Length = line.Length - col.Start;
                }

                candidates.Add(col);


                res = JoinFixedColCandidates(res, candidates);
            }

            return res;
        }

        private List<FixedColumnInfo> JoinFixedColCandidates(List<FixedColumnInfo> cand1, List<FixedColumnInfo> cand2)
        {
            if (cand1 == null)
                return cand2;
            if (cand2 == null)
                return cand1;

            // Merge the result based on confidence
            return cand1;
        }

		bool HeadersInData (DelimiterInfo info, string[] headerValues, string[] rows)
		{
			var duplicate = 0;
			var first = true;
			foreach (var row in rows) {
				if (first) {
					first = false;
					continue;

				}
				var values = row.Split (new char[]{ info.Delimiter });
				if (values.Length != headerValues.Length)
					continue;

				for (int i = 0; i < values.Length; i++) {
					if (values [i] == headerValues [i])
						duplicate++;
				}
			}

			return duplicate >= rows.Length * 0.25;


		}

		bool DetectIfContainsHeaders (DelimiterInfo info, string[][] sampleData)
		{
			if (sampleData.Length >= 2) {
				return SameFirstLine (info, sampleData);
			}
			
			if (sampleData.Length >= 1) {
				var firstLine = sampleData [0] [0].Split (new char[]{ info.Delimiter });
				var res = AreAllHeaders (firstLine);
				if (res == false)
					return false; // if has headers that starts with numbers so near sure are data and no header is present

				if (HeadersInData(info, firstLine, sampleData[0]))
					return false;

				return true;

			}
			return false;
		}

		bool SameFirstLine (DelimiterInfo info, string[][] sampleData)
		{
			for (int i = 1; i < sampleData.Length; i++) {
				if (!SameHeaders (info, sampleData [0][0], sampleData [i][0]))
					return false;
			}
			return true;

		}

		bool SameHeaders (DelimiterInfo info, string line1, string line2)
		{
			return line1.Replace (info.Delimiter.ToString (), "").Trim ()
			== line2.Replace (info.Delimiter.ToString (), "").Trim ();
		}

		bool AreAllHeaders ( string[] rowData)
		{
			foreach (var item in rowData) {
				var fieldData = item.Trim ();
				if (fieldData.Length == 0)
					return false;
				if (char.IsDigit (fieldData [0]))
					return false;
			}
			return true;
		}

        // DELIMITED

        private void CreateDelimiterOptions(string[][] sampleData, List<RecordFormatInfo> res, char delimiter = '\0')
        {
            var delimiters = new List<DelimiterInfo>();

            if (delimiter == '\0')
                delimiters = GetDelimiters(sampleData);
            else
                delimiters.Add(GetDelimiterInfo(sampleData, delimiter));

            foreach (var info in delimiters) {
                var format = new RecordFormatInfo {
                    mConfidence = (int) ((1 - info.Deviation)*100)
                };
                AdjustConfidence(format, info);
				var fileHasHeaders = false;
				if (FileHasHeaders.HasValue)
					fileHasHeaders = FileHasHeaders.Value;
				else {
					fileHasHeaders = DetectIfContainsHeaders (info, sampleData) ;
				}
                var builder = new DelimitedClassBuilder("AutoDetectedClass", info.Delimiter.ToString()) {
					IgnoreFirstLines = fileHasHeaders
                        ? 1
                        : 0
                };

                var firstLineSplitted = sampleData[0][0].Split(info.Delimiter);
                for (int i = 0; i < info.Max + 1; i++) {
                    string name = "Field " + (i + 1).ToString().PadLeft(3, '0');
                    if (fileHasHeaders && i < firstLineSplitted.Length)
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
            switch (info.Delimiter) {
                case '"': // Avoid the quote identifier
                case '\'': // Avoid the quote identifier
                    format.mConfidence = (int) (format.Confidence*0.2);
                    break;

                case '/': // Avoid the date delimiters and url to be selected
                case '.': // Avoid the decimal separator to be selected
                    format.mConfidence = (int) (format.Confidence*0.4);
                    break;

                case '@': // Avoid the mails separator to be selected
                case '&': // Avoid this is near a letter and URLS
                case '=': // Avoid because URLS contains it
                case ':': // Avoid because URLS contains it
                    format.mConfidence = (int) (format.Confidence*0.6);
                    break;

                case '-': // Avoid this other date separator
                    format.mConfidence = (int) (format.Confidence*0.7);
                    break;

                case ',': // Help the , ; tab | to be confident
                case ';':
                case '\t':
                case '|':
                    format.mConfidence = (int) Math.Min(100, format.Confidence*1.15);
                    break;
            }
        }

        #endregion

        #region "  Helper & Utility Methods  "

        private string[][] GetSampleLines(IEnumerable<string> files, int nroOfLines)
        {
            var res = new List<string[]>();

            foreach (var file in files)
                res.Add(RawReadFirstLinesArray(file, nroOfLines, mEncoding));

            return res.ToArray();
        }

        private static string[][] GetSampleLines(IEnumerable<TextReader> files, int nroOfLines)
        {
            var res = new List<string[]>();

            foreach (var file in files)
                res.Add(RawReadFirstLinesArray(file, nroOfLines));

            return res.ToArray();
        }

        private static int NumberOfLines(string[][] data)
        {
            int lines = 0;
            foreach (var fileData in data)
                lines += fileData.Length;
            return lines;
        }

        /// <summary>
        /// Shortcut method to read the first n lines of a text file as array.
        /// </summary>
        /// <param name="file">The file name</param>
        /// <param name="lines">The number of lines to read.</param>
        /// <param name="encoding">The Encoding used to read the file</param>
        /// <returns>The first n lines of the file.</returns>
        private static string[] RawReadFirstLinesArray(string file, int lines, Encoding encoding)
        {
            var res = new List<string>(lines);
            using (var reader = new StreamReader(file, encoding))
            {
                for (int i = 0; i < lines; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    else
                        res.Add(line);
                }
            }

            return res.ToArray();
        }

        /// <summary>
        /// Shortcut method to read the first n lines of a text file as array.
        /// </summary>
        /// <param name="stream">The text reader name</param>
        /// <param name="lines">The number of lines to read.</param>
        /// <returns>The first n lines of the file.</returns>
        private static string[] RawReadFirstLinesArray(TextReader stream, int lines)
        {
            var res = new List<string>(lines);
            for (int i = 0; i < lines; i++)
            {
                string line = stream.ReadLine();
                if (line == null)
                    break;
                else
                    res.Add(line);
            }

            return res.ToArray();
        }

        /// <summary>
        /// Calculate statistics based on sample data for the delimitter supplied
        /// </summary>
        /// <param name="data"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        private DelimiterInfo GetDelimiterInfo(string[][] data, char delimiter)
        {
            var indicators = Indicators.CalculateByDelimiter (delimiter, data, QuotedChar);

            return new DelimiterInfo (delimiter, indicators.Avg, indicators.Max, indicators.Min, indicators.Deviation);
        }

        private List<DelimiterInfo> GetDelimiters(string[][] data)
        {
            var frequency = new Dictionary<char, int>();
            int lines = 0;
            for (int i = 0; i < data.Length; i++) {
                for (int j = 0; j < data[i].Length; j++) {
                    // Ignore Header Line (if any)
                    if (j == 0)
                        continue;
                    // ignore empty lines
                    string line = data[i][j];
                    if (string.IsNullOrEmpty (line))
                        continue;

                    // analyse line
                    lines++;
                                        
                    for (int ci = 0; ci < line.Length; ci++) {
                        char c = line[ci];

                        if (char.IsLetterOrDigit(c)
                            ||
                            c == ' ')
                            continue;

                        int count;
                        if (frequency.TryGetValue(c, out count)) {
                            count++;
                            frequency[c] = count;
                        }
                        else
                            frequency.Add(c, 1);
                    }
                }
            }

            var candidates = new List<DelimiterInfo>();

            // sanity check
            if (lines == 0)
                return candidates;

            // remove delimiters with low occurrence count
            var delimiters = new List<char> (frequency.Count);
            foreach (var pair in frequency)
            {
                if (pair.Value >= lines)
                    delimiters.Add (pair.Key);
            }

            // calculate 
            foreach (var key in delimiters)
            {
                var indicators = Indicators.CalculateByDelimiter (key, data, QuotedChar);
                // Adjust based on the number of lines
                if (lines < MinSampleData)
                indicators.Deviation = indicators.Deviation * Math.Min (1, ((double)lines) / MinSampleData);
                if (indicators.Avg > 1 &&
                    indicators.Deviation < MinDelimitedDeviation)
                    candidates.Add (new DelimiterInfo (key, indicators.Avg, indicators.Max, indicators.Min, indicators.Deviation));
            }

            return candidates;
        }

        #endregion        

        /// <summary>
        /// Gets or sets the quoted char.
        /// </summary>
        /// <value>The quoted char.</value>
        private char QuotedChar { get; set; }

        #region "  Statistics Functions  "
        /// <summary>
        /// Collection of statistics about fields found
        /// </summary>
        private class Indicators
        {
            /// <summary>
            /// Maximum number of fields found
            /// </summary>
            public int Max = int.MinValue;

            /// <summary>
            /// Mimumim number of fields found
            /// </summary>
            public int Min = int.MaxValue;

            /// <summary>
            /// Average number of delimiters foudn per line
            /// </summary>
            public double Avg = 0;

            /// <summary>
            /// Calculated deviation
            /// </summary>
            public double Deviation = 0;

            /// <summary>
            /// Total analysed lines
            /// </summary>
            public int Lines = 0;

            private static double CalculateDeviation (IList<int> values, double avg)
            {
                double sum = 0;
                for (int i = 0; i < values.Count; i++)
                {
                    sum += Math.Pow (values[i] - avg, 2);
                }
                return Math.Sqrt (sum / values.Count);
            }

            private static int CountNumberOfDelimiters (string line, char delimiter)
            {
                int count = 0;
                char c;
                for (int i = 0; i < line.Length; i++)
                {
                    c = line[i];
                    if (c == ' ' || char.IsLetterOrDigit (c))
                        continue;
                    count++;
                }
                return count;
            }

            public static Indicators CalculateByDelimiter (char delimiter, string[][] data, char? quotedChar)
            {
                var res = new Indicators ();
                int totalDelimiters = 0;
                int lines = 0;
                List<int> delimiterPerLine = new List<int> (100);

                foreach (var fileData in data)
                {
                    foreach (var line in fileData)
                    {
                        if (string.IsNullOrEmpty (line))
                            continue;

                        lines++;

                        var delimiterInLine = 0;
                        if (quotedChar.HasValue)
                            delimiterInLine = QuoteHelper.CountNumberOfDelimiters (line, delimiter, quotedChar.Value);
                        else
                            delimiterInLine = CountNumberOfDelimiters (line, delimiter);
                        // add count for deviation analysis
                        delimiterPerLine.Add (delimiterInLine);

                        if (delimiterInLine > res.Max)
                            res.Max = delimiterInLine;

                        if (delimiterInLine < res.Min)
                            res.Min = delimiterInLine;

                        totalDelimiters += delimiterInLine;
                    }
                }

                res.Avg = totalDelimiters / (double)lines;

                // calculate deviation
                res.Deviation = CalculateDeviation (delimiterPerLine, res.Avg);

                return res;
            }

            public static Indicators CalculateAsFixedSize (string[][] data)
            {
                var res = new Indicators ();
                double sum = 0;
                int lines = 0;
                List<int> sizePerLine = new List<int> (100);

                foreach (var fileData in data)
                {
                    foreach (var line in fileData)
                    {
                        if (string.IsNullOrEmpty (line))
                            continue;
                        lines++;
                        sum += line.Length;
                        sizePerLine.Add (line.Length);

                        if (line.Length > res.Max)
                            res.Max = line.Length;

                        if (line.Length < res.Min)
                            res.Min = line.Length;
                    }
                }

                res.Avg = sum / (double)lines;
                // calculate deviation
                res.Deviation = CalculateDeviation (sizePerLine, res.Avg);

                return res;
            }
        }

        #endregion
    }
}