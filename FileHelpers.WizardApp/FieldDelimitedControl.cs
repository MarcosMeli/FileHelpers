using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FileHelpers.RunTime;

namespace FileHelpers.WizardApp
{
    public partial class FieldDelimitedControl : FieldBaseControl
    {
        public FieldDelimitedControl()
        {
            InitializeComponent();
        }

        private void LabelControlsClick(object sender, EventArgs e)
        {
            this.Focus();
        }

        private  DelimitedFieldBuilder CastedFieldInfo
        {
            get
            {
                return (DelimitedFieldBuilder)this.FieldInfo;
            }
        }

        private void txtQuoted_TextChanged(object sender, EventArgs e)
        {
            if (txtQuoted.Text == string.Empty)
                CastedFieldInfo.FieldQuoted = false;
            else
            {
                CastedFieldInfo.FieldQuoted = true;
                CastedFieldInfo.QuoteChar = txtQuoted.Text[0];
            }

            OnInfoChanged();
        }

        public override void FieldInfoReload()
        {
            base.FieldInfoReload();
            if (CastedFieldInfo.FieldQuoted)
            {
                if (CastedFieldInfo.QuoteChar.ToString().Length == 0)
                    // by default use " as quote char 
                    txtQuoted.Text = "\"";
                else
                    txtQuoted.Text = CastedFieldInfo.QuoteChar.ToString();
             }
        }

    }
}
