using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FileHelpers.RunTime;

namespace FileHelpers.WizardApp
{
    public partial class FieldBaseControl : UserControl
    {

        bool mInit = true;

        public FieldBaseControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            cboTrim.SelectedIndex = 0;
            mInit = false;
        }

        bool isFocused = false;

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            isFocused = true;
            this.Invalidate();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            isFocused = false;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            LinearGradientBrush lb;
            
            if (isFocused)
//                lb = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(255, 224, 192), Color.White, LinearGradientMode.Horizontal);
                lb = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(205, 195, 180), Color.FromArgb(248,248,248), LinearGradientMode.Horizontal);
            else
                lb = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(235, 225, 215), Color.FromArgb(250,250,250), LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(lb, this.ClientRectangle);

            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, this.Width - 1, this.Height - 1);
            lb.Dispose();


        }


        private FieldBuilder mFieldInfo;

        public virtual void FieldInfoReload()
        {
            if (mFieldInfo != null)
            {
                txtName.Text = mFieldInfo.FieldName;
                txtName.SelectAll();
                txtType.Text = mFieldInfo.FieldType;
                cboTrim.SelectedItem = mFieldInfo.TrimMode;
            }

        }

        internal FieldBuilder FieldInfo
        {
            get { return mFieldInfo; }
            set 
            {
                 mFieldInfo = value;
                 FieldInfoReload();
            }
        }


        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mFieldInfo.FieldName = txtName.Text;
            OnInfoChanged();
        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mFieldInfo.FieldType = txtType.Text;
            OnInfoChanged();
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            mFieldInfo.FieldType = txtType.Text;
            OnInfoChanged();
        }

        private void LabelControlsClick(object sender, EventArgs e)
        {
            this.Focus();
        }

        public event EventHandler InfoChanged;
        public event EventHandler OrderChanged;

        protected void OnInfoChanged()
        {
            int index = this.mFieldInfo.FieldIndex;
            lblFieldPosition.Text = (index + 1).ToString();

            if (InfoChanged != null)
                InfoChanged(this, EventArgs.Empty);
        }

        protected void OnOrderChanged()
        {

            if (OrderChanged != null)
                OrderChanged(this, EventArgs.Empty);
        }

        private void lblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Parent.Controls.Remove(this);
            OnOrderChanged();
        }

        private void lblUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int index = this.Parent.Controls.GetChildIndex(this);
            if (index > 0)
            {
                this.Parent.Controls.SetChildIndex(this, index - 1);
                OnOrderChanged();
            }
        }

        private void lblDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int index = this.Parent.Controls.GetChildIndex(this);
            if (index < this.Parent.Controls.Count - 1)
            {
                this.Parent.Controls.SetChildIndex(this, index + 1);
                OnOrderChanged();
            }
        }

        private void cboTrim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mInit == false)
            {
                mFieldInfo.TrimMode = (TrimMode)Enum.Parse(typeof(TrimMode), cboTrim.Text);
                OnInfoChanged();
            }
        }

        private void chkOptional_CheckedChanged(object sender, EventArgs e)
        {
            if (mInit == false)
            {
                mFieldInfo.FieldOptional = chkOptional.Checked;
                OnInfoChanged();
            }
        }




        internal void RePaintData()
        {
            int index = this.mFieldInfo.FieldIndex;
            lblFieldPosition.Text = (index + 1).ToString();
        }
    }
}
