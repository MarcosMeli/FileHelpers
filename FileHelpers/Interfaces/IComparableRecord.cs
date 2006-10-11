using System;

namespace FileHelpers
{

	/// <summary>Used by the FileDiffEngine to compare records. Your record class must implement this interface if you like to work with it.</summary>
	public interface IComparableRecord
	{
		/// <summary>
		/// Compare two records and return true if are equal.
		/// </summary>
		/// <param name="record">The other record.</param>
		/// <returns>Returns true only if the records are equals.</returns>
		bool IsEqualRecord(object record);
	}

}
