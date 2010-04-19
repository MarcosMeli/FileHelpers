using FileHelpers.Events;

namespace FileHelpers.Events
{
    /// <summary>
    /// Interface used to provide In record notification of read operations.
    /// </summary>
    /// <example>
    /// <code>
    /// private class SampleType: INotifyRead, INotifyWrite
    /// { ....
    /// 
    ///		public void AfterRead(AfterReadRecordEventArgs<SampleType> e)
    ///		{
    ///			// Your Code Here
    ///		}
    ///		public void BeforeWrite(BeforeWriteRecordEventArgs<SampleType> e engine)
    ///		{
    /// 		// Your Code Here
    ///		}
    /// 
    /// }
    /// </code>
    /// </example>
    public interface INotifyRead<T>
        where T : class
    {
        /// <summary>
        /// Method called by the engines after read a record from the source data.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void AfterRead(AfterReadEventArgs<T> e);

        /// <summary>
        /// Method called by the engines before fill the info of the record and after read the source line.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void BeforeRead(BeforeReadEventArgs<T> e);
    }
}