using FileHelpers;

namespace FileHelpersSamples
{
	public class MainClass
	{
		public static void Main()
		{
//			CustomerExcelDataLink provider  = new CustomerExcelDataLink();
//			object[] res = provider.ExtractRecords();
//
//			provider.FileName = @"d:\salida.xls";
//
//			provider.StartRow = 10;
//			provider.StartColumn = 5;
//			provider.InsertRecords(res);


			frmSamples frm = new frmSamples();
			frm.ShowDialog();
			frm.Dispose();
		}
	}
}