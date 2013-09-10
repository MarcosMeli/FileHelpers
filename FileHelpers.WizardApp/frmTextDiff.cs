using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers.WizardApp
{
    public partial class frmTextDiff : Form
    {
        public frmTextDiff()
        {
            InitializeComponent();
        }


        private void txtTextA_TextChanged(object sender, EventArgs e)
        {
            txtResult.Clear();
        }

        private void txtTextB_TextChanged(object sender, EventArgs e)
        {
            txtResult.Clear();
        }

        private void cmdCopyResult_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtResult.Text, TextDataFormat.UnicodeText);
        }

        private void cmdPasteA_Click(object sender, EventArgs e)
        {
            txtTextA.Text = Clipboard.GetText();
        }

        private void cmdPasteB_Click(object sender, EventArgs e)
        {
            txtTextB.Text = Clipboard.GetText();
        }


        private void cmdNor_Click(object sender, EventArgs e)
        {
            var dictA = new HashSet<string>();
            var dictB = new HashSet<string>();

            var textA = txtTextA.Text.SplitInLines();
            foreach (var line in textA)
            {
                if (!dictA.Contains(line))
                    dictA.Add(line);
            }
            var res = new StringBuilder();

            var textB = txtTextB.Text.SplitInLines();
            foreach (var line in textB)
            {
                if (!dictB.Contains(line))
                    dictB.Add(line);
            }

            foreach (var line in textA)
            {
                if (!dictB.Contains(line))
                    res.AppendLine(line);
            }

            txtResult.Text = res.ToString();
        }

        private void cmdIntersect_Click(object sender, EventArgs e)
        {
            var dictA = new HashSet<string>();

            var textA = txtTextA.Text.SplitInLines();
            foreach (var line in textA)
            {
                if (!dictA.Contains(line))
                    dictA.Add(line);
            }
            var res = new StringBuilder();

            var textB = txtTextB.Text.SplitInLines();
           
            foreach (var line in textB)
            {
                if (dictA.Contains(line))
                    res.AppendLine(line);
            }

            txtResult.Text = res.ToString();
        }

        private void cmdUnion_Click(object sender, EventArgs e)
        {
            var dictA = new HashSet<string>();
            var res = new StringBuilder();

            var textA = txtTextA.Text.SplitInLines();
            foreach (var line in textA)
            {
                if (!dictA.Contains(line))
                {
                    res.AppendLine(line);
                    dictA.Add(line);
                }
            }

            var textB = txtTextB.Text.SplitInLines();

            foreach (var line in textB)
            {
                if (!dictA.Contains(line))
                    res.AppendLine(line);
            }

            txtResult.Text = res.ToString();
        }
    }
}



