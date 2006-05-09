using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FileHelpers.DataLink;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmEasySample.
	/// </summary>
	public class frmDataLinkSample : frmFather
	{
		private Button cmdRun;
		private RichTextBox richTextBox1;
		private Label label1;
		private Label label2;
		private RichTextBox richTextBox2;
		private Label lblStatus;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmDataLinkSample()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdRun = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(400, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(672, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.White;
			this.cmdRun.Location = new System.Drawing.Point(312, 8);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(152, 32);
			this.cmdRun.TabIndex = 3;
			this.cmdRun.Text = "RUN >>";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(8, 240);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.ShowSelectionMargin = true;
			this.richTextBox1.Size = new System.Drawing.Size(752, 240);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "{\\rtf1\\ansi\\ansicpg1252\\uc1\\deff0\\stshfdbch0\\stshfloch0\\stshfhich0\\stshfbi0\\defla" +
				"ng1033\\deflangfe1033{\\fonttbl{\\f0\\froman\\fcharset0\\fprq2{\\*\\panose 0202060305040" +
				"5020304}Times New Roman;}\n{\\f2\\fmodern\\fcharset0\\fprq1{\\*\\panose 020703090202050" +
				"20404}Courier New;}{\\f36\\froman\\fcharset238\\fprq2 Times New Roman CE;}{\\f37\\from" +
				"an\\fcharset204\\fprq2 Times New Roman Cyr;}{\\f39\\froman\\fcharset161\\fprq2 Times N" +
				"ew Roman Greek;}\n{\\f40\\froman\\fcharset162\\fprq2 Times New Roman Tur;}{\\f41\\froma" +
				"n\\fcharset177\\fprq2 Times New Roman (Hebrew);}{\\f42\\froman\\fcharset178\\fprq2 Tim" +
				"es New Roman (Arabic);}{\\f43\\froman\\fcharset186\\fprq2 Times New Roman Baltic;}\n{" +
				"\\f44\\froman\\fcharset163\\fprq2 Times New Roman (Vietnamese);}{\\f56\\fmodern\\fchars" +
				"et238\\fprq1 Courier New CE;}{\\f57\\fmodern\\fcharset204\\fprq1 Courier New Cyr;}{\\f" +
				"59\\fmodern\\fcharset161\\fprq1 Courier New Greek;}\n{\\f60\\fmodern\\fcharset162\\fprq1" +
				" Courier New Tur;}{\\f61\\fmodern\\fcharset177\\fprq1 Courier New (Hebrew);}{\\f62\\fm" +
				"odern\\fcharset178\\fprq1 Courier New (Arabic);}{\\f63\\fmodern\\fcharset186\\fprq1 Co" +
				"urier New Baltic;}\n{\\f64\\fmodern\\fcharset163\\fprq1 Courier New (Vietnamese);}}{\\" +
				"colortbl;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\gr" +
				"een255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;" +
				"\\red255\\green255\\blue255;\n\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\gree" +
				"n128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\\blue0;\\red128\\green128\\blue0;\\r" +
				"ed128\\green128\\blue128;\\red192\\green192\\blue192;}{\\stylesheet{\n\\ql \\li0\\ri0\\widc" +
				"tlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang1033\\langfe10" +
				"33\\cgrid\\langnp1033\\langfenp1033 \\snext0 Normal;}{\\*\\cs10 \\additive \\ssemihidden" +
				" Default Paragraph Font;}{\\*\n\\ts11\\tsrowd\\trftsWidthB3\\trpaddl108\\trpaddr108\\trp" +
				"addfl3\\trpaddft3\\trpaddfb3\\trpaddfr3\\trcbpat1\\trcfpat1\\tscellwidthfts0\\tsvertalt" +
				"\\tsbrdrt\\tsbrdrl\\tsbrdrb\\tsbrdrr\\tsbrdrdgl\\tsbrdrdgr\\tsbrdrh\\tsbrdrv \n\\ql \\li0\\r" +
				"i0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs20\\lang1024\\l" +
				"angfe1024\\cgrid\\langnp1024\\langfenp1024 \\snext11 \\ssemihidden Normal Table;}}{\\*" +
				"\\latentstyles\\lsdstimax156\\lsdlockeddef0}{\\*\\rsidtbl \\rsid1600206}{\\*\\generator " +
				"Micro\nsoft Word 11.0.5604;}{\\info{\\author Marcos}{\\operator Marcos}{\\creatim\\yr2" +
				"005\\mo9\\dy27\\hr22\\min44}{\\revtim\\yr2005\\mo9\\dy27\\hr22\\min45}{\\version1}{\\edmins1" +
				"}{\\nofpages2}{\\nofwords255}{\\nofchars1455}{\\*\\company Yo}{\\nofcharsws1707}{\\vern" +
				"24689}}\n\\widowctrl\\ftnbj\\aenddoc\\noxlattoyen\\expshrtn\\noultrlspc\\dntblnsbdb\\nosp" +
				"aceforul\\formshade\\horzdoc\\dgmargin\\dghspace180\\dgvspace180\\dghorigin1800\\dgvori" +
				"gin1440\\dghshow1\\dgvshow1\n\\jexpand\\viewkind1\\viewscale100\\pgbrdrhead\\pgbrdrfoot\\" +
				"splytwnine\\ftnlytwnine\\htmautsp\\nolnhtadjtbl\\useltbaln\\alntblind\\lytcalctblwd\\ly" +
				"ttblrtgr\\lnbrkrule\\nobrkwrptbl\\snaptogridincell\\allowfieldendsel\\wrppunct\n\\asian" +
				"brkrule\\rsidroot1600206\\newtblstyruls\\nogrowautofit \\fet0\\sectd \\linex0\\endnhere" +
				"\\sectlinegrid360\\sectdefaultcl\\sftnbj {\\*\\pnseclvl1\\pnucrm\\pnstart1\\pnindent720\\" +
				"pnhang {\\pntxta .}}{\\*\\pnseclvl2\\pnucltr\\pnstart1\\pnindent720\\pnhang {\\pntxta .}" +
				"}\n{\\*\\pnseclvl3\\pndec\\pnstart1\\pnindent720\\pnhang {\\pntxta .}}{\\*\\pnseclvl4\\pnlc" +
				"ltr\\pnstart1\\pnindent720\\pnhang {\\pntxta )}}{\\*\\pnseclvl5\\pndec\\pnstart1\\pninden" +
				"t720\\pnhang {\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl6\\pnlcltr\\pnstart1\\pnindent720\\pn" +
				"hang {\\pntxtb (}\n{\\pntxta )}}{\\*\\pnseclvl7\\pnlcrm\\pnstart1\\pnindent720\\pnhang {\\" +
				"pntxtb (}{\\pntxta )}}{\\*\\pnseclvl8\\pnlcltr\\pnstart1\\pnindent720\\pnhang {\\pntxtb " +
				"(}{\\pntxta )}}{\\*\\pnseclvl9\\pnlcrm\\pnstart1\\pnindent720\\pnhang {\\pntxtb (}{\\pntx" +
				"ta )}}\\pard\\plain \n\\ql \\li0\\ri0\\widctlpar\\faauto\\rin0\\lin0\\itap0\\pararsid1600206" +
				" \\fs24\\lang1033\\langfe1033\\cgrid\\langnp1033\\langfenp1033 {\\f2\\fs20\\insrsid160020" +
				"6 \\tab }{\\f2\\fs20\\cf2\\insrsid1600206 public}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20" +
				"\\cf2\\insrsid1600206 class}{\n\\f2\\fs20\\insrsid1600206  CustomersVerticalBarLinkPro" +
				"vider : AccessDataLinkProvider\n\\par \\tab \\{\n\\par \\tab \\tab }{\\f2\\fs20\\cf2\\insrsi" +
				"d1600206 private}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 string}" +
				"{\\f2\\fs20\\insrsid1600206  mAccessFileName = @\"..\\\\..\\\\FileHelpersTests\\\\data\\\\Te" +
				"stData.mdb\";\n\\par \n\\par }{\\f2\\fs20\\cf2\\insrsid1600206 \\tab \\tab #region}{\\f2\\fs2" +
				"0\\insrsid1600206  \"  Constructors  \"\n\\par \n\\par \\tab \\tab }{\\f2\\fs20\\cf2\\insrsid" +
				"1600206 public}{\\f2\\fs20\\insrsid1600206  CustomersVerticalBarLinkProvider()\n\\par" +
				" \\tab \\tab \\{\n\\par \\tab \\tab \\}\n\\par \n\\par \\tab \\tab }{\\f2\\fs20\\cf2\\insrsid16002" +
				"06 public}{\\f2\\fs20\\insrsid1600206  CustomersVerticalBarLinkProvider(}{\\f2\\fs20\\" +
				"cf2\\insrsid1600206 string}{\\f2\\fs20\\insrsid1600206  fileName)\n\\par \\tab \\tab \\{\n" +
				"\\par \\tab \\tab \\tab mAccessFileName = fileName;\n\\par \\tab \\tab \\}\n\\par \n\\par }{\\" +
				"f2\\fs20\\cf2\\insrsid1600206 \\tab \\tab #endregion\n\\par \n\\par \\tab \\tab #region}{\\f" +
				"2\\fs20\\insrsid1600206  \"  RecordType  \"\n\\par \n\\par \\tab \\tab }{\\f2\\fs20\\cf2\\insr" +
				"sid1600206 public}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 overri" +
				"de}{\\f2\\fs20\\insrsid1600206  Type RecordType\n\\par \\tab \\tab \\{\n\\par \\tab \\tab \\t" +
				"ab }{\\f2\\fs20\\cf2\\insrsid1600206 get}{\\f2\\fs20\\insrsid1600206  \\{ }{\\f2\\fs20\\cf2" +
				"\\insrsid1600206 return}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 t" +
				"ypeof}{\\f2\\fs20\\insrsid1600206  (CustomersVerticalBar); \\}\n\\par \\tab \\tab \\}\n\\pa" +
				"r \n\\par }{\\f2\\fs20\\cf2\\insrsid1600206 \\tab \\tab #endregion\n\\par \n\\par \\tab \\tab " +
				"#region}{\\f2\\fs20\\insrsid1600206  \"  FillRecord  \"\n\\par \n\\par \\tab \\tab }{\\f2\\fs" +
				"20\\cf2\\insrsid1600206 protected}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid" +
				"1600206 override}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 object}" +
				"{\\f2\\fs20\\insrsid1600206  FillRecord(}{\\f2\\fs20\\cf2\\insrsid1600206 object}{\n\\f2\\" +
				"fs20\\insrsid1600206 [] fields)\n\\par \\tab \\tab \\{\n\\par \\tab \\tab \\tab CustomersVe" +
				"rticalBar record = }{\\f2\\fs20\\cf2\\insrsid1600206 new}{\\f2\\fs20\\insrsid1600206  C" +
				"ustomersVerticalBar();\n\\par \n\\par \\tab \\tab \\tab record.CustomerID = (}{\\f2\\fs20" +
				"\\cf2\\insrsid1600206 string}{\\f2\\fs20\\insrsid1600206 ) fields[0];\n\\par \\tab \\tab " +
				"\\tab record.CompanyName = (}{\\f2\\fs20\\cf2\\insrsid1600206 string}{\\f2\\fs20\\insrsi" +
				"d1600206 ) fields[1];\n\\par \\tab \\tab \\tab record.ContactName = (}{\\f2\\fs20\\cf2\\i" +
				"nsrsid1600206 string}{\\f2\\fs20\\insrsid1600206 ) fields[2];\n\\par \\tab \\tab \\tab r" +
				"ecord.ContactTitle = (}{\\f2\\fs20\\cf2\\insrsid1600206 string}{\\f2\\fs20\\insrsid1600" +
				"206 ) fields[3];\n\\par \\tab \\tab \\tab record.Address = (}{\\f2\\fs20\\cf2\\insrsid160" +
				"0206 string}{\\f2\\fs20\\insrsid1600206 ) fields[4];\n\\par \\tab \\tab \\tab record.Cit" +
				"y = (}{\\f2\\fs20\\cf2\\insrsid1600206 string}{\\f2\\fs20\\insrsid1600206 ) fields[5];\n" +
				"\\par \\tab \\tab \\tab record.Country = (}{\\f2\\fs20\\cf2\\insrsid1600206 string}{\\f2\\" +
				"fs20\\insrsid1600206 ) fields[6];\n\\par \n\\par \\tab \\tab \\tab }{\\f2\\fs20\\cf2\\insrsi" +
				"d1600206 return}{\\f2\\fs20\\insrsid1600206  record;\n\\par \\tab \\tab \\}\n\\par \n\\par }" +
				"{\\f2\\fs20\\cf2\\insrsid1600206 \\tab \\tab #endregion\n\\par \n\\par \\tab \\tab #region}{" +
				"\\f2\\fs20\\insrsid1600206  \"  GetSelectSql  \"\n\\par \n\\par \\tab \\tab }{\\f2\\fs20\\cf2\\" +
				"insrsid1600206 protected}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206" +
				" override}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 string}{\\f2\\fs" +
				"20\\insrsid1600206  GetSelectSql()\n\\par \\tab \\tab \\{\n\\par \\tab \\tab \\tab }{\\f2\\fs" +
				"20\\cf2\\insrsid1600206 return}{\\f2\\fs20\\insrsid1600206  \"SELECT * FROM Customers\"" +
				";\n\\par \\tab \\tab \\}\n\\par \n\\par }{\\f2\\fs20\\cf2\\insrsid1600206 \\tab \\tab #endregio" +
				"n\n\\par \n\\par \\tab \\tab #region}{\\f2\\fs20\\insrsid1600206  \"  GetInsertSql  \"\n\\par" +
				" \n\\par \\tab \\tab }{\\f2\\fs20\\cf2\\insrsid1600206 protected}{\\f2\\fs20\\insrsid160020" +
				"6  }{\\f2\\fs20\\cf2\\insrsid1600206 override}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\c" +
				"f2\\insrsid1600206 string}{\\f2\\fs20\\insrsid1600206  GetInsertSql(}{\\f2\\fs20\\cf2\\i" +
				"nsrsid1600206 object}{\n\\f2\\fs20\\insrsid1600206  record)\n\\par \\tab \\tab \\{\n\\par \\" +
				"tab \\tab \\tab CustomersVerticalBar obj = (CustomersVerticalBar) record;\n\\par \n\\p" +
				"ar \\tab \\tab \\tab }{\\f2\\fs20\\cf2\\insrsid1600206 return}{\\f2\\fs20\\insrsid1600206 " +
				" String.Format(\"INSERT INTO Customers (Address, City, CompanyName, ContactName, " +
				"ContactTitle, Country, CustomerID) \" + \n\\par \\tab \\tab \\tab \\tab \" VALUES ( \\\\\"\\" +
				"{0\\}\\\\\" , \\\\\"\\{1\\}\\\\\" , \\\\\"\\{2\\}\\\\\" , \\\\\"\\{3\\}\\\\\" , \\\\\"\\{4\\}\\\\\" , \\\\\"\\{5\\}\\\\\" , " +
				"\\\\\"\\{6\\}\\\\\"  ); \",\n\\par \\tab \\tab \\tab \\tab obj.Address,\n\\par \\tab \\tab \\tab \\ta" +
				"b obj.City,\n\\par \\tab \\tab \\tab \\tab obj.CompanyName,\n\\par \\tab \\tab \\tab \\tab o" +
				"bj.ContactName,\n\\par \\tab \\tab \\tab \\tab obj.ContactTitle,\n\\par \\tab \\tab \\tab \\" +
				"tab obj.Country,\n\\par \\tab \\tab \\tab \\tab obj.CustomerID\n\\par \\tab \\tab \\tab \\ta" +
				"b );\n\\par \\tab \\tab \\}\n\\par \n\\par }{\\f2\\fs20\\cf2\\insrsid1600206 \\tab \\tab #endre" +
				"gion\n\\par \n\\par }{\\f2\\fs20\\insrsid1600206 \\tab \\tab }{\\f2\\fs20\\cf2\\insrsid160020" +
				"6 public}{\\f2\\fs20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 override}{\\f2\\f" +
				"s20\\insrsid1600206  }{\\f2\\fs20\\cf2\\insrsid1600206 string}{\\f2\\fs20\\insrsid160020" +
				"6  MdbFileName\n\\par \\tab \\tab \\{\n\\par \\tab \\tab \\tab }{\\f2\\fs20\\cf2\\insrsid16002" +
				"06 get}{\\f2\\fs20\\insrsid1600206  \\{ }{\\f2\\fs20\\cf2\\insrsid1600206 return}{\\f2\\fs" +
				"20\\insrsid1600206  mAccessFileName; \\}\n\\par \\tab \\tab \\}\n\\par }\\pard \\ql \\li0\\ri" +
				"0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0\\pararsid1600206 {" +
				"\\f2\\fs20\\insrsid1600206 \\tab \\}}{\\insrsid1600206 \n\\par }}";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(8, 216);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(432, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Class generated by the Code Smith templates included in the release";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(8, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Code Used by the Run Button";
			// 
			// richTextBox2
			// 
			this.richTextBox2.Location = new System.Drawing.Point(8, 72);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ReadOnly = true;
			this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox2.ShowSelectionMargin = true;
			this.richTextBox2.Size = new System.Drawing.Size(752, 136);
			this.richTextBox2.TabIndex = 7;
			this.richTextBox2.Text = "{\\rtf1\\ansi\\ansicpg1252\\uc1\\deff0\\stshfdbch0\\stshfloch0\\stshfhich0\\stshfbi0\\defla" +
				"ng1033\\deflangfe1033{\\fonttbl{\\f0\\froman\\fcharset0\\fprq2{\\*\\panose 0202060305040" +
				"5020304}Times New Roman;}\n{\\f2\\fmodern\\fcharset0\\fprq1{\\*\\panose 020703090202050" +
				"20404}Courier New;}{\\f36\\froman\\fcharset238\\fprq2 Times New Roman CE;}{\\f37\\from" +
				"an\\fcharset204\\fprq2 Times New Roman Cyr;}{\\f39\\froman\\fcharset161\\fprq2 Times N" +
				"ew Roman Greek;}\n{\\f40\\froman\\fcharset162\\fprq2 Times New Roman Tur;}{\\f41\\froma" +
				"n\\fcharset177\\fprq2 Times New Roman (Hebrew);}{\\f42\\froman\\fcharset178\\fprq2 Tim" +
				"es New Roman (Arabic);}{\\f43\\froman\\fcharset186\\fprq2 Times New Roman Baltic;}\n{" +
				"\\f44\\froman\\fcharset163\\fprq2 Times New Roman (Vietnamese);}{\\f56\\fmodern\\fchars" +
				"et238\\fprq1 Courier New CE;}{\\f57\\fmodern\\fcharset204\\fprq1 Courier New Cyr;}{\\f" +
				"59\\fmodern\\fcharset161\\fprq1 Courier New Greek;}\n{\\f60\\fmodern\\fcharset162\\fprq1" +
				" Courier New Tur;}{\\f61\\fmodern\\fcharset177\\fprq1 Courier New (Hebrew);}{\\f62\\fm" +
				"odern\\fcharset178\\fprq1 Courier New (Arabic);}{\\f63\\fmodern\\fcharset186\\fprq1 Co" +
				"urier New Baltic;}\n{\\f64\\fmodern\\fcharset163\\fprq1 Courier New (Vietnamese);}}{\\" +
				"colortbl;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\gr" +
				"een255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;" +
				"\\red255\\green255\\blue255;\n\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\gree" +
				"n128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\\blue0;\\red128\\green128\\blue0;\\r" +
				"ed128\\green128\\blue128;\\red192\\green192\\blue192;}{\\stylesheet{\n\\ql \\li0\\ri0\\widc" +
				"tlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang1033\\langfe10" +
				"33\\cgrid\\langnp1033\\langfenp1033 \\snext0 Normal;}{\\*\\cs10 \\additive \\ssemihidden" +
				" Default Paragraph Font;}{\\*\n\\ts11\\tsrowd\\trftsWidthB3\\trpaddl108\\trpaddr108\\trp" +
				"addfl3\\trpaddft3\\trpaddfb3\\trpaddfr3\\trcbpat1\\trcfpat1\\tscellwidthfts0\\tsvertalt" +
				"\\tsbrdrt\\tsbrdrl\\tsbrdrb\\tsbrdrr\\tsbrdrdgl\\tsbrdrdgr\\tsbrdrh\\tsbrdrv \n\\ql \\li0\\r" +
				"i0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs20\\lang1024\\l" +
				"angfe1024\\cgrid\\langnp1024\\langfenp1024 \\snext11 \\ssemihidden Normal Table;}}{\\*" +
				"\\latentstyles\\lsdstimax156\\lsdlockeddef0}{\\*\\rsidtbl \\rsid11826382}{\\*\\generator" +
				" Micr\nosoft Word 11.0.5604;}{\\info{\\title FileDataLink mLinkEngine = new FileDat" +
				"aLink(new CustomersVerticalBarLinkProvider());}{\\author Marcos}{\\operator Marcos" +
				"}{\\creatim\\yr2005\\mo9\\dy27\\hr23\\min34}{\\revtim\\yr2005\\mo9\\dy27\\hr23\\min36}{\\vers" +
				"ion1}{\\edmins1}\n{\\nofpages1}{\\nofwords38}{\\nofchars217}{\\*\\company Yo}{\\nofchars" +
				"ws254}{\\vern24689}}\\widowctrl\\ftnbj\\aenddoc\\noxlattoyen\\expshrtn\\noultrlspc\\dntb" +
				"lnsbdb\\nospaceforul\\formshade\\horzdoc\\dgmargin\\dghspace180\\dgvspace180\\dghorigin" +
				"1800\\dgvorigin1440\\dghshow1\n\\dgvshow1\\jexpand\\viewkind1\\viewscale100\\pgbrdrhead\\" +
				"pgbrdrfoot\\splytwnine\\ftnlytwnine\\htmautsp\\nolnhtadjtbl\\useltbaln\\alntblind\\lytc" +
				"alctblwd\\lyttblrtgr\\lnbrkrule\\nobrkwrptbl\\snaptogridincell\\allowfieldendsel\\wrpp" +
				"unct\n\\asianbrkrule\\rsidroot11826382\\newtblstyruls\\nogrowautofit \\fet0\\sectd \\lin" +
				"ex0\\sectdefaultcl\\sectrsid9600568\\sftnbj {\\*\\pnseclvl1\\pnucrm\\pnstart1\\pnindent7" +
				"20\\pnhang {\\pntxta .}}{\\*\\pnseclvl2\\pnucltr\\pnstart1\\pnindent720\\pnhang {\\pntxta" +
				" .}}{\\*\\pnseclvl3\n\\pndec\\pnstart1\\pnindent720\\pnhang {\\pntxta .}}{\\*\\pnseclvl4\\p" +
				"nlcltr\\pnstart1\\pnindent720\\pnhang {\\pntxta )}}{\\*\\pnseclvl5\\pndec\\pnstart1\\pnin" +
				"dent720\\pnhang {\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl6\\pnlcltr\\pnstart1\\pnindent720" +
				"\\pnhang {\\pntxtb (}{\\pntxta )}}\n{\\*\\pnseclvl7\\pnlcrm\\pnstart1\\pnindent720\\pnhang" +
				" {\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl8\\pnlcltr\\pnstart1\\pnindent720\\pnhang {\\pntx" +
				"tb (}{\\pntxta )}}{\\*\\pnseclvl9\\pnlcrm\\pnstart1\\pnindent720\\pnhang {\\pntxtb (}{\\p" +
				"ntxta )}}\\pard\\plain \n\\ql \\li0\\ri0\\widctlpar\\faauto\\rin0\\lin0\\itap0\\pararsid1182" +
				"6382 \\fs24\\lang1033\\langfe1033\\cgrid\\langnp1033\\langfenp1033 {\\f2\\fs20\\insrsid11" +
				"826382 FileDataLink mLinkEngine = }{\\f2\\fs20\\cf2\\insrsid11826382 new }{\\f2\\fs20\\" +
				"insrsid11826382 FileDataLink(}{\n\\f2\\fs20\\cf2\\insrsid11826382 new}{\\f2\\fs20\\insrs" +
				"id11826382  CustomersVerticalBarLinkProvider());\n\\par \n\\par }{\\f2\\fs20\\cf11\\insr" +
				"sid11826382 // Db -> File\n\\par }{\\f2\\fs20\\insrsid11826382 mLinkEngine.ExtractToF" +
				"ile(@\"..\\\\..\\\\FileHelpersTests\\\\data\\\\temp.txt\");\n\\par }{\\f2\\fs20\\insrsid1182638" +
				"2 \n\\par }{\\f2\\fs20\\cf11\\insrsid11826382 // File -> }{\\f2\\fs20\\cf11\\insrsid118263" +
				"82 D}{\\f2\\fs20\\cf11\\insrsid11826382 b}{\\f2\\fs20\\cf11\\insrsid11826382 \n\\par }{\\f2" +
				"\\fs20\\insrsid11826382 mLinkEngine.InsertFromFile(@\"..\\\\..\\\\FileHelpersTests\\\\dat" +
				"a\\\\temp.txt\");}{\\f2\\fs20\\cf11\\insrsid11826382\\charrsid11826382 \n\\par }\\pard \\ql " +
				"\\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 {\\insrsid1" +
				"1826382 \n\\par }}";
			this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(232, 54);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(568, 16);
			this.lblStatus.TabIndex = 8;
			// 
			// frmDataLinkSample
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(770, 512);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.richTextBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmDataLinkSample";
			this.Text = "Example of the DataLink Feature";
			this.Load += new System.EventHandler(this.frmDataLinkSample_Load);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.richTextBox1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.richTextBox2, 0);
			this.Controls.SetChildIndex(this.lblStatus, 0);
			this.ResumeLayout(false);

		}

		#endregion

		
		private object FillRecord(object[] fields)
		{
			CustomersVerticalBar record = new CustomersVerticalBar();

			record.CustomerID = (string) fields[0];
			record.CompanyName = (string) fields[1];
			record.ContactName = (string) fields[2];
			record.ContactTitle = (string) fields[3];
			record.Address = (string) fields[4];
			record.City = (string) fields[5];
			record.Country = (string) fields[6];

			return record;
		}
		
		private string GetInsertSql(object record)
		{
			CustomersVerticalBar obj = (CustomersVerticalBar) record;

			return String.Format("INSERT INTO Customers (Address, City, CompanyName, ContactName, ContactTitle, Country, CustomerID) " +
				" VALUES ( \"{0}\" , \"{1}\" , \"{2}\" , \"{3}\" , \"{4}\" , \"{5}\" , \"{6}\"  ); ",
				obj.Address,
				obj.City,
				obj.CompanyName,
				obj.ContactName,
				obj.ContactTitle,
				obj.Country,
				obj.CustomerID
				);

		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			try
			{
				lblStatus.Text = "Creating the DataLinkEngine";

				AccessStorage storage = new AccessStorage(typeof(CustomersVerticalBar), "TestData.mdb");
				
				storage.FillRecordCallback = new FillRecordHandler(FillRecord);
				storage.GetInsertSqlCallback = new GetInsertSqlHandler(GetInsertSql);

				storage.SelectSql = "SELECT * FROM Customers";

				FileDataLink mLink = new FileDataLink(storage);

				Application.DoEvents();
				Thread.Sleep(800);

				// Db -> File
				lblStatus.Text = "Extracting records from the access db to a file";
				mLink.ExtractToFile(@"temp.txt");
				Application.DoEvents();
				Thread.Sleep(800);

				// File -> Db
				lblStatus.Text = "Inserting records from a file to the access db";
				mLink.InsertFromFile(@"temp.txt");

				if (File.Exists(@"temp.txt"))
					File.Delete(@"temp.txt");

				Application.DoEvents();
				Thread.Sleep(800);


				lblStatus.Text = "DONE !!!";

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Unexpected Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void frmDataLinkSample_Load(object sender, EventArgs e)
		{
			richTextBox1.Rtf = richTextBox1.Text;
			richTextBox2.Rtf = richTextBox2.Text;
		}

		private void richTextBox2_TextChanged(object sender, EventArgs e)
		{
		}

	}
}