using System;
using FileHelpers;
using FileHelpers.Detection;
using FileHelpers.Dynamic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;


namespace FileHelpers.Tests.Mocking
{

    [TestFixture]
    public class FileHelperEngineTests
    {
        public enum ClientStatus
        {
            Unknown,
            Ok,
            FileNotFound,
            UnhandledError

        }

        public class FileHelpersClient
        {
            public IFileHelperEngine<SampleType> Engine { get; set; }
            public SampleType[] DataInFile { get; set; }
            public ClientStatus Status { get; set; }

            public FileHelpersClient(IFileHelperEngine<SampleType> engine)
            {
                Engine = engine;
            }

            public void WorkWithFiles()
            {
                DataInFile = null;
                try
                {
                    DataInFile = Engine.ReadFile("bla bla bla");
                    Status = ClientStatus.Ok;
                }
                catch (FileNotFoundException)
                {
                    Status = ClientStatus.FileNotFound;
                }
                catch (OutOfMemoryException)
                {
                    Status = ClientStatus.UnhandledError;
                    throw;
                }
                catch (Exception)
                {
                    Status = ClientStatus.UnhandledError;
                }
                
            }
        }


        [Test]
        public void BasicMocking()
        {
            var mock = new Mock<IFileHelperEngine<SampleType>>();
            mock.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(new[]
                             {
                                 new SampleType {Field1 = new DateTime(2010, 3, 2), Field2 = "field2.1", Field3 = 1},
                                 new SampleType {Field1 = new DateTime(2010, 4, 5), Field2 = "field2.2", Field3 = 2},
                                 new SampleType {Field1 = new DateTime(2010, 6, 7), Field2 = "field2.3", Field3 = 3}
                             });

            var client = new FileHelpersClient(mock.Object);

            client.WorkWithFiles();

            client.DataInFile.Length.AssertEqualTo(3);

            client.Status.AssertEqualTo(ClientStatus.Ok);
            client.DataInFile[0].Field1.AssertEqualTo(new DateTime(2010, 3, 2));
            client.DataInFile[1].Field1.AssertEqualTo(new DateTime(2010, 4, 5));
            client.DataInFile[2].Field1.AssertEqualTo(new DateTime(2010, 6, 7));


            client.DataInFile[0].Field2.AssertEqualTo("field2.1");
            client.DataInFile[1].Field2.AssertEqualTo("field2.2");
            client.DataInFile[2].Field2.AssertEqualTo("field2.3");

            client.DataInFile[0].Field3.AssertEqualTo(1);
            client.DataInFile[1].Field3.AssertEqualTo(2);
            client.DataInFile[2].Field3.AssertEqualTo(3);

            mock.VerifyAll();
        }

        
        [Test]
        public void FileNotFoundException()
        {
            var mock = new Mock<IFileHelperEngine<SampleType>>();
            mock.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Throws(new FileNotFoundException());

            var client = new FileHelpersClient(mock.Object);
            client.WorkWithFiles();

            client.Status.AssertEqualTo(ClientStatus.FileNotFound);
            client.DataInFile.AssertIsNull();

            mock.VerifyAll();
        }


        [Test]
        public void UnhandledExceptionBasic()
        {
            var mock = new Mock<IFileHelperEngine<SampleType>>();
            mock.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Throws(new Exception());

            var client = new FileHelpersClient(mock.Object);
            client.WorkWithFiles();

            client.Status.AssertEqualTo(ClientStatus.UnhandledError);
            client.DataInFile.AssertIsNull();

            mock.VerifyAll();
        }

        [Test]
        public void UnhandledExceptionAnother()
        {
            var mock = new Mock<IFileHelperEngine<SampleType>>();
            mock.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Throws(new InvalidOperationException());

            var client = new FileHelpersClient(mock.Object);
            client.WorkWithFiles();

            client.Status.AssertEqualTo(ClientStatus.UnhandledError);
            client.DataInFile.AssertIsNull();

            mock.VerifyAll();
        }



        [Test]
        public void BubblingOutOfMemory()
        {
            var mock = new Mock<IFileHelperEngine<SampleType>>();
            mock.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Throws(new OutOfMemoryException());

            var client = new FileHelpersClient(mock.Object);
            
            Assert.Throws<OutOfMemoryException>( 
                    () => client.WorkWithFiles() 
                );

            mock.VerifyAll();
        }
    }

}
