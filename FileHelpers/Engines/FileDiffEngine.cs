using System;
using System.Collections;

namespace FileHelpers
{

	#region " IComparableRecord "

	/// <summary>Used by the FileDiffEngine to compare records. Your record class must implement this interface if you like to work with it.</summary>
	public interface IComparableRecord
	{
		/// <summary>
		/// Compare two records and return true if are equal.
		/// </summary>
		/// <param name="record">The other record.</param>
		/// <returns>Returns true only if the records are equals.</returns>
		bool IsEqualRecord(object record);
	}

	#endregion

	/// <summary>
	/// Engine used to create diff files based on the <see cref="IComparableRecord"/> interface.
	/// </summary>
	public sealed class FileDiffEngine: EngineBase
	{
		/// <summary>
		/// Creates a new <see cref="FileDiffEngine"/>
		/// </summary>
		/// <param name="recordType">The record type class that implements the <see cref="IComparableRecord"/> interface.</param>
		public FileDiffEngine(Type recordType):base(recordType)
		{
			if (typeof(IComparableRecord).IsAssignableFrom(recordType) == false)
				throw new BadUsageException("The record class '" + "' need to implement the interface IComparableRecord.");
		}


		/// <summary>Returns the records in newFile that not are in the sourceFile</summary>
		/// <param name="sourceFile">The file with the old records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <returns>The records in newFile that not are in the sourceFile</returns>
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

		
		/// <summary>Returns the records in newFile that not are in the sourceFile</summary>
		/// <param name="sourceFile">The file with the old records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <returns>The records in newFile that not are in the sourceFile</returns>
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

		private FileHelperEngine CreateEngineAndClearErrors()
		{
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo);
			engine.Encoding = this.Encoding;

			this.ErrorManager.ClearErrors();
			engine.ErrorManager.ErrorMode = this.ErrorManager.ErrorMode;
			
			return engine;
		}


		/// <summary>
		/// Returns the duplicated records in both files.
		/// </summary>
		/// <param name="file1">A file with record.</param>
		/// <param name="file2">A file with record.</param>
		/// <returns>The records that appear in both files.</returns>
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

		#region "  ApplyDiff  "
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

		/// <summary>Read the source file, the new file, get the new records and write them to a destination file.</summary>
		/// <param name="sourceFile">The file with the source records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The new records on the new file.</returns>
		public object[] WriteNewRecords(string sourceFile, string newFile, string destFile)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			object[] res = OnlyNewRecords(sourceFile, newFile);
			engine.WriteFile(destFile, res);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			return res;
		}

	}
}
