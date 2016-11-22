using System;
using System.Linq;

namespace FileHelpers
{
    public interface ILineInfo
    {
        string CurrentString { get; }

        int Number { get; }

        int CurrentPos { get; }

        string LineString { get; }

        void ReLoadAsNextLine();

        void ReLoad(string line);
    }
}
