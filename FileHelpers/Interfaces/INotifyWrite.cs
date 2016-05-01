using System;
using System.Collections;
using System.Collections.Generic;

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
    ///		public void AfterRead(BeforeWriteEventArgs e)
    ///		{
    ///			// Your Code Here
    ///		}
    ///		public void BeforeWrite(AfterWriteEventArgs e)
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
        /// Method called by the engines before write a record to the
        /// destination stream.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void BeforeWrite(BeforeWriteEventArgs e);

        /// <summary>
        /// Method called by the engines after write a record to the
        /// destination stream.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void AfterWrite(AfterWriteEventArgs e);
    }
}