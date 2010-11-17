using System;

namespace FileHelpers
{

    /// <summary>
    /// Used by the FileDiffEngine to compare records. Your record class must
    /// implement this interface if you like to work with it.
    /// </summary>
	public interface IComparableRecord<T>
	{
		/// <summary>
		/// Compare two records and return true if are equal.
		/// </summary>
		/// <param name="other">The other record.</param>
		/// <returns>Returns true only if the records are equals.</returns>
		bool IsEqualRecord(T other);
	}
}
