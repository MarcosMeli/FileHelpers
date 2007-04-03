using System;
using FileHelpers;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FileHelpersSamples
{
    [DelimitedRecord("|")]
    public class VersionData
    {
        public string Version;

        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime ReleaseDate;

        public string DownloadUrl;
        public string DownloadOthers;

        [FieldInNewLine]
        [FieldQuoted(MultilineMode.AllowForBoth)]
        public string Description;

        [FieldQuoted(MultilineMode.AllowForBoth)]
        public string History;

        public static int CompararVersiones(string ver1, string ver2)
        {
            string[] ver1A = ver1.Split('.');
            string[] ver2A = ver2.Split('.');
            if (ver1A.Length > 0 & ver2A.Length > 0)
            {
                if (int.Parse(ver1A[0]) != int.Parse(ver2A[0]))
                {
                    return int.Parse(ver1A[0]) - int.Parse(ver2A[0]);
                }
            }
            if (ver1A.Length > 1 & ver2A.Length > 1)
            {
                if (int.Parse(ver1A[1]) != int.Parse(ver2A[1]))
                {
                    return int.Parse(ver1A[1]) - int.Parse(ver2A[1]);
                }
            }
            if (ver1A.Length > 2 & ver2A.Length > 2)
            {
                if (int.Parse(ver1A[2]) != int.Parse(ver2A[2]))
                {
                    return int.Parse(ver1A[2]) - int.Parse(ver2A[2]);
                }
            }
            return 0;
        }

        public static VersionData GetLastVersion()
        {

            string dataString;
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData("http://filehelpers.sourceforge.net/version.txt");
                dataString = System.Text.Encoding.Default.GetString(data);
            }

            VersionData[] versions = null;
            FileHelperEngine engine = new FileHelperEngine(typeof(VersionData));
            versions = (VersionData[])engine.ReadString(dataString);

            return versions[versions.Length - 1];
        }

    }

}
