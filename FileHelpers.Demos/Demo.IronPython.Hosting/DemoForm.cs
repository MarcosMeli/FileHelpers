using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using IronPython.Hosting;
using IronPython.Runtime.Calls;

using FileHelpers;
using FileHelpers.RunTime;

namespace Demo.IronPython.Hosting
{
    public partial class DemoForm : Form
    {
        string pythonFile, dataFile;
        Type CopyrightRecordType, DetailRecordType;
        TextBoxOutputStream myConsole;
        PythonEngine pe;
        MultiRecordEngine engine;
        Function2 mainFunc;

        public DemoForm()
        {
            InitializeComponent();
            myConsole = new TextBoxOutputStream(textBox1);
            // IronPython's "print" builtin calls Flush() after every write!
            myConsole.AllowAllFlushes = false;
        }

        private Type mySelector(MultiRecordEngine unused, string record)
        {
            if (record[0] == 'C')
                return CopyrightRecordType;
            else if (record[0] == 'D')
                return DetailRecordType;
            else
                throw new Exception();
        }

        private void ToggleProcessButton()
        {
            ProcessButton.Enabled = (pythonFile != null && dataFile != null);
        }

        private void LoadModuleButton_Click(object sender, EventArgs e)
        {
            StringBuilder errorMessage = new StringBuilder();

            openFileDialog1.Filter = "test.py|test.py";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pythonFile = openFileDialog1.FileName;
                pe = new PythonEngine();
                pe.SetStandardOutput(myConsole);
                pe.SetStandardError(myConsole);

                try
                {
                    pe.ExecuteFile(pythonFile);
                    mainFunc = ((Function2)pe.Globals["main"]);
                }
                catch (InvalidCastException)
                {
                    errorMessage.Append("Symbol 'main' found in Python script, but of wrong type!\n");
                }
                catch (KeyNotFoundException)
                {
                    errorMessage.Append("Symbol 'main' not found in Python script!\n");
                }
                catch (Exception ex)
                {
                    errorMessage.Append("Execution failed!\nException: ");
                    errorMessage.Append(ex.Message);
                }

                if (errorMessage.Length > 0)
                {
                    errorMessage.Append("\nScript must contain a 'main' function of the following signature:\n");
                    errorMessage.Append("     def main(engine, record)");
                    MessageBox.Show(errorMessage.ToString(),
                        string.Format("Error Loading {0}", pythonFile),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    pythonFile = null;
                }
            }
            ToggleProcessButton();
        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CINLIST|CINLIST";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dataFile = openFileDialog1.FileName;

                try
                {
                    CopyrightRecordType = 
                        FixedLengthClassBuilder.ClassFromXmlFile("CinListCopyrightRecord.xml");
                    DetailRecordType = 
                        FixedLengthClassBuilder.ClassFromXmlFile("CinListDetailRecord.xml");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error loading RunTime Record Classes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    dataFile = null;
                    return;
                }

                try
                {
                    engine = new MultiRecordEngine(
                                new RecordTypeSelector(mySelector),
                                CopyrightRecordType,
                                DetailRecordType);

                    engine.BeginReadFile(dataFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error loading data file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    dataFile = null;
                    return;
                }
            }
            ToggleProcessButton();
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            while (engine.ReadNext() != null)
            {
                mainFunc.Call(engine, engine.LastRecord);
            }
            myConsole.realFlush();
        }
    }
}