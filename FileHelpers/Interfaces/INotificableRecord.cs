using System;

namespace FileHelpers
{

	/// <summary>
	/// Interface used to provide In record notification of read operations.
	/// </summary>
	/// <example>
	/// <code>
	/// private class SampleType: INotifyRead, INotifyWrite
	/// { ....
	/// 
	///		public void AfterRead(EngineBase engine, string line)
	///		{
	///			// Your Code Here
	///		}
	///		public void BeforeWrite(EngineBase engine)
	///		{
	/// 		// Your Code Here
	///		}
	/// 
	/// }
	/// </code>
	/// </example>
	public interface INotifyRead
	{
		/// <summary>
		/// Method called by the engines after read a record from the source data.
		/// </summary>
		/// <param name="engine">The engine that makes the call.</param>
		/// <param name="line">The source line.</param>
		void AfterRead(EngineBase engine, string line);
	}

	/// <summary>
	/// Interface used to provide <b>In record notification of write operations.</b>
	/// </summary>
	/// <example>
	/// <code>
	/// private class SampleType: INotifyRead, INotifyWrite
	/// { ....
	/// 
	///		public void AfterRead(EngineBase engine, string line)
	///		{
	///			// Your Code Here
	///		}
	///		public void BeforeWrite(EngineBase engine)
	///		{
	/// 		// Your Code Here
	///		}
	/// 
	/// }
	/// </code>
	/// </example>
	public interface INotifyWrite
	{
		/// <summary>
		/// Method called by the engines before write a record to the destination stream.
		/// </summary>
		/// <param name="engine">The engine that makes the call.</param>
		void BeforeWrite(EngineBase engine);
	}

}
