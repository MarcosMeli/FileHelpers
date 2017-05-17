namespace FileHelpers.WizardApp
{
    /// <summary>
    /// Filename and contents of the file as a template
    /// </summary>
    /// <remarks>
    /// Variables to replace are in ${} currently only classname.
    /// </remarks>
    public class TemplateInfo
    {
        /// <summary>
        /// Filename minus the .tpl extension
        /// </summary>
        public string TemplateName;

        /// <summary>
        /// Contents of the file before substitution
        /// </summary>
        public string TemplateBody;

        /// <summary>
        /// Allows you to set your own template information
        /// </summary>
        public TemplateInfo() {}

        /// <summary>
        /// Set up template information from the file specified
        /// </summary>
        /// <param name="filename">Filename to read and store name of</param>
        public TemplateInfo(string name, string template)
        {
            this.TemplateName = name;
            TemplateBody = template;
        }

        /// <summary>
        /// Returns template name
        /// </summary>
        /// <remarks>
        /// Great for combo boxes.
        /// </remarks>
        /// <returns>Template name</returns>
        public override string ToString()
        {
            return TemplateName;
        }
    }
}