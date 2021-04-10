using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileHelpers
{
    /// <summary>
    /// Engine used to create diff files based on the
    /// <see cref="IComparable{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The record type.</typeparam>
    [DebuggerDisplay(
        "FileDiffEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}"
        )]
    public sealed class FileDiffEngine<T> : EngineBase
        where T : class, IComparable<T>
    {
        /// <summary>
        /// Creates a new <see cref="FileDiffEngine{T}"/>
        /// </summary>
        public FileDiffEngine()
            : base(typeof (T)) {}

        /// <summary>Returns the records in newFile that not are in the sourceFile</summary>
        /// <param name="sourceFile">The file with the old records.</param>
        /// <param name="newFile">The file with the new records.</param>
        /// <returns>The records in newFile that not are in the sourceFile</returns>
        public T[] OnlyNewRecords(string sourceFile, string newFile)
        {
            FileHelperEngine<T> engine = CreateEngineAndClearErrors();

            var olds = engine.ReadFile(sourceFile);
            ErrorManager.AddErrors(engine.ErrorManager);
            var currents = engine.ReadFile(newFile);
            ErrorManager.AddErrors(engine.ErrorManager);

            var news = new List<T>();
            ApplyDiffOnlyIn1(currents, olds, news);

            return news.ToArray();
        }

        /// <summary>Returns the records in newFile that not are in the sourceFile</summary>
        /// <param name="sourceFile">The file with the old records.</param>
        /// <param name="newFile">The file with the new records.</param>
        /// <returns>The records in newFile that not are in the sourceFile</returns>
        public T[] OnlyMissingRecords(string sourceFile, string newFile)
        {
            FileHelperEngine<T> engine = CreateEngineAndClearErrors();

            T[] olds = engine.ReadFile(sourceFile);
            ErrorManager.AddErrors(engine.ErrorManager);

            T[] currents = engine.ReadFile(newFile);
            ErrorManager.AddErrors(engine.ErrorManager);

            var news = new List<T>();

            ApplyDiffOnlyIn1(olds, currents, news);

            return news.ToArray();
        }

        private FileHelperEngine<T> CreateEngineAndClearErrors()
        {
            var engine = new FileHelperEngine<T> {
                Encoding = Encoding
            };

            ErrorManager.ClearErrors();
            engine.ErrorManager.ErrorMode = ErrorManager.ErrorMode;

            return engine;
        }

        /// <summary>
        /// Returns the duplicated records in both files.
        /// </summary>
        /// <param name="file1">A file with records.</param>
        /// <param name="file2">A file with records.</param>
        /// <returns>The records that appear in both files.</returns>
        public T[] OnlyDuplicatedRecords(string file1, string file2)
        {
            FileHelperEngine<T> engine = CreateEngineAndClearErrors();

            T[] olds = engine.ReadFile(file1);
            ErrorManager.AddErrors(engine.ErrorManager);
            T[] currents = engine.ReadFile(file2);
            ErrorManager.AddErrors(engine.ErrorManager);

            var news = new List<T>();

            ApplyDiffInBoth(currents, olds, news);

            return news.ToArray();
        }

        #region "  ApplyDiff  "

        private static void ApplyDiffInBoth(T[] col1, T[] col2, List<T> arr)
        {
            ApplyDiff(col1, col2, arr, true);
        }

        private static void ApplyDiffOnlyIn1(T[] col1, T[] col2, List<T> arr)
        {
            ApplyDiff(col1, col2, arr, false);
        }

        private static void ApplyDiff(T[] col1, T[] col2, List<T> arr, bool addIfIn1)
        {
            for (int i = 0; i < col1.Length; i++) {
                bool isNew = false;

                //OPT: aca podemos hacer algo asi que para cada nuevo 
                //     que encuentra no empieze en j = i sino en j = i - nuevos =)
                //     Otra idea de guille es agrear un window y usar el Max de las dos

                T current = col1[i];

                for (int j = i; j < col2.Length; j++) {
                    if (current.CompareTo(col2[j]) == 0) {
                        isNew = true;
                        break;
                    }
                }

                if (isNew == false) {
                    for (int j = 0; j < Math.Min(i, col2.Length); j++) {
                        if (current.CompareTo(col2[j]) == 0) {
                            isNew = true;
                            break;
                        }
                    }
                }

                if (isNew == addIfIn1)
                    arr.Add(current);
            }
        }

        #endregion

        /// <summary>
        /// Returns the NON duplicated records in both files.
        /// </summary>
        /// <param name="file1">A file with record.</param>
        /// <param name="file2">A file with record.</param>
        /// <returns>The records that NOT appear in both files.</returns>
        public T[] OnlyNoDuplicatedRecords(string file1, string file2)
        {
            FileHelperEngine<T> engine = CreateEngineAndClearErrors();

            T[] olds = engine.ReadFile(file1);
            ErrorManager.AddErrors(engine.ErrorManager);
            T[] currents = engine.ReadFile(file2);
            ErrorManager.AddErrors(engine.ErrorManager);

            var news = new List<T>();

            ApplyDiffOnlyIn1(currents, olds, news);

            ApplyDiffOnlyIn1(olds, currents, news);

            return news.ToArray();
        }

        /// <summary>
        /// Read the source file, the new File, get the records and write
        /// them to a destination file.  (record not in first file but in
        /// second will be written to the third)
        /// </summary>
        /// <param name="sourceFile">The file with the source records.</param>
        /// <param name="newFile">The file with the new records.</param>
        /// <param name="destFile">The destination file.</param>
        /// <returns>The new records on the new file.</returns>
        public T[] WriteNewRecords(string sourceFile, string newFile, string destFile)
        {
            FileHelperEngine<T> engine = CreateEngineAndClearErrors();
            T[] res = OnlyNewRecords(sourceFile, newFile);

            engine.WriteFile(destFile, res);
            ErrorManager.AddErrors(engine.ErrorManager);
            return res;
        }
    }
}