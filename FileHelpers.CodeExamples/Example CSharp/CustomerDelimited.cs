using System;
using FileHelpers;

namespace Examples
{

	[DelimitedRecord(",")]
	public class Customer
	{
		public int CustId;
		
		public string Name;

		public decimal Balance;

		[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
		public DateTime AddedDate;

		#region "  only for the transformation example  "
		 
		[TransformToRecord(typeof(Customer2))]
		public Customer2 CreateSimilar()
		{
			Customer2 res = new Customer2();

			res.CustId = this.CustId;
			res.Name = this.Name;
			res.Balance = this.Balance;
			res.AddedDate = this.AddedDate;

			return res;
		}

		#endregion
	}

}
