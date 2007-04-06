using System;
using FileHelpers;
using FileHelpers.RunTime;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fireball.CodeEditor.SyntaxFiles;

namespace FileHelpers.WizardApp
{
    public partial class frmDataPreview : Form
    {
        public frmDataPreview(string data, int index)
        {
            InitializeComponent();
            sdClassOut.Text = data;
            cboClassLeng.SelectedIndex = index;
        }

 
        private void cmdReadFile_Click(object sender, EventArgs e)
        {

        }

        private void cmdReadTest_Click(object sender, EventArgs e)
        {
            try
            {
                string classStr = sdClassOut.Text;
                Type mType = null;

                switch (cboClassLeng.SelectedIndex)
                {
                    case 0:
                        mType = ClassBuilder.ClassFromString(classStr, NetLanguage.CSharp);
                        break;

                    case 1:
                        mType = ClassBuilder.ClassFromString(classStr, NetLanguage.VbNet);
                        break;

                    default:
                        break;
                }

                try
                {
                    FileHelperEngine engine = new FileHelperEngine(mType);
                    dgPreview.DataSource = engine.ReadStringAsDT(txtInput.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Parsing the Sample Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Compiling Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            sdClassOut.Text = Clipboard.GetText(TextDataFormat.Text);
        }

        private void cboClassLeng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string output = sdClassOut.Text;
            switch (cboClassLeng.SelectedIndex)
            {
                case 0:
                    CodeEditorSyntaxLoader.SetSyntax(txtClass, SyntaxLanguage.VBNET);
                    CodeEditorSyntaxLoader.SetSyntax(txtClass, SyntaxLanguage.CSharp);
                    break;

                case 1:
                    CodeEditorSyntaxLoader.SetSyntax(txtClass, SyntaxLanguage.CSharp);
                    CodeEditorSyntaxLoader.SetSyntax(txtClass, SyntaxLanguage.VBNET);
                    break;

                default:
                    break;
            }

            sdClassOut.Text = output;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dlgOpenTest.FileName = "";

            dlgOpenTest.Title = "Open FileHelpers xml class file";
            dlgOpenTest.Filter = "RunTime Record xml class file|*.fhw;*.xml|All Files|*.*";


            if (dlgOpenTest.ShowDialog() != DialogResult.OK)
                return;

            
            ClassBuilder sb = ClassBuilder.LoadFromXml(dlgOpenTest.FileName);

            sdClassOut.Text = sb.GetClassSourceCode(NetLanguage.CSharp);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            dlgOpenTest.FileName = "";

            dlgOpenTest.Title = "Open Class Source Code";
            dlgOpenTest.Filter = "Class source code files|*.txt;*.cs;*.vb|All Files|*.*";


            if (dlgOpenTest.ShowDialog() != DialogResult.OK)
                return;

            sdClassOut.Text = File.ReadAllText(dlgOpenTest.FileName);
        }

        private void frmDataPreview_Load(object sender, EventArgs e)
        {
            cboClassLeng.SelectedIndex = 0;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dlgOpenTest.FileName = "";
            dlgOpenTest.Title = "Open sample data file";

            dlgOpenTest.Filter = "Flat Files|*.txt;*.csv;*.prn;*.dat|All Files|*.*";

            if (dlgOpenTest.ShowDialog() != DialogResult.OK)
                return;

            txtInput.Text = File.ReadAllText(dlgOpenTest.FileName);

        }
    }
}