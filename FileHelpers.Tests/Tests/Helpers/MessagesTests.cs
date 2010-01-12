using System;
using System.Text.RegularExpressions;
using System.Xml;
using FileHelpers;
using FileHelpers.Detection;
using FileHelpers.Dynamic;
using NUnit.Framework;
using System.Collections.Generic;


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
            try
            {
                new DelimitedClassBuilder("", "\t");
                Assert.Fail("No exception :(");
            }
            catch (FileHelpersException ex)
            {
                ex.Message.AssertEqualTo("The string '' not is a valid .NET identifier");
            }
        }


    }

}
