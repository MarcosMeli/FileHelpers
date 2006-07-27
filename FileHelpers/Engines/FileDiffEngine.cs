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


		public object[] OnlyDuplicatedRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
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

		public object[] OnlyNoDuplicatedRecords(string sourceFile, string newFile)
		{
			FileHelperEngine engine = CreateEngineAndClearErrors();
			
			IComparableRecord[] olds = (IComparableRecord[]) engine.ReadFile(sourceFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
			IComparableRecord[] currents = (IComparableRecord[]) engine.ReadFile(newFile);
			this.ErrorManager.AddErrors(engine.ErrorManager);
            
			ArrayList news = new ArrayList();

			ApplyDiffOnlyIn1(currents, olds, news);

			ApplyDiffOnlyIn1(olds, currents, news);

			return (object[]) news.ToArray(mRecordInfo.mRecordType);
    
		} 

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
