using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Dynamic;
using NUnit.Framework;


namespace FileHelpers.Tests
{
    [TestFixture]
    public class MessagesTests
    {
        [Test]
        public void MessageBasic()
        {
            var final =
                @"The field: FieldForTest must be marked as optional because the previous field is marked with FieldOptional. (Try adding [FieldOptional] to FieldForTest)";

            Messages.Errors.FieldOptional
                .Field("FieldForTest")
                .Text
                .AssertEqualTo(final);

            Messages.Errors.FieldOptional
                .Field("FieldForTest")
                .ToString()
                .AssertEqualTo(final);
        }


        [Test]
        public void MessageForExeptions()
        {
            try {
                new DelimitedClassBuilder("", "\t");
                Assert.Fail("No exception :(");
            }
            catch (FileHelpersException ex) {
                ex.Message.AssertEqualTo("The string '' not is a valid .NET identifier");
            }
        }

        [Test]
        public void Quotes()
        {
            Messages.Errors.TestQuote
                .Text
                .AssertEqualTo("The Message class also allows to use \" in any part of the \" text \" .");
        }
    }
}