#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Reflection;

namespace FileHelpers.DataLink
{
	/// <summary>
	/// This class has the responsability to enable the two directional
	/// transformation.
	/// <list type="bullet">
	/// <item> DataStorage &lt;-> DataStorage </item>
	/// </list>
	/// </summary>
	/// <remarks>
	/// <para>Uses two <see cref="DataStorage"/> to accomplish this task.</para>
	/// </remarks>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="example_datalink.html">Example of the DataLink</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
	public sealed class GenericDataLink
	{
		#region "  Constructor  "

		/// <summary>Create a new instance of the class.</summary>
		/// <param name="provider1">The First <see cref="DataStorage"/> used to insert/extract records .</param>
		/// <param name="provider2">The Second <see cref="DataStorage"/> used to insert/extract records .</param>
		public GenericDataLink(DataStorage provider1, DataStorage provider2)
		{
			if (provider1 == null)
				throw new ArgumentException("provider1 can´t be null", "provider1");
			else
				mDataStorage1 = provider1;

			if (provider2 == null)
				throw new ArgumentException("provider2 can´t be null", "provider2");
			else
				mDataStorage2 = provider2;

			ValidateRecordTypes();
		}

		#endregion

		private DataStorage mDataStorage1;
		private DataStorage mDataStorage2;

		/// <summary>Extract the records from DataStorage1 and Insert them to the DataStorage2.</summary>
		/// <returns>The Copied records.</returns>
		public object[] CopyDataFrom1To2()
		{
			object[] res = DataStorage1.ExtractRecords();
			DataStorage2.InsertRecords(res);
			return res;
		}

		/// <summary>Extract the records from DataStorage2 and Insert them to the DataStorage1.</summary>
		/// <returns>The Copied records.</returns>
		public object[] CopyDataFrom2To1()
		{
			object[] res = DataStorage2.ExtractRecords();
			DataStorage1.InsertRecords(res);
			return res;
		}

		MethodInfo mConvert1to2 = null;
		MethodInfo mConvert2to1 = null;


		/// <summary>The fisrt <see cref="DataStorage"/> of the <see cref="GenericDataLink"/>.</summary>
		public DataStorage DataStorage1
		{
			get { return mDataStorage1; }
		}

		/// <summary>The second <see cref="DataStorage"/> of the <see cref="GenericDataLink"/>.</summary>
		public DataStorage DataStorage2
		{
			get { return mDataStorage2; }
		}

		private void ValidateRecordTypes()
		{
			if (DataStorage1.RecordType == null)
				throw new BadUsageException("DataLink1 can´t have a null RecordType.");

			if (DataStorage2.RecordType == null)
				throw new BadUsageException("DataLink2 can´t have a null RecordType.");

			if (DataStorage1.RecordType != DataStorage2.RecordType)
			{
				mConvert1to2 = GetTransformMethod(DataStorage1.RecordType, DataStorage2.RecordType);
				if (mConvert1to2 == null)
					throw new BadUsageException("You must to define a method in the class " + DataStorage1.RecordType.Name + " with the attribute [TransfortToRecord(typeof(" + DataStorage2.RecordType.Name + "))]");

				mConvert2to1 = GetTransformMethod(DataStorage2.RecordType, DataStorage1.RecordType);
				if (mConvert2to1 == null)
					throw new BadUsageException("You must to define a method in the class " + DataStorage2.RecordType.Name + " with the attribute [TransfortToRecord(typeof(" + DataStorage1.RecordType.Name + "))]");
			}
		}

		private MethodInfo GetTransformMethod(Type sourceType, Type destType)
		{
			MethodInfo[] methods = sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
//			foreach (MethodInfo m in methods)
//			{
//				if (m.IsDefined(typeof (TransformToRecordAttribute), false))
//				{
//					TransformToRecordAttribute ta = (TransformToRecordAttribute) m.GetCustomAttributes(typeof (TransformToRecordAttribute), false)[0];
//					if (ta.TargetType == destType)
//					{
//						return m;
//					}
//				}
//			}

			return null;
		}

	}
}