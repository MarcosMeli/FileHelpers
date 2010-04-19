

using System;

namespace FileHelpers.Events
{
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
	public interface INotifyWrite<T> 
        where T : class
    {
		/// <summary>
		/// Method called by the engines before write a record to the destination stream.
		/// </summary>
        /// <param name="e">The Event Info</param>
        void BeforeWrite(BeforeWriteEventArgs<T> e);

        /// <summary>
        /// Method called by the engines after write a record to the destination stream.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void AfterWrite(AfterWriteEventArgs<T> e);

    }

}
