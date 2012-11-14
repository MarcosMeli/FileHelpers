// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using ExamplesFx.ColorCode.Common;
using ExamplesFx.ColorCode.Compilation;
using ExamplesFx.ColorCode.Compilation.Languages;

namespace ExamplesFx.ColorCode
{
    /// <summary>
    /// Provides easy access to ColorCode's built-in languages, as well as methods to load and find custom languages.
    /// </summary>
    internal static class Languages
    {
        internal static readonly LanguageRepository LanguageRepository;
        internal static readonly Dictionary<string, ILanguage> LoadedLanguages;
        internal static Dictionary<string, CompiledLanguage> CompiledLanguages;

        static Languages()
        {
            LoadedLanguages = new Dictionary<string, ILanguage>();
            CompiledLanguages = new Dictionary<string, CompiledLanguage>();
            LanguageRepository = new LanguageRepository(LoadedLanguages);

            Load<JavaScript>();
            Load<Html>();
            Load<CSharp>();
            Load<VbDotNet>();
            Load<Ashx>();
            Load<Asax>();
            Load<Aspx>();
            Load<AspxCs>();
            Load<AspxVb>();
            Load<Sql>();
            Load<Xml>();
            Load<Php>();
            Load<Css>();
            Load<Cpp>();
            Load<Java>();
            Load<PowerShell>();
        }

        /// <summary>
        /// Gets an enumerable list of all loaded languages.
        /// </summary>
        public static IEnumerable<ILanguage> All 
        {
            get { return LanguageRepository.All; }
        }

        /// <summary>
        /// Language support for ASP.NET HTTP Handlers (.ashx files).
        /// </summary>
        /// <value>Language support for ASP.NET HTTP Handlers (.ashx files).</value>
        public static ILanguage Ashx
        {
            get { return LanguageRepository.FindById(LanguageId.Ashx); }
        }

        /// <summary>
        /// Language support for ASP.NET application files (.asax files).
        /// </summary>
        /// <value>Language support for ASP.NET application files (.asax files).</value>
        public static ILanguage Asax
        {
            get { return LanguageRepository.FindById(LanguageId.Asax); }
        }

        /// <summary>
        /// Language support for ASP.NET pages (.aspx files).
        /// </summary>
        /// <value>Language support for ASP.NET pages (.aspx files).</value>
        public static ILanguage Aspx
        {
            get { return LanguageRepository.FindById(LanguageId.Aspx); }
        }

        /// <summary>
        /// Language support for ASP.NET C# code-behind files (.aspx.cs files).
        /// </summary>
        /// <value>Language support for ASP.NET C# code-behind files (.aspx.cs files).</value>
        public static ILanguage AspxCs
        {
            get { return LanguageRepository.FindById(LanguageId.AspxCs); }
        }

        /// <summary>
        /// Language support for ASP.NET Visual Basic.NET code-behind files (.aspx.vb files).
        /// </summary>
        /// <value>Language support for ASP.NET Visual Basic.NET code-behind files (.aspx.vb files).</value>
        public static ILanguage AspxVb
        {
            get { return LanguageRepository.FindById(LanguageId.AspxVb); }
        }

        /// <summary>
        /// Language support for C#.
        /// </summary>
        /// <value>Language support for C#.</value>
        public static ILanguage CSharp
        {
            get { return LanguageRepository.FindById(LanguageId.CSharp); }
        }

        /// <summary>
        /// Language support for HTML.
        /// </summary>
        /// <value>Language support for HTML.</value>
        public static ILanguage Html
        {
            get { return LanguageRepository.FindById(LanguageId.Html); }
        }

        /// <summary>
        /// Language support for Java.
        /// </summary>
        /// <value>Language support for Java.</value>
        public static ILanguage Java
        {
            get { return LanguageRepository.FindById(LanguageId.Java); }
        }

        /// <summary>
        /// Language support for JavaScript.
        /// </summary>
        /// <value>Language support for JavaScript.</value>
        public static ILanguage JavaScript
        {
            get { return LanguageRepository.FindById(LanguageId.JavaScript); }
        }

        /// <summary>
        /// Language support for PowerShell
        /// </summary>
        /// <value>Language support for PowerShell.</value>
        public static ILanguage PowerShell
        {
            get { return LanguageRepository.FindById(LanguageId.PowerShell); }
        }

        /// <summary>
        /// Language support for SQL.
        /// </summary>
        /// <value>Language support for SQL.</value>
        public static ILanguage Sql
        {
            get { return LanguageRepository.FindById(LanguageId.Sql); }
        }

        /// <summary>
        /// Language support for Visual Basic.NET.
        /// </summary>
        /// <value>Language support for Visual Basic.NET.</value>
        public static ILanguage VbDotNet
        {
            get { return LanguageRepository.FindById(LanguageId.VbDotNet); }
        }

        /// <summary>
        /// Language support for XML.
        /// </summary>
        /// <value>Language support for XML.</value>
        public static ILanguage Xml
        {
            get { return LanguageRepository.FindById(LanguageId.Xml); }
        }

        /// <summary>
        /// Language support for PHP.
        /// </summary>
        /// <value>Language support for PHP.</value>
        public static ILanguage Php
        {
            get { return LanguageRepository.FindById(LanguageId.Php); }
        }

        /// <summary>
        /// Language support for CSS.
        /// </summary>
        /// <value>Language support for CSS.</value>
        public static ILanguage Css
        {
            get { return LanguageRepository.FindById(LanguageId.Css); }
        }

        /// <summary>
        /// Language support for C++.
        /// </summary>
        /// <value>Language support for C++.</value>
        public static ILanguage Cpp
        {
            get { return LanguageRepository.FindById(LanguageId.Cpp); }
        }

        /// <summary>
        /// Finds a loaded language by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the language to find.</param>
        /// <returns>An <see cref="ILanguage" /> instance if the specified identifier matches a loaded language; otherwise, null.</returns>
        public static ILanguage FindById(string id)
        {
            return LanguageRepository.FindById(id);
        }

        private static void Load<T>()
            where T : ILanguage, new()
        {
            Load(new T());
        }

        /// <summary>
        /// Loads the specified language.
        /// </summary>
        /// <param name="language">The language to load.</param>
        /// <remarks>
        /// If a language with the same identifier has already been loaded, the existing loaded language will be replaced by the new specified language.
        /// </remarks>
        public static void Load(ILanguage language)
        {
            LanguageRepository.Load(language);
        }
    }
}