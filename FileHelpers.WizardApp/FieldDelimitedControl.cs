using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

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

        private  DesignFieldInfoDelimited CastedFieldInfo
        {
            get
            {
                return (DesignFieldInfoDelimited) this.FieldInfo;
            }
        }

        private void txtQuoted_TextChanged(object sender, EventArgs e)
        {

            CastedFieldInfo.QuotedChar = txtQuoted.Text;
            OnInfoChanged();
        }

        public override void FieldInfoReload()
        {
            base.FieldInfoReload();

            txtQuoted.Text = CastedFieldInfo.QuotedChar;
        }

    }
}
