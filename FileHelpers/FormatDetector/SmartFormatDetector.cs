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

            return res.ToArray();
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
            
        }

        // FIXED LENGTH
        private void CreateFixedLengthOptions(string[][] sampleData, List<FormatOption> res)
        {
            
        }

        // DELIMITED
        private void CreateDelimiterOptions(string[][] sampleData, List<FormatOption> res)
        {
            CreateDelimiterOptions(sampleData, res, '\0');
        }

        private void CreateDelimiterOptions(string[][] sampleData, List<FormatOption> res, char delimiter)
        {
            List<char> delimiters = new List<char>();
            if (delimiter == '\0')
                delimiters = GetDelimiters(sampleData);
            else 
                delimiters.Add(delimiter);

            FormatOption option = new FormatOption();
            DelimitedClassBuilder builder = new DelimitedClassBuilder("AutoDetectedClass", delimiter.ToString());


        }

        private List<char> GetDelimiters(string[][] sampleData)
        {
            SortedDictionary<int, List<char>> frequency = FrequencyTable(sampleData);
            
            foreach (KeyValuePair<int, List<char>> pair in frequency)
            {
                
            }

            return new List<char>();
        }

        private SortedDictionary<int, List<char>> FrequencyTable(string[][] data)
        {
            SortedDictionary<int, List<char>> res = new SortedDictionary<int, List<char>>();

            SortedDictionary<char, int> frecuency = new SortedDictionary<char, int>();

            for (int i = 0; i < data.Length; i++)
            {
                
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (j == 0) continue; // Ignore Header Line (if any)

                    string line = data[i][j];
                    for (int ci = 0; ci < line.Length; ci++)
                    {
                        char c = line[ci];
                        if (Char.IsLetterOrDigit(c) || c == ' ')
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

            foreach (KeyValuePair<char, int> pair in frecuency)
            {
                if (res.ContainsKey(pair.Value))
                    res[pair.Value].Add(pair.Key);
                else
                {
                    List<char> valueList = new List<char>();
                    valueList.Add(pair.Key);
                    res.Add(pair.Value, valueList);
                }
            }

            return res;
        }
    }
}
