using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Devoo.WinForms;

namespace ExamplesFx.Controls
{
    public class ExampleContainerWithHeader
        :ExamplesContainer
    {
        private ReflectionHeader reflectionHeader1;

        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof(ExampleContainerWithHeader));
            var textShape1 = new TextShape();
            this.reflectionHeader1 = new ReflectionHeader();
            this.SuspendLayout();
            // 
            // reflectionHeader1
            // 
            this.reflectionHeader1.BandDown.Color.Color1 = Color.Black;
            this.reflectionHeader1.BandDown.Color.Color2 = Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.reflectionHeader1.BandDown.Color.Direction = GradientDirection.Horizontal;
            this.reflectionHeader1.BandDown.Height = 18;
            this.reflectionHeader1.BandUp.Color.Color1 = Color.Black;
            this.reflectionHeader1.BandUp.Color.Color2 = Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.reflectionHeader1.BandUp.Color.Direction = GradientDirection.Horizontal;
            this.reflectionHeader1.BandUp.Height = 0;
            this.reflectionHeader1.Dock = DockStyle.Top;
            this.reflectionHeader1.GradientBack.Color1 = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(1)))), ((int)(((byte)(74)))));
            this.reflectionHeader1.GradientBack.Color2 = Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(0)))), ((int)(((byte)(107)))));
            this.reflectionHeader1.GradientBack.Direction = GradientDirection.Vertical;
            this.reflectionHeader1.Header.Color.Color1 = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reflectionHeader1.Header.Color.Color2 = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.reflectionHeader1.Header.Color.Direction = GradientDirection.Vertical;
            this.reflectionHeader1.Header.Font = new Font("Trebuchet MS", 27.75F, FontStyle.Bold);
            this.reflectionHeader1.Header.Position = new Point(80, 17);
            this.reflectionHeader1.Header.ReflectionLevel = ((byte)(100));
            this.reflectionHeader1.Header.ReflectionOpacity = ((byte)(200));
            this.reflectionHeader1.Header.Text = "FileHelpers Examples";
            this.reflectionHeader1.Images.AddRange(new ImageShape[] {
            new ImageShape(((Bitmap)(resources.GetObject("reflectionHeader1.Images"))), true, ((byte)(255)), new Point(3, 1), ((byte)(0)), 0)});
            this.reflectionHeader1.Location = new Point(0, 0);
            this.reflectionHeader1.Name = "reflectionHeader1";
            this.reflectionHeader1.Size = new Size(753, 85);
            this.reflectionHeader1.Text = "FileHelpers Examples";
            textShape1.Color.Color1 = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            textShape1.Color.Color2 = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            textShape1.Color.Direction = GradientDirection.Vertical;
            textShape1.Font = new Font("Trebuchet MS", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            textShape1.Position = new Point(615, 67);
            textShape1.ReflectionLevel = ((byte)(100));
            textShape1.ReflectionOpacity = ((byte)(0));
            textShape1.Text = "All FileHelpers Demos in one place";
            this.reflectionHeader1.Texts.AddRange(new TextShape[] {
            textShape1});
            // 
            // ExampleContainerWithHeader
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.Name = "ExampleContainerWithHeader";
            this.ResumeLayout(false);

        }
    }
}
