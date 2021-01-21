using ExamplesFx.Properties;
using ExamplesFx.Controls;
using FileHelpers;

namespace ExamplesFx
{
    partial class frmExamples
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExamples));
            Devoo.WinForms.TextShape textShape1 = new Devoo.WinForms.TextShape();
            this.reflectionHeader1 = new Devoo.WinForms.ReflectionHeader();
            this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
            this.examplesContainer = new ExamplesContainer();
            this.SuspendLayout();
            // 
            // reflectionHeader1
            // 
            this.reflectionHeader1.BandDown.Color.Color1 = System.Drawing.Color.Black;
            this.reflectionHeader1.BandDown.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.reflectionHeader1.BandDown.Color.Direction = Devoo.WinForms.GradientDirection.Horizontal;
            this.reflectionHeader1.BandDown.Height = 18;
            this.reflectionHeader1.BandUp.Color.Color1 = System.Drawing.Color.Black;
            this.reflectionHeader1.BandUp.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.reflectionHeader1.BandUp.Color.Direction = Devoo.WinForms.GradientDirection.Horizontal;
            this.reflectionHeader1.BandUp.Height = 0;
            this.reflectionHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionHeader1.GradientBack.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(1)))), ((int)(((byte)(74)))));
            this.reflectionHeader1.GradientBack.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(0)))), ((int)(((byte)(107)))));
            this.reflectionHeader1.GradientBack.Direction = Devoo.WinForms.GradientDirection.Vertical;
            this.reflectionHeader1.Header.Color.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reflectionHeader1.Header.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.reflectionHeader1.Header.Color.Direction = Devoo.WinForms.GradientDirection.Vertical;
            this.reflectionHeader1.Header.Font = new System.Drawing.Font("Trebuchet MS", 27.75F, System.Drawing.FontStyle.Bold);
            this.reflectionHeader1.Header.Position = new System.Drawing.Point(80, 17);
            this.reflectionHeader1.Header.ReflectionLevel = ((byte)(100));
            this.reflectionHeader1.Header.ReflectionOpacity = ((byte)(200));
            this.reflectionHeader1.Header.Text = "FileHelpers Examples";
            this.reflectionHeader1.Location = new System.Drawing.Point(0, 0);
            this.reflectionHeader1.Name = "reflectionHeader1";
            this.reflectionHeader1.Size = new System.Drawing.Size(934, 85);
            this.reflectionHeader1.Text = "FileHelpers Examples";
            textShape1.Color.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            textShape1.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            textShape1.Color.Direction = Devoo.WinForms.GradientDirection.Vertical;
            textShape1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textShape1.Position = new System.Drawing.Point(615, 67);
            textShape1.ReflectionLevel = ((byte)(100));
            textShape1.ReflectionOpacity = ((byte)(0));
            textShape1.Text = "All FileHelpers Demos in one place";
            this.reflectionHeader1.Texts.AddRange(new Devoo.WinForms.TextShape[] {
            textShape1});
            // 
            // imgTreeView
            // 
            this.imgTreeView.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // examplesContainer
            // 
            this.examplesContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.examplesContainer.Location = new System.Drawing.Point(0, 85);
            this.examplesContainer.Name = "examplesContainer";
            this.examplesContainer.Size = new System.Drawing.Size(934, 577);
            this.examplesContainer.TabIndex = 4;
            // 
            // frmExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 662);
            this.Controls.Add(this.examplesContainer);
            this.Controls.Add(this.reflectionHeader1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmExamples";
            this.Text = "FileHelpers Examples";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private Devoo.WinForms.ReflectionHeader reflectionHeader1;
        private System.Windows.Forms.ImageList imgTreeView;
        private ExamplesFx.Controls.ExamplesContainer examplesContainer;
    }
}