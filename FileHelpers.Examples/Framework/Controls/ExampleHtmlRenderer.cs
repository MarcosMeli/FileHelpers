using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace ExamplesFramework
{
    public partial class ExampleHtmlRenderer : UserControl
    {
        public ExampleHtmlRenderer()
        {
            Theme = new DefaultExampleTheme();
            InitializeComponent();
            
            //DoubleBuffered = true;
        }

        public void ShowCustomHtml(string html)
        {
            splitOutHorizontal.Panel2Collapsed = true;
            browserExample.DocumentText = html;
        }

        private ExampleCode mExample;
        public ExampleCode Example
        {
            get { return mExample; }
            set
            {
                if (mExample == value)
                    return;

                mExample = value;
                RenderExample();
            }
        }

        public ExampleThemeBase Theme { get; set; }

        private void RenderExample()
        {
            this.SuspendLayout();
            
            Clear();

            //lblDescription.Text = Example.Description;
            cmdRunDemo.Visible = Example.Runnable;
            browserExample.DocumentText = ExampleToHtml();
            this.ResumeLayout();

            if (Example.AutoRun)
                RunExample();
        }

        private void RunExample()
        {
            if (Example == null)
                return;
            try
            {
                Example.ConsoleChanged += Example_ConsoleChanged;
                Example.ConsoleChanged += Example_InputChanged;
                Example.AddedFile += FileHandler;
                Example.RunExample();
            }
            finally  
            {
                Example.AddedFile -= FileHandler;
                Example.ConsoleChanged -= Example_ConsoleChanged;
                Example.ConsoleChanged -= Example_InputChanged;
            }}

        
        void Example_ConsoleChanged(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            Theme.AddHeaderStyles(sb);

            sb.AppendLine(@"<body style=""margin:0px;background-color:#000;color:#DDD;"">");

//            consoleRes.AppendLine(@"<div class=""FileLeft"">&nbsp;</div>
//<div class=""FileMiddle"">Console</div>
//<div class=""FileRight"">&nbsp;</div><br/>
//<div style=""clear:both""></div>");

sb.AppendLine(@"<div id=""consola""><pre style=""background-color:#000;color:#DDD;border: 0px;"">" + Example.Example.Console.Output + "</pre></div></body>");
            browserOutput.DocumentText = sb.ToString();

            splitOutHorizontal.Panel2Collapsed = false;

            //var console = browserOutput.Document.GetElementById("consola");

            //if (console != null)
            //    console.InnerHtml = "<pre>"+  Example.Example.Console.Output + "</pre>";
        }


        void Example_InputChanged(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            Theme.AddHeaderStyles(sb);

            sb.AppendLine(@"<body style=""margin:0px;background-color:#000;color:#DDD;"">");

            //            consoleRes.AppendLine(@"<div class=""FileLeft"">&nbsp;</div>
            //<div class=""FileMiddle"">Console</div>
            //<div class=""FileRight"">&nbsp;</div><br/>
            //<div style=""clear:both""></div>");

            sb.AppendLine(@"<div id=""consola""><pre style=""background-color:#000;color:#DDD;border: 0px;"">" + Example.Example.InputFile + "</pre></div></body>");
            browserInput.DocumentText = sb.ToString();
            
            splitOutHorizontal.Panel2Collapsed = false;
            splitBottom.Panel1Collapsed = false;

            //var console = browserOutput.Document.GetElementById("consola");

            //if (console != null)
            //    console.InnerHtml = "<pre>"+  Example.Example.Console.Output + "</pre>";
        }
        
        private string ExampleToHtml()
        {
            var res = new StringBuilder();

            Theme.AddHeaderStyles(res);

            Theme.AddExampleTitle(res, Example);

            if (string.IsNullOrEmpty(Example.Example.HtmlBody))
            {

                for (int i = 0; i < Example.Files.Count; i++)
                {
                    var file = Example.Files[i];

                    if (file.Status ==
                        ExampleFile.FileType.OutputFile)
                        continue;

                    Theme.AddFile(res, file);

                    res.AppendLine("<BR/>");
                }
            }
            else
            {
                var html = Example.Example.HtmlBody;
                
                for (int i = 0; i < Example.Files.Count; i++)
                {
                    var file = Example.Files[i];

                    if (file.Status == ExampleFile.FileType.OutputFile)
                        continue;

                    var filecontents = Theme.AddFile(file);
                    html = html.Replace("${" + file.Filename + "}", filecontents);
                    
                }

                res.AppendLine(html);
            }

            Theme.AddExampleFooter(res, Example);

            return res.ToString();
        }

        
        public void Clear()
        {
            cmdRunDemo.Visible = false;
            browserExample.DocumentText = string.Empty;
            browserOutput.DocumentText = string.Empty;
            splitOutHorizontal.Panel2Collapsed = true;
        }

        private void cmdRunDemo_Click(object sender, EventArgs e)
        {
            RunExample();

        }

        private void FileHandler(object sender, ExampleCode.NewFileEventArgs e)
        {
            browserExample.DocumentText = ExampleToHtml();
        }



    }
}
