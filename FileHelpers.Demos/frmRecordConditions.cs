using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
	/// <summary>
    /// Example of reading a file with a selection criteria
    /// on the records.
    /// Read customer details beginning with F and display
    /// on a grid.
	/// </summary>
	public class frmRecordConditions: frmFather
	{
		private TextBox txtClass;
		private TextBox txtData;
		private PropertyGrid grid1;
		private Button cmdRun;
		private Label label2;
		private Label label1;
		private Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmRecordConditions()
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
			this.txtClass = new System.Windows.Forms.TextBox();
			this.txtData = new System.Windows.Forms.TextBox();
			this.grid1 = new System.Windows.Forms.PropertyGrid();
			this.cmdRun = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(642, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// txtClass
			// 
			this.txtClass.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClass.Location = new System.Drawing.Point(8, 136);
			this.txtClass.Multiline = true;
			this.txtClass.Name = "txtClass";
			this.txtClass.ReadOnly = true;
			this.txtClass.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtClass.Size = new System.Drawing.Size(416, 160);
			this.txtClass.TabIndex = 0;
			this.txtClass.Text = @"[ConditionalRecord(RecordCondition.IncludeIfBegins, ""F"")]
[DelimitedRecord(""|"")]
public class CustomersVerticalBar
{
	public string CustomerID;
	public string CompanyName;
	public string ContactName;
	public string ContactTitle;
	public string Address;
	public string City;
	public string Country;
}";
			this.txtClass.WordWrap = false;
			// 
			// txtData
			// 
			this.txtData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtData.Location = new System.Drawing.Point(8, 320);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.ReadOnly = true;
			this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtData.Size = new System.Drawing.Size(728, 144);
			this.txtData.TabIndex = 1;
			this.txtData.Text = "ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|" +
				"Germany\r\nANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la" +
				" Constitución 2222|México D.F.|Mexico\r\nANTON|Antonio Moreno Taquería|Antonio Mor" +
				"eno|Owner|Mataderos  2312|México D.F.|Mexico\r\nAROUT|Around the Horn|Thomas Hardy" +
				"|Sales Representative|120 Hanover Sq.|London|UK\r\nBERGS|Berglunds snabbköp|Christ" +
				"ina Berglund|Order Administrator|Berguvsvägen  8|Luleå|Sweden\r\nBLAUS|Blauer See " +
				"Delikatessen|Hanna Moos|Sales Representative|Forsterstr. 57|Mannheim|Germany\r\nBL" +
				"ONP|Blondesddsl père et fils|Frédérique Citeaux|Marketing Manager|24, place Kléb" +
				"er|Strasbourg|France\r\nBOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Ara" +
				"quil, 67|Madrid|Spain\r\nBONAP|Bon app\'|Laurence Lebihan|Owner|12, rue des Boucher" +
				"s|Marseille|France\r\nBOTTM|Bottom-Dollar Markets|Elizabeth Lincoln|Accounting Man" +
				"ager|23 Tsawassen Blvd.|Tsawassen|Canada\r\nBSBEV|B\'s Beverages|Victoria Ashworth|" +
				"Sales Representative|Fauntleroy Circus|London|UK\r\nCACTU|Cactus Comidas para llev" +
				"ar|Patricio Simpson|Sales Agent|Cerrito 333|Buenos Aires|Argentina\r\nCENTC|Centro" +
				" comercial Moctezuma|Francisco Chang|Marketing Manager|Sierras de Granada 9993|M" +
				"éxico D.F.|Mexico\r\nCHOPS|Chop-suey Chinese|Yang Wang|Owner|Hauptstr. 29|Bern|Swi" +
				"tzerland\r\nCOMMI|Comércio Mineiro|Pedro Afonso|Sales Associate|Av. dos Lusíadas, " +
				"23|Sao Paulo|Brazil\r\nCONSH|Consolidated Holdings|Elizabeth Brown|Sales Represent" +
				"ative|Berkeley Gardens 12  Brewery|London|UK\r\nDRACD|Drachenblut Delikatessen|Sve" +
				"n Ottlieb|Order Administrator|Walserweg 21|Aachen|Germany\r\nDUMON|Du monde entier" +
				"|Janine Labrune|Owner|67, rue des Cinquante Otages|Nantes|France\r\nEASTC|Eastern " +
				"Connection|Ann Devon|Sales Agent|35 King George|London|UK\r\nERNSH|Ernst Handel|Ro" +
				"land Mendel|Sales Manager|Kirchgasse 6|Graz|Austria\r\nFAMIA|Familia Arquibaldo|Ar" +
				"ia Cruz|Marketing Assistant|Rua Orós, 92|Sao Paulo|Brazil\r\nFISSA|FISSA Fabrica I" +
				"nter. Salchichas S.A.|Diego Roel|Accounting Manager|C/ Moralzarzal, 86|Madrid|Sp" +
				"ain\r\nFOLIG|Folies gourmandes|Martine Rancé|Assistant Sales Agent|184, chaussée d" +
				"e Tournai|Lille|France\r\nFOLKO|Folk och fä HB|Maria Larsson|Owner|Åkergatan 24|Br" +
				"äcke|Sweden\r\nFRANK|Frankenversand|Peter Franken|Marketing Manager|Berliner Platz" +
				" 43|München|Germany\r\nFRANR|France restauration|Carine Schmitt|Marketing Manager|" +
				"54, rue Royale|Nantes|France\r\nFRANS|Franchi S.p.A.|Paolo Accorti|Sales Represent" +
				"ative|Via Monte Bianco 34|Torino|Italy\r\nFURIB|Furia Bacalhau e Frutos do Mar|Lin" +
				"o Rodriguez|Sales Manager|Jardim das rosas n. 32|Lisboa|Portugal\r\nGALED|Galería " +
				"del gastrónomo|Eduardo Saavedra|Marketing Manager|Rambla de Cataluña, 23|Barcelo" +
				"na|Spain\r\nGODOS|Godos Cocina Típica|José Pedro Freyre|Sales Manager|C/ Romero, 3" +
				"3|Sevilla|Spain\r\nGOURL|Gourmet Lanchonetes|André Fonseca|Sales Associate|Av. Bra" +
				"sil, 442|Campinas|Brazil\r\nGREAL|Great Lakes Food Market|Howard Snyder|Marketing " +
				"Manager|2732 Baker Blvd.|Eugene|USA\r\nGROSR|GROSELLA-Restaurante|Manuel Pereira|O" +
				"wner|5ª Ave. Los Palos Grandes|Caracas|Venezuela\r\nHANAR|Hanari Carnes|Mario Pont" +
				"es|Accounting Manager|Rua do Paço, 67|Rio de Janeiro|Brazil\r\nHILAA|HILARION-Abas" +
				"tos|Carlos Hernández|Sales Representative|Carrera 22 con Ave. Carlos Soublette #" +
				"8-35|San Cristóbal|Venezuela\r\nHUNGC|Hungry Coyote Import Store|Yoshi Latimer|Sal" +
				"es Representative|City Center Plaza 516 Main St.|Elgin|USA\r\nHUNGO|Hungry Owl All" +
				"-Night Grocers|Patricia McKenna|Sales Associate|8 Johnstown Road|Cork|Ireland\r\nI" +
				"SLAT|Island Trading|Helen Bennett|Marketing Manager|Garden House Crowther Way|Co" +
				"wes|UK\r\nKOENE|Königlich Essen|Philip Cramer|Sales Associate|Maubelstr. 90|Brande" +
				"nburg|Germany\r\nLACOR|La corne d\'abondance|Daniel Tonini|Sales Representative|67," +
				" avenue de l\'Europe|Versailles|France\r\nLAMAI|La maison d\'Asie|Annette Roulet|Sal" +
				"es Manager|1 rue Alsace-Lorraine|Toulouse|France\r\nLAUGB|Laughing Bacchus Wine Ce" +
				"llars|Yoshi Tannamuri|Marketing Assistant|1900 Oak St.|Vancouver|Canada\r\nLAZYK|L" +
				"azy K Kountry Store|John Steel|Marketing Manager|12 Orchestra Terrace|Walla Wall" +
				"a|USA\r\nLEHMS|Lehmanns Marktstand|Renate Messner|Sales Representative|Magazinweg " +
				"7|Frankfurt a.M.|Germany\r\nLETSS|Let\'s Stop N Shop|Jaime Yorres|Owner|87 Polk St." +
				" Suite 5|San Francisco|USA\r\nLILAS|LILA-Supermercado|Carlos González|Accounting M" +
				"anager|Carrera 52 con Ave. Bolívar #65-98 Llano Largo|Barquisimeto|Venezuela\r\nLI" +
				"NOD|LINO-Delicateses|Felipe Izquierdo|Owner|Ave. 5 de Mayo Porlamar|I. de Margar" +
				"ita|Venezuela\r\nLONEP|Lonesome Pine Restaurant|Fran Wilson|Sales Manager|89 Chiar" +
				"oscuro Rd.|Portland|USA\r\nMAGAA|Magazzini Alimentari Riuniti|Giovanni Rovelli|Mar" +
				"keting Manager|Via Ludovico il Moro 22|Bergamo|Italy\r\nMAISD|Maison Dewey|Catheri" +
				"ne Dewey|Sales Agent|Rue Joseph-Bens 532|Bruxelles|Belgium\r\nMEREP|Mère Paillarde" +
				"|Jean Fresnière|Marketing Assistant|43 rue St. Laurent|Montréal|Canada\r\nMORGK|Mo" +
				"rgenstern Gesundkost|Alexander Feuer|Marketing Assistant|Heerstr. 22|Leipzig|Ger" +
				"many\r\nNORTS|North/South|Simon Crowther|Sales Associate|South House 300 Queensbri" +
				"dge|London|UK\r\nOCEAN|Océano Atlántico Ltda.|Yvonne Moncada|Sales Agent|Ing. Gust" +
				"avo Moncada 8585 Piso 20-A|Buenos Aires|Argentina\r\nOLDWO|Old World Delicatessen|" +
				"Rene Phillips|Sales Representative|2743 Bering St.|Anchorage|USA\r\nOTTIK|Ottilies" +
				" Käseladen|Henriette Pfalzheim|Owner|Mehrheimerstr. 369|Köln|Germany\r\nPARIS|Pari" +
				"s spécialités|Marie Bertrand|Owner|265, boulevard Charonne|Paris|France\r\nPERIC|P" +
				"ericles Comidas clásicas|Guillermo Fernández|Sales Representative|Calle Dr. Jorg" +
				"e Cash 321|México D.F.|Mexico\r\nPICCO|Piccolo und mehr|Georg Pipps|Sales Manager|" +
				"Geislweg 14|Salzburg|Austria\r\nPRINI|Princesa Isabel Vinhos|Isabel de Castro|Sale" +
				"s Representative|Estrada da saúde n. 58|Lisboa|Portugal\r\nQUEDE|Que Delícia|Berna" +
				"rdo Batista|Accounting Manager|Rua da Panificadora, 12|Rio de Janeiro|Brazil\r\nQU" +
				"EEN|Queen Cozinha|Lúcia Carvalho|Marketing Assistant|Alameda dos Canàrios, 891|S" +
				"ao Paulo|Brazil\r\nQUICK|QUICK-Stop|Horst Kloss|Accounting Manager|Taucherstraße 1" +
				"0|Cunewalde|Germany\r\nRANCH|Rancho grande|Sergio Gutiérrez|Sales Representative|A" +
				"v. del Libertador 900|Buenos Aires|Argentina\r\nRATTC|Rattlesnake Canyon Grocery|P" +
				"aula Wilson|Assistant Sales Representative|2817 Milton Dr.|Albuquerque|USA\r\nREGG" +
				"C|Reggiani Caseifici|Maurizio Moroni|Sales Associate|Strada Provinciale 124|Regg" +
				"io Emilia|Italy\r\nRICAR|Ricardo Adocicados|Janete Limeira|Assistant Sales Agent|A" +
				"v. Copacabana, 267|Rio de Janeiro|Brazil\r\nRICSU|Richter Supermarkt|Michael Holz|" +
				"Sales Manager|Grenzacherweg 237|Genève|Switzerland\r\nROMEY|Romero y tomillo|Aleja" +
				"ndra Camino|Accounting Manager|Gran Vía, 1|Madrid|Spain\r\nSANTG|Santé Gourmet|Jon" +
				"as Bergulfsen|Owner|Erling Skakkes gate 78|Stavern|Norway\r\nSAVEA|Save-a-lot Mark" +
				"ets|Jose Pavarotti|Sales Representative|187 Suffolk Ln.|Boise|USA\r\nSEVES|Seven S" +
				"eas Imports|Hari Kumar|Sales Manager|90 Wadhurst Rd.|London|UK\r\nSIMOB|Simons bis" +
				"tro|Jytte Petersen|Owner|Vinbæltet 34|Kobenhavn|Denmark\r\nSPECD|Spécialités du mo" +
				"nde|Dominique Perrier|Marketing Manager|25, rue Lauriston|Paris|France\r\nSPLIR|Sp" +
				"lit Rail Beer & Ale|Art Braunschweiger|Sales Manager|P.O. Box 555|Lander|USA\r\nSU" +
				"PRD|Suprêmes délices|Pascale Cartrain|Accounting Manager|Boulevard Tirou, 255|Ch" +
				"arleroi|Belgium\r\nTHEBI|The Big Cheese|Liz Nixon|Marketing Manager|89 Jefferson W" +
				"ay Suite 2|Portland|USA\r\nTHECR|The Cracker Box|Liu Wong|Marketing Assistant|55 G" +
				"rizzly Peak Rd.|Butte|USA\r\nTOMSP|Toms Spezialitäten|Karin Josephs|Marketing Mana" +
				"ger|Luisenstr. 48|Münster|Germany\r\nTORTU|Tortuga Restaurante|Miguel Angel Paolin" +
				"o|Owner|Avda. Azteca 123|México D.F.|Mexico\r\nTRADH|Tradição Hipermercados|Anabel" +
				"a Domingues|Sales Representative|Av. Inês de Castro, 414|Sao Paulo|Brazil\r\nTRAIH" +
				"|Trail\'s Head Gourmet Provisioners|Helvetius Nagy|Sales Associate|722 DaVinci Bl" +
				"vd.|Kirkland|USA\r\nVAFFE|Vaffeljernet|Palle Ibsen|Sales Manager|Smagsloget 45|Årh" +
				"us|Denmark\r\nVICTE|Victuailles en stock|Mary Saveley|Sales Agent|2, rue du Commer" +
				"ce|Lyon|France\r\nVINET|Vins et alcools Chevalier|Paul Henriot|Accounting Manager|" +
				"59 rue de l\'Abbaye|Reims|France\r\nWANDK|Die Wandernde Kuh|Rita Müller|Sales Repre" +
				"sentative|Adenauerallee 900|Stuttgart|Germany\r\nWARTH|Wartian Herkku|Pirkko Koski" +
				"talo|Accounting Manager|Torikatu 38|Oulu|Finland\r\nWELLI|Wellington Importadora|P" +
				"aula Parente|Sales Manager|Rua do Mercado, 12|Resende|Brazil\r\nWHITC|White Clover" +
				" Markets|Karl Jablonski|Owner|305 - 14th Ave. S. Suite 3B|Seattle|USA\r\nWILMK|Wil" +
				"man Kala|Matti Karttunen|Owner/Marketing Assistant|Keskuskatu 45|Helsinki|Finlan" +
				"d\r\nWOLZA|Wolski  Zajazd|Zbyszek Piestrzeniewicz|Owner|ul. Filtrowa 68|Warszawa|P" +
				"oland";
			this.txtData.WordWrap = false;
			// 
			// grid1
			// 
			this.grid1.CommandsVisibleIfAvailable = true;
			this.grid1.HelpVisible = false;
			this.grid1.LargeButtons = false;
			this.grid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.grid1.Location = new System.Drawing.Point(432, 136);
			this.grid1.Name = "grid1";
			this.grid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.grid1.Size = new System.Drawing.Size(304, 160);
			this.grid1.TabIndex = 2;
			this.grid1.Text = "propertyGrid1";
			this.grid1.ToolbarVisible = false;
			this.grid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.grid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.White;
			this.cmdRun.Location = new System.Drawing.Point(336, 8);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(152, 32);
			this.cmdRun.TabIndex = 0;
			this.cmdRun.Text = "RUN >>";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(8, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Code of the Mapping Class";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(344, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Output Array";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(8, 304);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(264, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Input Data to the FileHelperEngine";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Code to Read the File";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 72);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(728, 40);
			this.textBox1.TabIndex = 11;
			this.textBox1.Text = "FileHelperEngine engine = new FileHelperEngine(typeof (CustomersVerticalBar));\r\n " +
				"...  = engine.ReadFile(\"infile.txt\")";
			this.textBox1.WordWrap = false;
			// 
			// frmRecordConditions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(746, 496);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.txtData);
			this.Controls.Add(this.txtClass);
			this.Controls.Add(this.label3);
			this.Name = "frmRecordConditions";
			this.Text = "FileHelpers - Record Conditions";
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtClass, 0);
			this.Controls.SetChildIndex(this.txtData, 0);
			this.Controls.SetChildIndex(this.grid1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.ResumeLayout(false);

		}

		#endregion

        /// <summary>
        /// Extract all records beginning with F (criteria)
        /// and display them on a grid
        /// </summary>
        /// <remarks>
        /// As an alternative to this
        /// you can also use the multi record engine and
        /// skip records from being selected.
        /// 
        /// There is always more than one way to do it.
        /// </remarks>
		private void cmdRun_Click(object sender, EventArgs e)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (CustomersVerticalBar));
 
			CustomersVerticalBar[] res = (CustomersVerticalBar[]) engine.ReadString(txtData.Text);
			grid1.SelectedObject = res;
	
		}
		
        /// <summary>
        /// Create a customer record with a
        /// selection criteria of begins with 'F'
        /// </summary>
		[DelimitedRecord("|")]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		[ConditionalRecord(RecordCondition.IncludeIfBegins, "F")]
		public class CustomersVerticalBar
		{
			private string mCustomerID;
			private string mCompanyName;
			private string mContactName;
			private string mContactTitle;
			private string mAddress;
			private string mCity;
			private string mCountry;

			public string CustomerID
			{
				get { return mCustomerID; }
				set { mCustomerID = value; }
			}

			public string CompanyName
			{
				get { return mCompanyName; }
				set { mCompanyName = value; }
			}

			public string ContactName
			{
				get { return mContactName; }
				set { mContactName = value; }
			}

			public string ContactTitle
			{
				get { return mContactTitle; }
				set { mContactTitle = value; }
			}

			public string Address
			{
				get { return mAddress; }
				set { mAddress = value; }
			}

			public string City
			{
				get { return mCity; }
				set { mCity = value; }
			}

			public string Country
			{
				get { return mCountry; }
				set { mCountry = value; }
			}


			//-> To display in the PropertyGrid.
			public override string ToString()
			{
				return CustomerID + " - " + CompanyName + ", " + ContactName;
			}
		}
		}
	}
