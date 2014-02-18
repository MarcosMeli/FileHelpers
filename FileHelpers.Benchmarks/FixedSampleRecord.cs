using System;

namespace FileHelpers.Benchmarks
{
    /// <summary>
    /// Sample fixed length record for testing
    /// </summary>
    [FixedLengthRecord()]
    public class FixedSampleRecord
    {
        [FieldFixedLength(11)]
        public long Cuit;

        [FieldFixedLength(160)]
        [FieldTrim(TrimMode.Both)]
        public string Nombre;

        [FieldFixedLength(6)]
        public int Actividad;


        public void AfterRead(EngineBase engine, string line)
        {
            Nombre = Nombre.Replace("#", "Ñ");
        }
    }
}