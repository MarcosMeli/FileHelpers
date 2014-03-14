// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using ExamplesFx.ColorCode.Common;
using ExamplesFx.ColorCode.Parsing;

namespace ExamplesFx.ColorCode.Formatting
{
    internal class HtmlFormatter : IFormatter
    {
        public void Write(string parsedSourceCode,
            IList<Scope> scopes,
            IStyleSheet styleSheet,
            TextWriter textWriter)
        {
            var styleInsertions = new List<TextInsertion>();

            foreach (var scope in scopes)
                GetStyleInsertionsForCapturedStyle(scope, styleInsertions);

            styleInsertions.SortStable((x, y) => x.Index.CompareTo(y.Index));

            int offset = 0;

            foreach (var styleInsertion in styleInsertions) {
                textWriter.Write(
                    HttpUtility.HtmlEncode(parsedSourceCode.Substring(offset, styleInsertion.Index - offset)));
                if (string.IsNullOrEmpty(styleInsertion.Text))
                    BuildSpanForCapturedStyle(styleInsertion.Scope, styleSheet, textWriter);
                else
                    textWriter.Write(styleInsertion.Text);
                offset = styleInsertion.Index;
            }

            textWriter.Write(HttpUtility.HtmlEncode(parsedSourceCode.Substring(offset)));
        }

        public void WriteFooter(IStyleSheet styleSheet,
            TextWriter textWriter)
        {
            Guard.ArgNotNull(styleSheet, "styleSheet");
            Guard.ArgNotNull(textWriter, "textWriter");

            textWriter.WriteLine();
            WriteHeaderPreEnd(textWriter);
            WriteHeaderDivEnd(textWriter);
        }

        public void WriteHeader(IStyleSheet styleSheet,
            TextWriter textWriter)
        {
            Guard.ArgNotNull(styleSheet, "styleSheet");
            Guard.ArgNotNull(textWriter, "textWriter");

            WriteHeaderDivStart(styleSheet, textWriter);
            WriteHeaderPreStart(textWriter);
            textWriter.WriteLine();
        }

        private static void GetStyleInsertionsForCapturedStyle(Scope scope, ICollection<TextInsertion> styleInsertions)
        {
            styleInsertions.Add(new TextInsertion {
                Index = scope.Index,
                Scope = scope
            });


            foreach (var childScope in scope.Children)
                GetStyleInsertionsForCapturedStyle(childScope, styleInsertions);

            styleInsertions.Add(new TextInsertion {
                Index = scope.Index + scope.Length,
                Text = "</span>"
            });
        }

        private static void BuildSpanForCapturedStyle(Scope scope,
            IStyleSheet styleSheet,
            TextWriter writer)
        {
            Color foreground = Color.Empty;
            Color background = Color.Empty;

            if (styleSheet.Styles.Contains(scope.Name)) {
                Style style = styleSheet.Styles[scope.Name];

                foreground = style.Foreground;
                background = style.Background;
            }

            WriteElementStart("span", foreground, background, writer);
        }

        private static void WriteHeaderDivEnd(TextWriter writer)
        {
            WriteElementEnd("div", writer);
        }

        private static void WriteElementEnd(string elementName,
            TextWriter writer)
        {
            writer.Write("</{0}>", elementName);
        }

        private static void WriteHeaderPreEnd(TextWriter writer)
        {
            WriteElementEnd("pre", writer);
        }

        private static void WriteHeaderPreStart(TextWriter writer)
        {
            WriteElementStart("pre", writer);
        }

        private static void WriteHeaderDivStart(IStyleSheet styleSheet,
            TextWriter writer)
        {
            Color foreground = Color.Empty;
            Color background = Color.Empty;

            if (styleSheet.Styles.Contains(ScopeName.PlainText)) {
                Style plainTextStyle = styleSheet.Styles[ScopeName.PlainText];

                foreground = plainTextStyle.Foreground;
                background = plainTextStyle.Background;
            }

            WriteElementStart("div", foreground, background, writer);
        }

        private static void WriteElementStart(string elementName,
            TextWriter writer)
        {
            WriteElementStart(elementName, Color.Empty, Color.Empty, writer);
        }

        private static void WriteElementStart(string elementName,
            Color foreground,
            Color background,
            TextWriter writer)
        {
            writer.Write("<{0}", elementName);

            if (foreground != Color.Empty ||
                background != Color.Empty) {
                writer.Write(" style=\"");

                if (foreground != Color.Empty)
                    writer.Write("color:{0};", foreground.ToHtmlColor());

                if (background != Color.Empty)
                    writer.Write("background-color:{0};", background.ToHtmlColor());

                writer.Write("\"");
            }

            writer.Write(">");
        }
    }
}