using System;
using System.Collections;

namespace FileHelpers
{

	#region " IComparableRecord "

	public interface IComparableRecord
	{
		bool IsEqualRecord(object record);
	}

	#endregion

	public sealed class FileDiffEngine: EngineBase
	{
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
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo);
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
            
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
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo);
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
            
			ArrayList news = new ArrayList();

			ApplyDiffOnlyIn1(olds, currents, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
		} 

		
		public object[] OnlyDuplicatedRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo);
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
            
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
			foreach (IComparableRecord current in col1)
			{
				bool isNew = false; 
				
				foreach (IComparableRecord old in col2)
				{
					if (current.IsEqualRecord(old))
					{
						isNew = true;
						break;
					}
				}

				if (isNew == addIfIn1) arr.Add(current); 
			}
		}

		#endregion

		public object[] OnlyNoDuplicatedRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo);
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
            
			ArrayList news = new ArrayList();

			ApplyDiffOnlyIn1(currents, olds, news);

			ApplyDiffOnlyIn1(olds, currents, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
    
		} 


	}
}
