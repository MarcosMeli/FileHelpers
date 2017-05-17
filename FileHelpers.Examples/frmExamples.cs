﻿using System;
using System.Windows.Forms;
using Examples;


namespace ExamplesFx
{
    public partial class frmExamples : Form
    {
        public frmExamples()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            examplesContainer.LoadExamples(ExamplesFactory.GetExamples());
        }
    }
}