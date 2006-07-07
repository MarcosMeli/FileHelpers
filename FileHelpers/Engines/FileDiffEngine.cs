using System;
using System.Collections;

namespace FileHelpers
{

	public interface IComparableRecord
	{
		bool IsEqualRecord(object record);
	}

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
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo.mRecordType);
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
            
			ArrayList news = new ArrayList();
			foreach (IComparableRecord current in currents)
			{
				bool isNew = true; 
				
				foreach (IComparableRecord old in olds)
				{
					if (current.IsEqualRecord(old))
					{
						isNew = false;
						break;
					}
				}

				if (isNew) 
					news.Add(current); 
			}

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
    
		} 

		
		/// <summary>Returns the records in newFile that not are in the sourceFile</summary>
		/// <param name="sourceFile">The file with the old records.</param>
		/// <param name="newFile">The file with the new records.</param>
		/// <returns>The records in newFile that not are in the sourceFile</returns>
		public object[] OnlyMissingRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = new FileHelperEngine(mRecordInfo.mRecordType);
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
            
			ArrayList news = new ArrayList();

			foreach (IComparableRecord old in olds)
			{
				bool isNew = true; 
				
				foreach (IComparableRecord current in currents)
				{
					if (old.IsEqualRecord(current))
					{
						isNew = false;
						break;
					}
				}

				if (isNew) 
					news.Add(old); 
			}

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
		} 

		
	}
}
