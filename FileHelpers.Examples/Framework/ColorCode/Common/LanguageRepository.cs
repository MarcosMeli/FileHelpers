// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;

namespace ColorCode.Common
{
    internal class LanguageRepository : ILanguageRepository
    {
        private readonly Dictionary<string, ILanguage> loadedLanguages;
        private readonly ReaderWriterLockSlim loadLock;

        public LanguageRepository(Dictionary<string, ILanguage> loadedLanguages)
        {
            this.loadedLanguages = loadedLanguages;
            loadLock = new ReaderWriterLockSlim();
        }

        public IEnumerable<ILanguage> All
        {
            get { return loadedLanguages.Values; }
        }

        public ILanguage FindById(string languageId)
        {
            Guard.ArgNotNullAndNotEmpty(languageId, "languageId");
            
            ILanguage language = null;
            
            loadLock.EnterReadLock();

            try
            {
                if (loadedLanguages.ContainsKey(languageId))
                    language = loadedLanguages[languageId];
            }
            finally
            {
                loadLock.ExitReadLock();
            }

            return language;
        }

        public void Load(ILanguage language)
        {
            Guard.ArgNotNull(language, "language");

            if (string.IsNullOrEmpty(language.Id))
                throw new ArgumentException("The language identifier must not be null or empty.", "language");
            
            loadLock.EnterWriteLock();

            try
            {
                loadedLanguages[language.Id] = language;
            }
            finally
            {
                loadLock.ExitWriteLock();
            }
        }
    }
}