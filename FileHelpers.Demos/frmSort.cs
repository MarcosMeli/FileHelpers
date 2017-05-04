using FileHelpers;

namespace FileHelpersSamples
{
    /// <summary>
    /// Example application that sorts
    /// the input file by various criteria
    /// </summary>
    /// <remarks>
    /// You might consider using Linq to sort the
    /// file after input as an alternative to this.
    /// </remarks>
    public class frmSort : FileHelpersSamples.frmFather
    {
        private System.Windows.Forms.PropertyGrid grid1;
        private System.Windows.Forms.PropertyGrid grid2;
        private System.Windows.Forms.Button cmdRun;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label txtSortedBy;
        private System.ComponentModel.IContainer components = null;

        public frmSort()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grid1 = new System.Windows.Forms.PropertyGrid();
            this.grid2 = new System.Windows.Forms.PropertyGrid();
            this.cmdRun = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtSortedBy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.CommandsVisibleIfAvailable = true;
            this.grid1.HelpVisible = false;
            this.grid1.LargeButtons = false;
            this.grid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grid1.Location = new System.Drawing.Point(8, 67);
            this.grid1.Name = "grid1";
            this.grid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.grid1.Size = new System.Drawing.Size(232, 272);
            this.grid1.TabIndex = 4;
            this.grid1.Text = "propertyGrid1";
            this.grid1.ToolbarVisible = false;
            this.grid1.ViewBackColor = System.Drawing.SystemColors.Window;
            this.grid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // grid2
            // 
            this.grid2.CommandsVisibleIfAvailable = true;
            this.grid2.HelpVisible = false;
            this.grid2.LargeButtons = false;
            this.grid2.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grid2.Location = new System.Drawing.Point(416, 67);
            this.grid2.Name = "grid2";
            this.grid2.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.grid2.Size = new System.Drawing.Size(232, 272);
            this.grid2.TabIndex = 5;
            this.grid2.Text = "propertyGrid1";
            this.grid2.ToolbarVisible = false;
            this.grid2.ViewBackColor = System.Drawing.SystemColors.Window;
            this.grid2.ViewForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // cmdRun
            // 
            this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdRun.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdRun.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdRun.Location = new System.Drawing.Point(248, 224);
            this.cmdRun.Name = "cmdRun";
            this.cmdRun.Size = new System.Drawing.Size(160, 34);
            this.cmdRun.TabIndex = 6;
            this.cmdRun.Text = "Sort By City >";
            this.cmdRun.Click += new System.EventHandler(this.SortByCity_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.button1.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(248, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Sort By ID >";
            this.button1.Click += new System.EventHandler(this.SortByCustomerId_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.button2.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(248, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 34);
            this.button2.TabIndex = 8;
            this.button2.Text = "Sort By Company >";
            this.button2.Click += new System.EventHandler(this.SortByCompanyName_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.button3.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(248, 176);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 34);
            this.button3.TabIndex = 9;
            this.button3.Text = "Sort By Name >";
            this.button3.Click += new System.EventHandler(this.SortByContactName_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.button4.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.button4.ForeColor = System.Drawing.Color.Gainsboro;
            this.button4.Location = new System.Drawing.Point(248, 272);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 34);
            this.button4.TabIndex = 10;
            this.button4.Text = "Sort By Country >";
            this.button4.Click += new System.EventHandler(this.SortByCountry_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New",
                8.25F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.textBox1.Location = new System.Drawing.Point(8, 344);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(640, 72);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "FileHelperEngine engine = new FileHelperEngine(typeof (CustomersVerticalBar));\r\nC" +
                                 "ustomerVerticalBar[] res  = (CustomerVerticalBar[]) engine.ReadFile(\"infile.txt\"" +
                                 ");\r\n\r\nCommonEngine.SortRecordsByField(res, \"FieldName\");";
            this.textBox1.WordWrap = false;
            // 
            // txtSortedBy
            // 
            this.txtSortedBy.BackColor = System.Drawing.Color.Transparent;
            this.txtSortedBy.Font = new System.Drawing.Font("Tahoma",
                9F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.txtSortedBy.ForeColor = System.Drawing.Color.White;
            this.txtSortedBy.Location = new System.Drawing.Point(416, 51);
            this.txtSortedBy.Name = "txtSortedBy";
            this.txtSortedBy.Size = new System.Drawing.Size(216, 16);
            this.txtSortedBy.TabIndex = 13;
            // 
            // frmSort
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(656, 446);
            this.Controls.Add(this.txtSortedBy);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdRun);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.grid1);
            this.MaximizeBox = false;
            this.Name = "frmSort";
            this.Text = "Sorting Example";
            this.Load += new System.EventHandler(this.frmSort_Load);
            this.Controls.SetChildIndex(this.grid1, 0);
            this.Controls.SetChildIndex(this.grid2, 0);
            this.Controls.SetChildIndex(this.cmdRun, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.txtSortedBy, 0);
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// Data we are going to read and sort
        /// </summary>
        private string SampleData = @"BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
BLONP|Blondesddsl père et fils|Frédérique Citeaux|Manager|24, Kléber|Strasbourg|France
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
AROUT|Around the Horn|Thomas Hardy|Sales Representative|120 Hanover Sq.|London|UK
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain";


        /// <summary>
        /// Sort by field (eg mCountry) and display result in a
        /// grid, replaces the previous sort
        /// </summary>
        /// <param name="field">Field to sort on</param>
        private void SortBy(string field)
        {
            // first character of field is always m, remove it.
            txtSortedBy.Text = "Sorted By " + field.Substring(1);

            CustomersVerticalBar[] res2 = (CustomersVerticalBar[]) mRecords.Clone();
            CommonEngine.SortRecordsByField(res2, field);
            grid2.SelectedObject = res2;
        }

        /// <summary>
        /// records parsed by the engine, will be read and sorted
        /// </summary>
        private CustomersVerticalBar[] mRecords;

        /// <summary>
        /// Open file and read into array,  display unsorted
        /// </summary>
        private void frmSort_Load(object sender, System.EventArgs e)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof (CustomersVerticalBar));
            mRecords = (CustomersVerticalBar[]) engine.ReadString(SampleData);

            grid1.SelectedObject = mRecords;
        }

//		mCustomerID;
//		private string mCompanyName;
//		private string mContactName;
//		private string mContactTitle;
//		private string mAddress;
//		private string mCity;
//		private string mCountry;

        private void SortByCustomerId_Click(object sender, System.EventArgs e)
        {
            SortBy("mCustomerID");
        }

        private void SortByCity_Click(object sender, System.EventArgs e)
        {
            SortBy("mCity");
        }

        private void SortByCompanyName_Click(object sender, System.EventArgs e)
        {
            SortBy("mCompanyName");
        }

        private void SortByContactName_Click(object sender, System.EventArgs e)
        {
            SortBy("mContactName");
        }

        private void SortByCountry_Click(object sender, System.EventArgs e)
        {
            SortBy("mCountry");
        }
    }
}