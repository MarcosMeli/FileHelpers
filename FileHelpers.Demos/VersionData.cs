using System;
using FileHelpers;
using System.Net;

namespace FileHelpersSamples
{
    /// <summary>
    /// Provides Version and History information to the
    /// GUI system directly from the FileHelpers website
    /// </summary>
    [DelimitedRecord("|")]
    public class VersionData
    {
        /// <summary>
        /// Current version number of FileHelpers available
        /// </summary>
        public string Version;

        /// <summary>
        /// When the version was released
        /// </summary>
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime ReleaseDate;

        /// <summary>
        /// Place to go get a fresh copy of the executable
        /// </summary>
        public string DownloadUrl;

        /// <summary>
        /// Place to go get all other possible downloads??
        /// </summary>
        public string DownloadOthers;

        /// <summary>
        /// Description of this release
        /// </summary>
        [FieldInNewLine]
        [FieldQuoted(MultilineMode.AllowForBoth)]
        public string Description;

        /// <summary>
        /// History dump describing changes
        /// </summary>
        [FieldQuoted(MultilineMode.AllowForBoth)]
        public string History;

        /// <summary>
        /// Compare two version numbers 
        /// </summary>
        /// <param name="ver1">Your version number</param>
        /// <param name="ver2">Other release version number</param>
        /// <returns>zero if equal,  > 0 if yours is newer</returns>
        public static int CompararVersiones(string ver1, string ver2)
        {
            string[] ver1A = ver1.Split('.');
            string[] ver2A = ver2.Split('.');
            if (ver1A.Length > 0 & ver2A.Length > 0) {
                if (int.Parse(ver1A[0]) != int.Parse(ver2A[0]))
                    return int.Parse(ver1A[0]) - int.Parse(ver2A[0]);
            }
            if (ver1A.Length > 1 & ver2A.Length > 1) {
                if (int.Parse(ver1A[1]) != int.Parse(ver2A[1]))
                    return int.Parse(ver1A[1]) - int.Parse(ver2A[1]);
            }
            if (ver1A.Length > 2 & ver2A.Length > 2) {
                if (int.Parse(ver1A[2]) != int.Parse(ver2A[2]))
                    return int.Parse(ver1A[2]) - int.Parse(ver2A[2]);
            }
            return 0;
        }

        /// <summary>
        /// Retrieve the current version information off the web
        /// Break it down for easy use
        /// </summary>
        /// <remarks>
        /// TODO: If the internet version cannot be found, I think this fails.
        /// Since it is background it is probably not trapped.
        /// </remarks>
        /// <returns>Current Version data from the Internet</returns>
        public static VersionData GetLastVersion()
        {
            string dataString;
            using (WebClient webClient = new WebClient()) {
                byte[] data = webClient.DownloadData("http://filehelpers.sourceforge.net/version.txt");
                dataString = System.Text.Encoding.Default.GetString(data);
            }

            VersionData[] versions = null;
            FileHelperEngine engine = new FileHelperEngine(typeof (VersionData));
            versions = (VersionData[]) engine.ReadString(dataString);

            return versions[versions.Length - 1];
        }
    }
}