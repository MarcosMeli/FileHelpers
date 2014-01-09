// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Drawing;
using ExamplesFx.ColorCode.Common;

namespace ExamplesFx.ColorCode.Styling.StyleSheets
{
    internal class DefaultStyleSheet : IStyleSheet
    {
        public static readonly Color DullRed = Color.FromArgb(163, 21, 21);
        private static readonly StyleDictionary styles;

        static DefaultStyleSheet()
        {
            styles = new StyleDictionary {
                new Style(ScopeName.PlainText) {
                    Foreground = Color.Black,
                    Background = Color.White
                },
                new Style(ScopeName.HtmlServerSideScript) {
                    Background = Color.Yellow
                },
                new Style(ScopeName.HtmlComment) {
                    Foreground = Color.Green
                },
                new Style(ScopeName.HtmlTagDelimiter) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.HtmlElementName) {
                    Foreground = DullRed
                },
                new Style(ScopeName.HtmlAttributeName) {
                    Foreground = Color.Red
                },
                new Style(ScopeName.HtmlAttributeValue) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.HtmlOperator) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.Comment) {
                    Foreground = Color.Green
                },
                new Style(ScopeName.XmlDocTag) {
                    Foreground = Color.Gray
                },
                new Style(ScopeName.XmlDocComment) {
                    Foreground = Color.Green
                },
                new Style(ScopeName.String) {
                    Foreground = DullRed
                },
                new Style(ScopeName.StringCSharpVerbatim) {
                    Foreground = DullRed
                },
                new Style(ScopeName.Keyword) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.PreprocessorKeyword) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.HtmlEntity) {
                    Foreground = Color.Red
                },
                new Style(ScopeName.XmlAttribute) {
                    Foreground = Color.Red
                },
                new Style(ScopeName.XmlAttributeQuotes) {
                    Foreground = Color.Black
                },
                new Style(ScopeName.XmlAttributeValue) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.XmlCDataSection) {
                    Foreground = Color.Gray
                },
                new Style(ScopeName.XmlComment) {
                    Foreground = Color.Green
                },
                new Style(ScopeName.XmlDelimiter) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.XmlName) {
                    Foreground = DullRed
                },
                new Style(ScopeName.ClassName) {
                    Foreground = Color.MediumTurquoise
                },
                new Style(ScopeName.CssSelector) {
                    Foreground = DullRed
                },
                new Style(ScopeName.CssPropertyName) {
                    Foreground = Color.Red
                },
                new Style(ScopeName.CssPropertyValue) {
                    Foreground = Color.Blue
                },
                new Style(ScopeName.SqlSystemFunction) {
                    Foreground = Color.Magenta
                },
                new Style(ScopeName.PowerShellAttribute) {
                    Foreground = Color.PowderBlue
                },
                new Style(ScopeName.PowerShellOperator) {
                    Foreground = Color.Gray
                },
                new Style(ScopeName.PowerShellType) {
                    Foreground = Color.Teal
                },
                new Style(ScopeName.PowerShellVariable) {
                    Foreground = Color.OrangeRed
                }
            };
        }

        public string Name
        {
            get { return "DefaultStyleSheet"; }
        }

        public StyleDictionary Styles
        {
            get { return styles; }
        }
    }
}