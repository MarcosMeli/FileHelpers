using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace FileHelpers.WizardApp
{
    /// <summary>
    /// Select a language on screen
    /// </summary>
    public class LanguageSelector : ComboBox
    {
        /// <summary>
        /// Notification of change of language
        /// </summary>
        public class EventLanguage : EventArgs
        {
            /// <summary>
            /// Language we are changing to
            /// </summary>
            public NetLanguage Language { get; private set; }

            /// <summary>
            /// Create a language change event
            /// </summary>
            /// <param name="pLanguage"></param>
            internal EventLanguage(NetLanguage pLanguage)
            {
                Language = pLanguage;
            }
        }

        /// <summary>
        /// Track language change events within the application
        /// </summary>
        public static event EventHandler<EventLanguage> LanguageChange;

        /// <summary>
        /// The language of the entire application
        /// </summary>
        public static NetLanguage ApplicationLanguage = NetLanguage.CSharp;

        /// <summary>
        /// Language type and it's text description
        /// </summary>
        private class LanguageType
        {
            /// <summary>
            /// Display text in GUI
            /// </summary>
            public string Text;

            /// <summary>
            /// Language that this represents
            /// </summary>
            public NetLanguage Language;

            /// <summary>
            /// Display value,  Text
            /// </summary>
            /// <returns>Text description of language</returns>
            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// Language selected by the user in the component
        /// </summary>
        [Browsable(false)]
        public NetLanguage Language;

        /// <summary>
        /// Select the working language changed.
        /// </summary>
        /// <remarks>
        /// Constructs a list of objects that display text value
        /// and you can easily get the language code out of the
        /// selected item value.
        /// </remarks>
        public LanguageSelector()
            : base() {}

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            LanguageType selected = null;
            LanguageType temp = new LanguageType() {
                Language = NetLanguage.CSharp,
                Text = "C#",
            };
            if (ApplicationLanguage == NetLanguage.CSharp)
                selected = temp;

            Items.Add(temp);
            temp = new LanguageType() {
                Language = NetLanguage.VbNet,
                Text = "VB.NET",
            };
            if (ApplicationLanguage == NetLanguage.VbNet)
                selected = temp;
            Items.Add(temp);
            this.SelectedItem = selected;

            LanguageChange += new EventHandler<EventLanguage>(LanguageSelector_LanguageChange);
        }

        private void LanguageSelector_LanguageChange(object sender, LanguageSelector.EventLanguage e)
        {
            foreach (var item in this.Items) {
                this.Language = e.Language;
                LanguageType lang = item as LanguageType;
                if (lang.Language == e.Language) {
                    this.SelectedItem = lang;
                    return;
                }
                this.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Change the selected language
        /// </summary>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            var value = (LanguageType) this.SelectedItem;
            if (Language != value.Language) {
                Language = value.Language;
                if (LanguageChange != null)
                    LanguageChange(this, new EventLanguage(this.Language));
            }
        }
    }
}