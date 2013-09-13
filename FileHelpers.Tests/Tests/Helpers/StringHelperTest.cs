﻿using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    /// <summary>
    /// FileHelpers Helpers  STringHelper tests
    /// </summary>
    [TestFixture]
    class StringHelperTest
    {
        [Test(Description="String without spaces into RemoveBlanks")]
        public void RemoveNoBlanks()
        {
            StringHelper.RemoveBlanks("+41").AssertEqualTo("+41");
        }

        [Test(Description = "String with leading blanks into RemoveBlanks")]
        public void RemoveLeadingBlanks()
        {
            StringHelper.RemoveBlanks("     +41").AssertEqualTo("     +41");
        }

        [Test(Description = "String with blanks after sign logic into RemoveBlanks")]
        public void RemoveBlanksAfterSign()
        {
            StringHelper.RemoveBlanks("     + 41").AssertEqualTo("+41");
        }

        [Test(Description = "String with trailing blank into RemoveBlanks")]
        public void RemoveTrailingBlanks()
        {
            StringHelper.RemoveBlanks("     + 41").AssertEqualTo("+41");
        }

        [Test (Description = "String IsNullOrWhiteSpace help method tests")]
        public void IsNullOrWhiteSpace ()
        {
            StringHelper.IsNullOrWhiteSpace ("     ").AssertEqualTo (true, "WhiteSpaces not detected");
            StringHelper.IsNullOrWhiteSpace (null).AssertEqualTo (true, "null string not detected");
            StringHelper.IsNullOrWhiteSpace (String.Empty).AssertEqualTo (true, "empty string not detected");
            StringHelper.IsNullOrWhiteSpace (" test ").AssertEqualTo (false, "valid string not detected");
        }
    }
}
