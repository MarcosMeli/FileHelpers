using System;
using ExamplesFx.ColorCode;
using FileHelpers.Dynamic;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace FileHelpers.WizardApp
{
    /// <summary>
    /// Load some data and test against the built class
    /// </summary>
    public partial class frmDataPreview : Form
    {

        public frmDataPreview(string data, NetLanguage language)
        {
            InitializeComponent();
            ShowCode(data, language);
            //sdClassOut.Text = data;
            cboClassLanguage.Items.Clear();
            cboClassLanguage.Items.Add(NetLanguage.CSharp);
            cboClassLanguage.Items.Add(NetLanguage.VbNet);

            cboClassLanguage.SelectedItem = language;
        }

        private string mLastCode;

        private void ShowCode(string code, NetLanguage language)
        {
            mLastCode = code;
            browserCode.DocumentText = "";
            var colorizer = new CodeColorizer();
            switch (language) {
                case NetLanguage.CSharp:
                    browserCode.DocumentText = GetDefaultCss() + colorizer.Colorize(code, Languages.CSharp);
                    break;
                case NetLanguage.VbNet:
                    browserCode.DocumentText = GetDefaultCss() + colorizer.Colorize(code, Languages.VbDotNet);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("language");
            }
        }

        private string GetDefaultCss()
        {
            return
                @"
<style type=""text/css"">
pre {
/*background-color: #EEF3F9;
border: 1px dashed grey;*/
font-family: Consolas,""Courier New"",Courier,Monospace !important;
font-size: 12px !important;
margin-top: 0;
padding: 6px 6px 6px 6px;
/*height: auto;
overflow: auto;
width: 100% !important;*/
}
</style>
";
        }

        public bool AutoRunTest { get; set; }

        public bool HasHeaders { get; set; }

        private void cmdReadFile_Click(object sender, EventArgs e) {}

        private void cmdReadTest_Click(object sender, EventArgs e)
        {
            RunTest();
        }

        private void RunTest()
        {
            try {
                string classStr = mLastCode;

                var selected = cboClassLanguage.SelectedItem is NetLanguage
                    ? (NetLanguage) cboClassLanguage.SelectedItem
                    : NetLanguage.CSharp;


                var type = ClassBuilder.ClassFromString(classStr, selected);
                FileHelperEngine engine = new FileHelperEngine (type);
                engine.ErrorMode = ErrorMode.SaveAndContinue;

                DataTable dt = engine.ReadStringAsDT (txtInput.Text);                

                if (engine.ErrorManager.Errors.Length > 0)
                {
                    dt = new DataTable ();

                    dt.Columns.Add ("LineNumber");
                    dt.Columns.Add ("ExceptionInfo");
                    dt.Columns.Add ("RecordString");
                    dt.Columns.Add("RecordTypeName");
                    foreach (var e in engine.ErrorManager.Errors)
                    {
                        dt.Rows.Add (e.LineNumber, e.ExceptionInfo.Message, e.RecordString, e.RecordTypeName);
                    }
                    dgPreview.DataSource = dt;

                    MessageBox.Show ("Error Parsing the Sample Data", 
                        "Layout errors detected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    dgPreview.DataSource = dt;
                    lblResults.Text = dt.Rows.Count.ToString () + " Rows - " + dt.Columns.Count.ToString () + " Fields";
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error Compiling Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowCode(Clipboard.GetText(TextDataFormat.Text), NetLanguage.CSharp);
        }

        private void cboClassLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboClassLanguage.SelectedIndex) {
                case 0:
                    ShowCode(mLastCode, NetLanguage.CSharp);
                    break;

                case 1:
                    ShowCode(mLastCode, NetLanguage.VbNet);
                    break;

                default:
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dlgOpenTest.FileName = "";
            dlgOpenTest.Title = "Open FileHelpers Class";
            dlgOpenTest.Filter = "Dynamic Record File (*.fhw;*.xml)|*.fhw;*.xml|All Files|*.*";


            if (dlgOpenTest.ShowDialog() != DialogResult.OK)
                return;

            RegConfig.SetStringValue("WizardOpenTest", Path.GetDirectoryName(dlgOpenTest.FileName));


            ClassBuilder sb = ClassBuilder.LoadFromXml(dlgOpenTest.FileName);

            ShowCode(sb.GetClassSourceCode(NetLanguage.CSharp), NetLanguage.CSharp);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            dlgOpenTest.FileName = "";
            dlgOpenTest.Title = "Open Class Source Code";
            dlgOpenTest.Filter = "Class source code files (*.cs;*.vb;*.txt)|*.txt;*.cs;*.vb|All Files|*.*";

            if (dlgOpenTest.ShowDialog() != DialogResult.OK)
                return;

            RegConfig.SetStringValue("WizardOpenTest", Path.GetDirectoryName(dlgOpenTest.FileName));

            ShowCode(File.ReadAllText(dlgOpenTest.FileName), NetLanguage.CSharp);
        }

        private void frmDataPreview_Load(object sender, EventArgs e)
        {
            if (cboClassLanguage.SelectedIndex < 0)
                cboClassLanguage.SelectedIndex = 0;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dlgOpenTest.FileName = "";

            try {
                if (RegConfig.HasValue("WizardOpenTestData"))
                    dlgOpenTest.InitialDirectory = RegConfig.GetStringValue("WizardOpenTestData", "");
            }
            catch {}

            dlgOpenTest.Title = "Open sample data file";
            dlgOpenTest.Filter = "Flat Files (*.txt;*.csv;*.prn;*.dat)|*.txt;*.csv;*.prn;*.dat|All Files|*.*";

            if (dlgOpenTest.ShowDialog() != DialogResult.OK)
                return;

            RegConfig.SetStringValue("WizardOpenTestData", Path.GetDirectoryName(dlgOpenTest.FileName));

            txtInput.Text = File.ReadAllText(dlgOpenTest.FileName);
        }

        private void txtClearData_Click(object sender, EventArgs e)
        {
            txtInput.Text = string.Empty;
        }

        private void txtPasteData_Click(object sender, EventArgs e)
        {
            txtInput.Text = Clipboard.GetText(TextDataFormat.Text);
        }

        private void frmDataPreview_Activated(object sender, EventArgs e)
        {
            if (AutoRunTest) {
                AutoRunTest = false;
                Application.DoEvents();
                RunTest();
                ShowCode (mLastCode, NetLanguage.CSharp);
            }
            
        }
    }
}