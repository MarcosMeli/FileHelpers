using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FileHelpers;
using FileHelpers.Events;
using FileHelpersSamples.Properties;

namespace FileHelpersSamples
{
    /// <summary>
    /// This example show how progress bars work
    /// </summary>
    public class frmProgressSample : frmFather
    {
        private Button cmdRun;
        private Framework.Controls.XpProgressBar prog1;
        private Framework.Controls.XpProgressBar prog4;
        private Framework.Controls.XpProgressBar prog3;
        private Framework.Controls.XpProgressBar prog2;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label label2;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmProgressSample()
        {
            //
            // Required for Windows Form Designer support
            //
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdRun = new System.Windows.Forms.Button();
            this.prog1 = new Framework.Controls.XpProgressBar();
            this.prog3 = new Framework.Controls.XpProgressBar();
            this.prog2 = new Framework.Controls.XpProgressBar();
            this.prog4 = new Framework.Controls.XpProgressBar();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.cmdRun.Location = new System.Drawing.Point(336, 8);
            this.cmdRun.Name = "cmdRun";
            this.cmdRun.Size = new System.Drawing.Size(152, 32);
            this.cmdRun.TabIndex = 0;
            this.cmdRun.Text = "RUN >>";
            this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
            // 
            // prog1
            // 
            this.prog1.Color1 = System.Drawing.Color.Blue;
            this.prog1.Color2 = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (64)));
            this.prog1.ColorText = System.Drawing.Color.WhiteSmoke;
            this.prog1.ColorTextShadow = System.Drawing.Color.DimGray;
            this.prog1.Font = new System.Drawing.Font("Tahoma",
                14.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.prog1.GradientStyle = Framework.Controls.GradientMode.Horizontal;
            this.prog1.Location = new System.Drawing.Point(8, 64);
            this.prog1.Name = "prog1";
            this.prog1.Position = 0;
            this.prog1.PositionMax = 100;
            this.prog1.PositionMin = 0;
            this.prog1.Size = new System.Drawing.Size(654, 32);
            this.prog1.StepDistance = ((System.Byte) (0));
            this.prog1.StepWidth = ((System.Byte) (3));
            this.prog1.Text = "CodeProject XpProgressBar";
            this.prog1.TextShadowAlpha = ((System.Byte) (200));
            this.prog1.WatermarkAlpha = 255;
            this.prog1.WatermarkImage = null;
            // 
            // prog3
            // 
            this.prog3.Color1 = System.Drawing.Color.FromArgb(((System.Byte) (255)),
                ((System.Byte) (224)),
                ((System.Byte) (192)));
            this.prog3.Color2 = System.Drawing.Color.FromArgb(((System.Byte) (192)),
                ((System.Byte) (64)),
                ((System.Byte) (0)));
            this.prog3.ColorText = System.Drawing.Color.FromArgb(((System.Byte) (115)),
                ((System.Byte) (50)),
                ((System.Byte) (0)));
            this.prog3.ColorTextShadow = System.Drawing.Color.DimGray;
            this.prog3.Font = new System.Drawing.Font("Tahoma",
                12F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.prog3.GradientStyle = Framework.Controls.GradientMode.Vertical;
            this.prog3.Location = new System.Drawing.Point(8, 144);
            this.prog3.Name = "prog3";
            this.prog3.Position = 0;
            this.prog3.PositionMax = 100;
            this.prog3.PositionMin = 0;
            this.prog3.Size = new System.Drawing.Size(654, 32);
            this.prog3.StepDistance = ((System.Byte) (0));
            this.prog3.StepWidth = ((System.Byte) (3));
            this.prog3.WatermarkAlpha = 255;
            this.prog3.WatermarkImage = null;
            // 
            // prog2
            // 
            this.prog2.Color1 = System.Drawing.Color.AliceBlue;
            this.prog2.Color2 = System.Drawing.Color.SteelBlue;
            this.prog2.ColorText = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (64)));
            this.prog2.Font = new System.Drawing.Font("Tahoma",
                12F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.prog2.GradientStyle = Framework.Controls.GradientMode.Diagonal;
            this.prog2.Location = new System.Drawing.Point(8, 104);
            this.prog2.Name = "prog2";
            this.prog2.Position = 0;
            this.prog2.PositionMax = 100;
            this.prog2.PositionMin = 0;
            this.prog2.Size = new System.Drawing.Size(654, 32);
            this.prog2.Text = "Full Customizable";
            this.prog2.TextShadow = false;
            this.prog2.WatermarkAlpha = 255;
            this.prog2.WatermarkImage = null;
            // 
            // prog4
            // 
            this.prog4.Color1 = System.Drawing.Color.RoyalBlue;
            this.prog4.Color2 = System.Drawing.Color.AliceBlue;
            this.prog4.ColorText = System.Drawing.Color.Navy;
            this.prog4.ColorTextShadow = System.Drawing.Color.DimGray;
            this.prog4.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.prog4.GradientStyle = Framework.Controls.GradientMode.HorizontalCenter;
            this.prog4.Location = new System.Drawing.Point(8, 184);
            this.prog4.Name = "prog4";
            this.prog4.Position = 0;
            this.prog4.PositionMax = 100;
            this.prog4.PositionMin = 0;
            this.prog4.Size = new System.Drawing.Size(654, 32);
            this.prog4.StepDistance = ((System.Byte) (0));
            this.prog4.StepWidth = ((System.Byte) (3));
            this.prog4.WatermarkAlpha = 255;
            this.prog4.WatermarkImage = null;
            // 
            // txtClass
            // 
            this.txtClass.Font = new System.Drawing.Font("Courier New",
                8.25F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.txtClass.Location = new System.Drawing.Point(8, 240);
            this.txtClass.Multiline = true;
            this.txtClass.Name = "txtClass";
            this.txtClass.ReadOnly = true;
            this.txtClass.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClass.Size = new System.Drawing.Size(656, 224);
            this.txtClass.TabIndex = 12;
            this.txtClass.Text = @"private void Run()
{
	FileHelperEngine engine = new FileHelperEngine(typeof (CustomersVerticalBar));
	engine.SetProgressHandler(new ProgressChangeHandler(ProgressChange));
	engine.WriteFile(""test.txt"", records);
}

private void ProgressChange(ProgressEventArgs e)
{
	prog1.PositionMax = e.ProgressTotal;
	prog1.Position = e.ProgressCurrent;
	prog1.Text = ""Record "" + e.ProgressCurrent.ToString();

	Application.DoEvents();
}";
            this.txtClass.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma",
                9F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sample code of using the ProgressChange";
            // 
            // frmProgressSample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(674, 496);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.prog4);
            this.Controls.Add(this.prog2);
            this.Controls.Add(this.prog3);
            this.Controls.Add(this.prog1);
            this.Controls.Add(this.cmdRun);
            this.Name = "frmProgressSample";
            this.Text = "FileHelpers - Progress Example";
            this.Controls.SetChildIndex(this.cmdRun, 0);
            this.Controls.SetChildIndex(this.prog1, 0);
            this.Controls.SetChildIndex(this.prog3, 0);
            this.Controls.SetChildIndex(this.prog2, 0);
            this.Controls.SetChildIndex(this.prog4, 0);
            this.Controls.SetChildIndex(this.txtClass, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// Run the engine with a progress bar
        /// hooked into it to give the user a visual
        /// cue on how things are progressing.
        /// </summary>
        private void cmdRun_Click(object sender, EventArgs e)
        {
            // Disable the button, don't want it clicked twice
            cmdRun.Enabled = false;

            FileHelperEngine engine = new FileHelperEngine(typeof (CustomersVerticalBar));
            object[] records = engine.ReadString(Resources.Customers);

            Application.DoEvents();

            engine.Progress += ProgressChange;
            engine.WriteString(records);

            // enable the button again we have finished.
            cmdRun.Enabled = true;
        }

        /// <summary>
        /// Set the progress bars data, how it is used is
        /// up to the progress bars themselves.
        /// </summary>
        private void ProgressChange(object sender, ProgressEventArgs e)
        {
            prog1.PositionMax = e.TotalRecords;
            prog2.PositionMax = e.TotalRecords;
            prog3.PositionMax = e.TotalRecords;
            prog4.PositionMax = e.TotalRecords;

            prog1.Position = e.CurrentRecord;
            prog2.Position = e.CurrentRecord;
            prog3.Position = e.CurrentRecord;
            prog4.Position = e.CurrentRecord;

            prog3.Text = "Record " + e.CurrentRecord.ToString();
            prog4.Text = e.CurrentRecord.ToString() + " Of " + e.TotalRecords.ToString();

            Application.DoEvents();
            Thread.Sleep(10);
        }
    }
}