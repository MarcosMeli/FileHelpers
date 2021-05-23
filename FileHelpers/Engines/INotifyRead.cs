namespace FileHelpers.Events
{
    /// <summary>
    /// Interface used to provide In record notification of read operations.
    /// </summary>
    /// <remarks>INotifyRead and INotifyWrite will only be executed from engines that derive from <see cref="EventEngineBase{T}" />.</remarks>
    /// <example>
    /// <code>
    /// private class SampleType: INotifyRead, INotifyWrite
    /// { ....
    /// 
    ///		public void AfterRead(AfterReadEventArgs e)
    ///		{
    ///			// Your Code Here
    ///		}
    ///		public void BeforeWrite(BeforeReadEventArgs e)
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
        /// Method called by the engines before fill the info of the record and
        /// after read the source line.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void BeforeRead(BeforeReadEventArgs e);

        /// <summary>
        /// Method called by the engines after read a record from the source data.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void AfterRead(AfterReadEventArgs e);

    }
}