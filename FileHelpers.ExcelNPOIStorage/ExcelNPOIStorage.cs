using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers.DataLink;

namespace FileHelpers.ExcelNPOIStorage {

	public class ExcelNPOIStorage :DataStorage{
	    
        public ExcelNPOIStorage(Type recordClass)
	        : base(recordClass)
	    {
	    }

	    public override object[] ExtractRecords() {
			throw new NotImplementedException();
		}

		public override void InsertRecords( object[] records ) {
			throw new NotImplementedException();
		}
	}
}
