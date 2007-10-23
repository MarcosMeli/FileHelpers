using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers.RunTime;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Helper class to 
    /// </summary>
    public class SmartFormatDetector
    {
        private FormatHint mFormatHint;

        public FormatHint FormatHint
        {
            get { return mFormatHint; }
            set { mFormatHint = value; }
        }

        private int mSampleLines = 20;

        public int SampleLines
        {
            get { return mSampleLines; }
            set { mSampleLines = value; }
        }

        private Encoding mEncoding = Encoding.Default;

        public Encoding Encoding 
        {
            get { return mEncoding; }
            set { mEncoding = value; }
        }

        public FormatOption[] DetectFileFormat(string file)
        {
            return DetectFileFormat(new string[] {file});
        }

        public FormatOption[] DetectFileFormat(IEnumerable<string> files)
        {
            List<FormatOption> res = new List<FormatOption>();
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

            foreach (FormatOption option in res)
            {
                DetectTypes(option);
                DetectQuoted(option);
            }

            return res.ToArray();
        }

        private void DetectQuoted(FormatOption option)
        {
        }

        private void DetectTypes(FormatOption option)
        {
        }

        private string[][] GetSampleLines(IEnumerable<string> files, int nroOfLines)
        {
            List<string[]> res = new List<string[]>();

            foreach (string file in files)
            {
                res.Add(CommonEngine.RawReadFirstLinesArray(file, nroOfLines, mEncoding));    
            }

            return res.ToArray();
        }

        // UNKNOWN
        private void CreateMixedOptions(string[][] sampleData, List<FormatOption> res)
        {
            if (LooksLikeFixed(sampleData))
                CreateFixedLengthOptions(sampleData, res);
            else 
                CreateDelimiterOptions(sampleData, res);
            
        }

        private bool LooksLikeFixed(string[][] data)
        {
            double average = CreateAverageLines(data);
            double deviation = CalculateDeviationLines(data, average);

            return deviation < average*0.02;
        }

        // FIXED LENGTH
        private void CreateFixedLengthOptions(string[][] sampleData, List<FormatOption> res)
        {
            FormatOption option = new FormatOption();
            option.Certainty = 50;

            FixedLengthClassBuilder builder = new FixedLengthClassBuilder("AutoDetectedClass");
            CreateFixedLengthFields(sampleData, builder);

            option.mClassBuilder = builder;

            res.Add(option);
        }

        private void CreateFixedLengthFields(string[][] data, FixedLengthClassBuilder builder)
        {
            
        }

        // DELIMITED
        private void CreateDelimiterOptions(string[][] sampleData, List<FormatOption> res)
        {
            CreateDelimiterOptions(sampleData, res, '\0');
        }

        private void CreateDelimiterOptions(string[][] sampleData, List<FormatOption> res, char delimiter)
        {
            List<DelimiterInfo> delimiters = new List<DelimiterInfo>();
           
            if (delimiter == '\0')
                delimiters = GetDelimiters(sampleData);
            else 
                delimiters.Add(GetDelimiterInfo(sampleData, delimiter));

            foreach (DelimiterInfo info in delimiters)
            {
                FormatOption option = new FormatOption();
                option.Certainty = (int) ((1 - info.Deviation) * 100);
                
                DelimitedClassBuilder builder = new DelimitedClassBuilder("AutoDetectedClass", info.Delimiter.ToString());
                builder.AddFields(info.AvergeByLine);

                option.mClassBuilder = builder;
                
                res.Add(option);
            }

        }

        private DelimiterInfo GetDelimiterInfo(string[][] data, char delimiter)
        {
            double average = CreateAverage(delimiter, data);
            double deviation = CalculateDeviation(delimiter, data, average);

            return new DelimiterInfo(delimiter, (int) Math.Round(average), deviation);

        }

        private List<DelimiterInfo> GetDelimiters(string[][] data)
        {
            Dictionary<char, int> frecuency = new Dictionary<char, int>();

            for (int i = 0; i < data.Length; i++)
            {
                
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (j == 0) continue; // Ignore Header Line (if any)

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

            int max = 0;
            foreach (KeyValuePair<char, int> pair in frecuency)
            {
                max = Math.Max(max, pair.Value);
            }

            List<DelimiterInfo> candidates = new List<DelimiterInfo>();


            foreach (KeyValuePair<char, int> pair in frecuency)
            {
                double average = CreateAverage(pair.Key, data);
                double deviation = CalculateDeviation(pair.Key, data, average);

                if (average > 1 && deviation < 0.2)
                    candidates.Add(new DelimiterInfo(pair.Key, (int) Math.Round(average), deviation));
            }

            return candidates;
        }

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

        private double CreateAverage(char c, string[][] data)
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

        private double CreateAverageLines(string[][] data)
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

        private double CalculateDeviationLines(string[][] data, double average)
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

    }

    internal class DelimiterInfo
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
