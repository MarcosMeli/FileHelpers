#undef GENERICS
//#define GENERICS
//#if NET_2_0


using System;
using System.Collections;
using System.Diagnostics;

#if GENERICS
using System.Collections.Generic;
#endif


namespace FileHelpers
{


	/// <summary>Engine used to create diff files based on the <see cref="IComparableRecord"/> interface.</summary>
#if NET_2_0
    [DebuggerDisplay("FileDiffEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#endif
#if ! GENERICS
	public sealed class FileDiffEngine: EngineBase
#else
    /// <typeparam name="T">The record type.</typeparam>
    public sealed class FileDiffEngine<T>: EngineBase
		where T: IComparableRecord
#endif
    {
		/// <summary>
		/// Creates a new <see cref="FileDiffEngine"/>
		/// </summary>
		/// <param name="recordType">The record type class that implements the <see cref="IComparableRecord"/> interface.</param>
#if ! GENERICS
		public FileDiffEngine(Type recordType):base(recordType)
		{
			if (typeof(IComparableRecord).IsAssignableFrom(recordType) == false)
				throw new BadUsageException("The record class '" + "' need to implement the interface IComparableRecord.");
		}
#else
		public FileDiffEngine():base(typeof(T))
		{
		}
#endif


		/// <summary>Returns the records in newFile that not are in the sourceFile</summary>
		/// <param name="sourceFile">The file with the old records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <returns>The records in newFile that not are in the sourceFile</returns>
#if ! GENERICS
		public object[] OnlyNewRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			ArrayList news = new ArrayList();
			ApplyDiffOnlyIn1(currents, olds, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
		} 
#else
		public T[] OnlyNewRecords(string sourceFile, string newFile)
		{
			FileHelperEngine<T> engine = CreateEngineAndClearErrors();
			
			T[] olds = engine.ReadFile(sourceFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			T[] currents = engine.ReadFile(newFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			List<T> news = new List<T>();
			ApplyDiffOnlyIn1(currents, olds, news);

			return news.ToArray();
		} 
#endif

		
		/// <summary>Returns the records in newFile that not are in the sourceFile</summary>
		/// <param name="sourceFile">The file with the old records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <returns>The records in newFile that not are in the sourceFile</returns>
#if ! GENERICS
		public object[] OnlyMissingRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();

			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);

			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			ArrayList news = new ArrayList();

			ApplyDiffOnlyIn1(olds, currents, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
		}
#else
		public T[] OnlyMissingRecords(string sourceFile, string newFile)
		{
			FileHelperEngine<T> engine = CreateEngineAndClearErrors();

			T[] olds = engine.ReadFile(sourceFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);

			T[] currents = engine.ReadFile(newFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			List<T> news = new List<T>();

			ApplyDiffOnlyIn1(olds, currents, news);

			return news.ToArray();
		}
#endif
		
#if ! GENERICS
		private FileHelperEngine CreateEngineAndClearErrors()
		{
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo);
#else
		private FileHelperEngine<T> CreateEngineAndClearErrors()
		{
			FileHelperEngine<T> engine = new FileHelperEngine<T>();
#endif
			engine.Encoding = this.Encoding;

			ErrorManager.ClearErrors();
			engine.ErrorManager.ErrorMode = this.ErrorManager.ErrorMode;
			
			return engine;
		}


		/// <summary>
		/// Returns the duplicated records in both files.
		/// </summary>
		/// <param name="file1">A file with record.</param>
		/// <param name="file2">A file with record.</param>
		/// <returns>The records that appear in both files.</returns>
#if ! GENERICS
		public object[] OnlyDuplicatedRecords(string file1, string file2)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(file1);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(file2);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			ArrayList news = new ArrayList();

			ApplyDiffInBoth(currents, olds, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
		}
#else
		public T[] OnlyDuplicatedRecords(string file1, string file2)
		{
			FileHelperEngine<T> engine = CreateEngineAndClearErrors();
			
			T[] olds = engine.ReadFile(file1);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			T[] currents = engine.ReadFile(file2);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			List<T> news = new List<T>();

			ApplyDiffInBoth(currents, olds, news);

			return news.ToArray();
		}
#endif

		#region "  ApplyDiff  "

#if ! GENERICS		
		private static void ApplyDiffInBoth(IComparableRecord[] col1, IComparableRecord[] col2, ArrayList arr)
		{
			ApplyDiff(col1, col2, arr, true);
		}

		private static void ApplyDiffOnlyIn1(IComparableRecord[] col1, IComparableRecord[] col2, ArrayList arr)
		{
			ApplyDiff(col1, col2, arr, false);
		}

		private static void ApplyDiff(IComparableRecord[] col1, IComparableRecord[] col2, ArrayList arr, bool addIfIn1)
		{
			for(int i = 0; i < col1.Length; i++)
			{
				bool isNew = false; 
				
				//OPT: aca podemos hacer algo asi que para cada nuevo 
				//     que encuentra no empieze en j = i sino en j = i - nuevos =)
				//     Otra idea de guille es agrear un window y usar el Max de las dos

				IComparableRecord current = col1[i];
#else
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
			for(int i = 0; i < col1.Length; i++)
			{
				bool isNew = false; 
				
				//OPT: aca podemos hacer algo asi que para cada nuevo 
				//     que encuentra no empieze en j = i sino en j = i - nuevos =)
				//     Otra idea de guille es agrear un window y usar el Max de las dos

				T current = col1[i];
#endif
		
				for(int j = i; j < col2.Length; j++)
				{
					if (current.IsEqualRecord(col2[j]))
					{
						isNew = true;
						break;
					}
				}

				
				if (isNew == false)
				{
					for(int j = 0; j < Math.Min(i, col2.Length); j++)
					{
						if (current.IsEqualRecord(col2[j]))
						{
							isNew = true;
							break;
						}
					}
				}

				if (isNew == addIfIn1) arr.Add(current); 

			}
		}

		#endregion

		/// <summary>
		/// Returns the NON duplicated records in both files.
		/// </summary>
		/// <param name="file1">A file with record.</param>
		/// <param name="file2">A file with record.</param>
		/// <returns>The records that NOT appear in both files.</returns>
#if ! GENERICS
		public object[] OnlyNoDuplicatedRecords(string file1, string file2)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(file1);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(file2);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			ArrayList news = new ArrayList();

			ApplyDiffOnlyIn1(currents, olds, news);

			ApplyDiffOnlyIn1(olds, currents, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
    
		} 
#else
		public T[] OnlyNoDuplicatedRecords(string file1, string file2)
		{
			FileHelperEngine<T> engine = CreateEngineAndClearErrors();
			
			T[] olds = engine.ReadFile(file1);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			T[] currents = engine.ReadFile(file2);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			List<T> news = new List<T>();

			ApplyDiffOnlyIn1(currents, olds, news);

			ApplyDiffOnlyIn1(olds, currents, news);

			return news.ToArray();
    
		} 
#endif

		/// <summary>Read the source file, the new file, get the new records and write them to a destination file.</summary>
		/// <param name="sourceFile">The file with the source records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The new records on the new file.</returns>
#if ! GENERICS
		public object[] WriteNewRecords(string sourceFile, string newFile, string destFile)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			object[] res = OnlyNewRecords(sourceFile, newFile);
#else
		public T[] WriteNewRecords(string sourceFile, string newFile, string destFile)
		{
			FileHelperEngine<T> engine = CreateEngineAndClearErrors();
			T[] res = OnlyNewRecords(sourceFile, newFile);
#endif
			engine.WriteFile(destFile, res);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			return res;
		}

	}
}
//#endif