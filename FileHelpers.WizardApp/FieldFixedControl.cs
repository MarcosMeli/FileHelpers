using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FileHelpers.Dynamic;

namespace FileHelpers.WizardApp
{
    public partial class FieldFixedControl : FieldBaseControl
    {
        public FieldFixedControl()
        {
            InitializeComponent();
        }

        private FixedFieldBuilder CastedFieldInfo
        {
            get { return (FixedFieldBuilder) FieldInfo; }
        }

        private void LabelControlsClick(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CastedFieldInfo.FieldLength = (int) txtLength.Value;
            OnInfoChanged();
        }

        public override void FieldInfoReload()
        {
            base.FieldInfoReload();

            txtLength.Value = CastedFieldInfo.FieldLength;
        }
    }
}