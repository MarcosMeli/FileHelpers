using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.WizardApp
{
    public class TemplateInfo
    {
        public string TemplateName;
        public string TemplateBody;

        public override string ToString()
        {
            return TemplateName;
        }

    }
}
